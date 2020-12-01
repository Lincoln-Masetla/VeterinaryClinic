using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Services
{
  public class DynamoDbTableService : IDynamoDbTableService
  {
    private readonly IAmazonDynamoDB DynamoDbClient;

    public DynamoDbTableService(IAmazonDynamoDB dynamoDbClient)
    {
      DynamoDbClient = dynamoDbClient;
    }

    public async Task CreateOwnerTable()
    {
      try
      {
        var tableName = "Owners";

        var requests = new DeleteTableRequest
        {
          TableName = tableName
        };

        await DynamoDbClient.DeleteTableAsync(requests);

        var request = new CreateTableRequest
        {
          AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "OwnerId",
                        AttributeType = "N"
                    }
                },
          KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "OwnerId",
                        KeyType = "HASH" // Partition Key
                    }
                },
          ProvisionedThroughput = new ProvisionedThroughput
          {
            ReadCapacityUnits = 5,
            WriteCapacityUnits = 5
          },
          TableName = tableName
        };

        DynamoDbClient.CreateTableAsync(request);

        WaitUntilTableReady(tableName);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public async Task CreatePetTable()
    {
      var tableName = "Pets";

      var requests = new DeleteTableRequest
      {
        TableName = tableName
      };

      await DynamoDbClient.DeleteTableAsync(requests);

      var request = new CreateTableRequest
      {
        AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "PetId",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "OwnerId",
                        AttributeType = "N"
                    }
                },
        KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "PetId",
                        KeyType = "HASH" // Partition Key
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "OwnerId",
                        KeyType = "OwnerId" // Sort Key
                    }
                },
        ProvisionedThroughput = new ProvisionedThroughput
        {
          ReadCapacityUnits = 5,
          WriteCapacityUnits = 5
        },
        TableName = tableName
      };

      DynamoDbClient.CreateTableAsync(request);
      WaitUntilTableReady(tableName);
    }

    public void WaitUntilTableReady(string tableName)
    {
      string status = null;

      do
      {
        Thread.Sleep(5000);
        try
        {
          var res = DynamoDbClient.DescribeTableAsync(new DescribeTableRequest
          {
            TableName = tableName
          });

          status = res.Result.Table.TableStatus;
        }
        catch (ResourceNotFoundException)
        {
          throw;
        }

      } while (status != "ACTIVE");
      {
        Console.WriteLine("Table Created Successfully");
      }
    }
  }

}
