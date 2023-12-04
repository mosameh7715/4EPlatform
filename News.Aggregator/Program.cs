using _4EPlatform_Controlpanel_Aggregator.GrpClientsConfig;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNewsGrpcClients(builder.Configuration["GrpcServiceSettings:NewsUrl"]);
builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);

builder.Services.AddAuthentication("Bearer")
	  .AddJwtBearer("Bearer",options =>
	  {
		  // the name of your api resources   
		  options.Audience = "UserManagementServer";
		  /// identity server url                    
		  options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
	  });

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ResourceOwnerPolicy",policy =>
	{
		policy.AddAuthenticationSchemes("Bearer");
		policy.RequireClaim("scope","4EMicroServices"); // Require scope associated with Resource Owner Password token
	});

	options.AddPolicy("ClientCredentialsPolicy",policy =>
	{
		policy.AddAuthenticationSchemes("Bearer");
		policy.RequireClaim("scope","CPAggregatorAPI"); // Require scope associated with Client Credentials token
	});
});

builder.Services.AddServices();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1",new OpenApiInfo
	{
		Version = "v1",
		Title = "News.Aggregator APIs Reference",
	});

	c.AddSecurityDefinition(
	"token",
	new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer",
		In = ParameterLocation.Header,
		Name = HeaderNames.Authorization
	}
		);
	c.AddSecurityRequirement(
	new OpenApiSecurityRequirement
	{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "token"
							},
						},
						Array.Empty<string>()
					}
	}
		);
});
#endregion

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
