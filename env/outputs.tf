output "resource_group_name" {
  value = "${module.func_app_host.resource_group_name}"
}
output "function_app_names" {
  value = module.func_app_host.function_app_names
}
output "database_connection_string" {
  value = module.sql_server.connection_string
  sensitive = true
}
output "app_service_name" {
  value = module.api_management.app_service_name
}