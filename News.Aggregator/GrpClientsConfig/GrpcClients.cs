namespace _4EPlatform_Controlpanel_Aggregator.GrpClientsConfig
{

    public static class GrpcClients
    {
        #region News
        public static void AddNewsGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClientWithClientCredentials<NewsProtoService.NewsProtoServiceClient>(url);

		}
		#endregion

		#region UserManagement
		public static void AddUserManagementGrpcClients(this IServiceCollection services,string url)
		{
			services.AddGrpcServiceClientWithClientCredentials<WorkspacePermissionProtoService.WorkspacePermissionProtoServiceClient>(url);

		}
		#endregion
	}
}
