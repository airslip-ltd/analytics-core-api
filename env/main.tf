terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "2.90.0"
    }
  }

  backend "azurerm" {
  }
}

provider "azurerm" {
  features {}
}

locals {
  tags = {
    Environment = "${var.environment}"
  }
  app_id = "analytics-core"
  short_app_id = "ancapi"
  short_environment = var.short_environment
  location = var.location
  admin_group_id = var.admin_group_id
  deployment_agent_group_id = var.deployment_agent_group_id
  sku_name = var.sku_name
  administrator_login_password = var.administrator_login_password
  administrator_login = var.administrator_login
  max_size_gb = var.max_size_gb
  ip_addresses = concat([{
    start_ip_address = "0.0.0.0",
    end_ip_address = "0.0.0.0"
  }], var.additional_ip_addresses)
  consumer_group = "data_analytics"
  apim_hostname = var.apim_hostname
  app_tier = var.web_tier
  app_size = var.web_size
  portal_url = var.portal_url
  additional_hosts = var.additional_hosts
  log_level = var.log_level
}

data "azurerm_eventhub_namespace" "yapily_event_hub" {
  name = "airslip-${local.short_environment}-matching-yapily-events-namespace"
  resource_group_name = "airslip-${local.short_environment}-matching-yapily-resources"
}

data "azurerm_eventhub_namespace" "integration_hub" {
  name = "airslip-${local.short_environment}-merchant-integrations-namespace"
  resource_group_name = "airslip-${local.short_environment}-merchant-integrations-resources"
}

data "azurerm_eventhub_namespace" "api2cart" {
  name = "airslip-${local.short_environment}-adapter-api2cart-events-namespace"
  resource_group_name = "airslip-${local.short_environment}-adapter-api2cart-resources"
}

data "azurerm_eventhub_namespace" "customer_portal" {
  name = "airslip-${local.short_environment}-customer-portal-api-events-namespace"
  resource_group_name = "airslip-${local.short_environment}-customer-portal-api-resources"
}

module "ingredient_bowl" {
  source              = "./tf_modules/Airslip.Terraform.Modules/modules/core/resource_group"

  tags                = local.tags
  app_id              = local.app_id
  short_environment   = local.short_environment
  location            = local.location
}

module "storage_account" {
  source                  = "./tf_modules/Airslip.Terraform.Modules/modules/storage_account/account"

  resource_group_name     = module.ingredient_bowl.name
  location                = module.ingredient_bowl.location

  tags                     = local.tags
  app_id                   = local.short_app_id
  short_environment        = local.short_environment
}

module "sql_server" {
  source = "./tf_modules/Airslip.Terraform.Modules/recipes/sql_server_with_databases"

  storage_account = {
    use_existing = true,
    id = module.storage_account.id,
    name = module.storage_account.name,
    primary_access_key = module.storage_account.primary_access_key,
    primary_blob_endpoint = module.storage_account.primary_blob_endpoint,
    account_tier = "Standard",
    account_replication_type = "LRS"
  }

  ip_addresses = local.ip_addresses

  db_configuration = {
    app_id = "${local.app_id}-sql",
    short_app_id = local.short_app_id,
    short_environment = local.short_environment,
    location = local.location,
    tags = local.tags,
    sql_version = "12.0",
    administrator_login = local.administrator_login,
    administrator_login_password = local.administrator_login_password,
    admin_group_id = local.admin_group_id,
    sku_name = local.sku_name,
    zone_redundant = false,
    read_scale = false
  }

  resource_group = {
    use_existing = true,
    resource_group_name = module.ingredient_bowl.name,
    resource_group_location = module.ingredient_bowl.location
  }

  databases = [
    {
      database_name = "analytics",
      max_size_gb = local.max_size_gb
    }
  ]
}

module "api_management" {
  source = "./tf_modules/Airslip.Terraform.Modules/recipes/api_only"

  app_configuration = {
    app_id = "${local.app_id}-api",
    short_environment = local.short_environment,
    location = local.location,
    tags = local.tags,
    app_tier = local.app_tier,
    app_size = local.app_size,
    health_check_path = "",
    apim_hostname = local.apim_hostname
  }

  resource_group = {
    use_existing = true,
    resource_group_name = module.ingredient_bowl.name,
    resource_group_location = module.ingredient_bowl.location
  }

  allowed_hosts = concat([ local.portal_url ], local.additional_hosts)

  app_settings = {
    "ConnectionStrings:SqlServer": module.sql_server.connection_string,
    "EnvironmentSettings:EnvironmentName": var.environment
  }
}

module "func_app_host" {
  source = "./tf_modules/Airslip.Terraform.Modules/recipes/function_app_multiple_apps"

  app_configuration = {
    app_id = "${local.app_id}-func",
    short_app_id = local.short_app_id,
    short_environment = local.short_environment,
    location = local.location,
    tags = local.tags,
    account_tier = "Standard",
    account_replication_type = "LRS"
  }

  resource_group = {
    use_existing = true,
    resource_group_name = module.ingredient_bowl.name,
    resource_group_location = module.ingredient_bowl.location
  }

  storage_account = {
    use_existing = true,
    id = module.storage_account.id,
    name = module.storage_account.name,
    primary_access_key = module.storage_account.primary_access_key,
    primary_connection_string = module.storage_account.connection_string,
    account_tier = "Standard",
    account_replication_type = "LRS"
  }

  function_apps = [
    {
      function_app_name = "proc",
      app_settings = {
        "ConnectionStrings:SqlServer": module.sql_server.connection_string,
        "EnvironmentSettings:EnvironmentName": var.environment,
        "YapilyEventHubConnectionString": data.azurerm_eventhub_namespace.yapily_event_hub.default_primary_connection_string,
        "Api2CartEventHubConnectionString": data.azurerm_eventhub_namespace.api2cart.default_primary_connection_string,
        "TransactionEventHubConnectionString": data.azurerm_eventhub_namespace.integration_hub.default_primary_connection_string,
        "PortalEventHubConnectionString": data.azurerm_eventhub_namespace.customer_portal.default_primary_connection_string,
        "ConsumerGroup": local.consumer_group,
        "Serilog:MinimumLevel:Default": local.log_level
      }
    }
  ]
}