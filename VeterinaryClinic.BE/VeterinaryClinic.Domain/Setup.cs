using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Services;

namespace VeterinaryClinic.Domain
{
  public static class DomainModuleRegistration
  {
    public static void AddDomainModuleDependencies(this IServiceCollection services, bool runLocalDynamoDb, string dynamoDbUrl)
    {
      services.AddSingleton<IDynamoDbService, DynamoDbService>();
      services.AddSingleton<IDynamoDbTableService, DynamoDbTableService>();
      services.AddScoped<DomainContext, DomainContext>();

      if (!runLocalDynamoDb)
        services.AddAWSService<IAmazonDynamoDB>();
      else
      {
        services.AddSingleton<IAmazonDynamoDB>(sp =>
        {
          var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbUrl };
          return new AmazonDynamoDBClient(clientConfig);
        });
      }
    }
  }
}
