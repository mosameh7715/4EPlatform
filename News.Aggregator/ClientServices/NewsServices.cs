namespace News.Aggregator.ClientServices
{
	public class NewsServices:INewsServices
	{
		private readonly NewsProtoService.NewsProtoServiceClient _newsProto;
		private readonly WorkspacePermissionProtoService.WorkspacePermissionProtoServiceClient _workspacePermissionProto;

		public NewsServices(NewsProtoService.NewsProtoServiceClient newsProto,
					  WorkspacePermissionProtoService.WorkspacePermissionProtoServiceClient workspacePermissionProto)
		{
			_newsProto = newsProto;
			_workspacePermissionProto = workspacePermissionProto;
		}

		public async Task PostActionContollerName()
		{
			try
			{

				var News = await GetNewsControllerAndActionName();

				var Requst = new RequestWorkspacePermissionRequest();

				foreach(var item in News)
				{

					Requst.ModuleInfo.Add(new ModuleInfo
					{
						ActionName = item.ActionName,
						ControllerName = item.ControllerName,
						Key = item.Key,
						MethodVerd = item.MethodVerd,
						Path = item.Path,
					});
				}
				var response = await _workspacePermissionProto.PostWorkspacePermissionAsync(Requst);

			}
			catch(Exception)
			{


			}
		}

		public async Task<List<ModulaInfo>> GetNewsControllerAndActionName()
		{

			var ModulaInfo = new List<ModulaInfo>();
			try
			{
				var response = await _newsProto.GetNewsControllerAndActionNameAsync(new Empty());
				;
				foreach(var item in response.ModulaInfo)
				{
					ModulaInfo.Add(new ModulaInfo()
					{
						ActionName = item.ActionName,
						ControllerName = item.ActionName,
						Path = item.Path
					,
						MethodVerd = item.MethodVerd,
						Key = item.Key
					});
				}
			}
			catch(Exception ex) { }

			return ModulaInfo;
		}
	}
}
