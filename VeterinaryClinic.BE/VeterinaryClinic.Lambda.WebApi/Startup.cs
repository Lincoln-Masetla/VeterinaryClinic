using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeterinaryClinic.Domain;
using VeterinaryClinic.Domain.Services;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace VeterinaryClinic.Lambda.WebApi
{
  public class Startup
  {

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public static IConfiguration Configuration { get; private set; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      // Domain Setup
      var dynamoDbConfig = Configuration.GetSection("DynamoDb");
      var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");
      var dynamoDbUrl = dynamoDbConfig.GetValue<string>("LocalServiceUrl");
      services.AddDomainModuleDependencies(runLocalDynamoDb, dynamoDbUrl);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseCors(options =>
      {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
        options.SetPreflightMaxAge(TimeSpan.FromMinutes(20));
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
