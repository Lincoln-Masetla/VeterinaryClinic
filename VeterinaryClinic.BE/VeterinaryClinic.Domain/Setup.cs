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
      services.AddTransient<IDynamoDbService, DynamoDbService>();
      services.AddTransient<IDynamoDbTableService, DynamoDbTableService>();
      services.AddTransient<DomainContext, DomainContext>();

      if (!runLocalDynamoDb)
        services.AddAWSService<IAmazonDynamoDB>();
      else
      {
        services.AddTransient<IAmazonDynamoDB>(sp =>
        {
          var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbUrl };
          return new AmazonDynamoDBClient(clientConfig);
        });
      }
    }
  }
}
