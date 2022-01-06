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
