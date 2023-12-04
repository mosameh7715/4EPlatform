namespace News.Aggregator.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionMiddleware, GlobalExceptionMiddleware>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
			services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
			services.AddSingleton<ITokenService,TokenService>();
			services.AddSingleton<INewsServices,NewsServices>();
		}
	}
}
