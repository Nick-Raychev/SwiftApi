using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace SwiftMT799Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string? connectionString = Configuration.GetConnectionString("SQLite");
        if (connectionString != null)
    {
        services.AddSingleton<SwiftRepository>(_ => new SwiftRepository(connectionString));
    }
    else
    {
        throw new InvalidOperationException("Connection string is missing or null.");
    }
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SwiftAPI", Version = "v1" });
        });

        services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(new NLogProviderOptions
        {
            CaptureMessageTemplates = true,
            CaptureMessageProperties = true
        });
    });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwiftAPI v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
    }
}