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
variable "min_capacity" {}
variable "auto_pause_delay_in_minutes" {}
variable "administrator_login_password" {}
variable "administrator_login" {}
variable "additional_ip_addresses" {
  default = []
}
variable "log_level" {
  default = "Warning"
}

variable "apim_hostname" {}
variable "web_tier" {
  description = "The tier used for the app service plan"
  default = "PremiumV2"
}
variable "web_size" {
  description = "The size used for the app service plan"
  default = "P1v2"
}
variable "portal_url" {}
variable "additional_hosts" {
  default = []
}
variable "include_metrics"{
  default = false
}
variable "external_api_url" {}
