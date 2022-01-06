variable "short_environment" {
  description = "The prefix used for all resources"
}

variable "location" {
  description = "The Azure location where all resources should be created"
}

variable "environment" {
  description = "The environment name being deployed to"
}

variable "admin_group_id" {}
variable "deployment_agent_group_id" {}
variable "max_size_gb" {}
variable "sku_name" {}
variable "administrator_login_password" {}
variable "administrator_login" {}
