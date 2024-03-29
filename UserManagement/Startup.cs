using Framework.Service.Builders.Abstraction;
using Framework.Service.Builders.Factory;
using Framework.Service.Builders;
using Framework.Service.Validators;
using Framework.Service;
using UserManagement.Extensions;
using UserManagement.Services;
using UserManagement.Validators.Factory;
using Framework.Configuration.Models;
using Framework.Constant;
using Framework.Service.Extension;


namespace UserManagement;

public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Gets the Configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// The ConfigureServices.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.Configure<AWSConfigurationOptions>(Configuration.GetSection(nameof(AWSConfigurationOptions)));
        services.AddScoped<IUserManagement, Services.UserManagement>();
        services.AddScoped(typeof(IResponseBuilder<>), typeof(ResponseBuilder<>));
        services.AddScoped<IResponseBuilderFactory, ResponseBuilderFactory>();
        services.AddScoped<IRequestDataValidatorFactory, RequestDataValidatorFactory>();

        services.AddCors(o => o.AddPolicy("UserCorsPolicy", builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();            
        }));        
        services.AddCognitoIdentity();

        services.ConfigureSwagger();
    }

    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.AddProblemDetailsSupport();

        app.UseSwagger(new[]
               {
                      new SwaggerConfigurationModel(ApiConstants.ApiVersion, ApiConstants.ApiName, true),
                      new SwaggerConfigurationModel(ApiConstants.JobsApiVersion, ApiConstants.JobsApiName, false)
                    });

        app.UseRouting();

        app.UseCors("UserCorsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}