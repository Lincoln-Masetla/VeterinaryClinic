using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Mappings.RequestBuilders;
using VeterinaryClinic.Domain.Mappings.ResponseBuilders;

namespace VeterinaryClinic.Domain.Services
{
  public class DynamoDbService : IDynamoDbService
  {
    private readonly IAmazonDynamoDB DynamoClient;

    public DynamoDbService(IAmazonDynamoDB dynamoClient)
    {
      DynamoClient = dynamoClient;
    }

    #region Owner Implementations
    public async Task<List<Owner>> GetOwners()
    {
      // Load our specific table 
      var ownerTable = Table.LoadTable(DynamoClient, "Owners");

      var scanFilter = new ScanFilter();

      var search = ownerTable.Scan(scanFilter);

      //todo add pagination
      var documentList = new List<Document>();
      do
      {
        documentList = await search.GetNextSetAsync();
      } while (!search.IsDone);

      return OwnerResponseBuilder.CreateList(documentList);
    }

    public async Task<Owner> GetOwner(int id)
    {
      // Load our specific table 
      var ownerTable = Table.LoadTable(DynamoClient, "Owners");

      // get the owner
      var result = await ownerTable.GetItemAsync(id);

      //Map results  to response
      return OwnerResponseBuilder.Create(result);
    }

    public async Task<Owner> CreateOwner(Owner owner)
    {
      // Load our specific table 
      var ownerTable = Table.LoadTable(DynamoClient, "Owners");

      // Set out uniq Id
      owner.OwnerId = new Random().Next(0, 1000000);

      // create the request to create the owner
      var queryRequest = OwnerRequestBuilder.Create(owner);

      // Save the changes 
      await ownerTable.PutItemAsync(queryRequest);
     
      return owner;

    }

    public async Task<Owner> UpdateOwner(Owner owner)
    {
      // Load our specific table 
      var ownerTable = Table.LoadTable(DynamoClient, "Owners");

      // Get the stored owner
      var foundOwner = await GetOwner(owner.OwnerId);

      if (foundOwner == null) return null;

      // update the request to create the owner
      var queryRequest = OwnerRequestBuilder.Create(owner);

       await ownerTable.UpdateItemAsync(queryRequest);
      return owner;
    }
    #endregion

    #region Pet Implementations
    public async Task<Pet> CreatePet(Pet pet)
    {
      //Load our specific table 
      var petTable = Table.LoadTable(DynamoClient, "Pets");

      //Set out uniq Id
      pet.PetId = new Random().Next(0, 1000000); 

      // create the request to create the pet
      var queryRequest = PetRequestBuilder.Create(pet);

      // Save the changes 
      await petTable.PutItemAsync(queryRequest);
      return pet;
    }

    public async Task<Pet> UpdatePet(Pet pet)
    {
      // Load our specific table 
      var petTable = Table.LoadTable(DynamoClient, "Pets");

      // get the owner
      var foundPet = await petTable.GetItemAsync(pet.PetId, pet.OwnerId);

      if (foundPet == null) return null;

      // create the request to create the pet
      var queryRequest = PetRequestBuilder.Create(pet);

      // Save the changes 
      var document = await petTable.UpdateItemAsync(queryRequest);
      return pet;
    }

    public async Task<Pet> GetPet(int petId, int ownerId)
    {
      // Load our specific table 
      var petTable = Table.LoadTable(DynamoClient, "Pets");

      // get the owner
      var result = await petTable.GetItemAsync(petId, ownerId);

      //Map results  to response
      return PetResponseBuilder.Create(result);
    }

    public async Task<List<Pet>> GetPets(int ownerId)
    {
      // Load our specific table 
      var petTable = Table.LoadTable(DynamoClient, "Pets");

      var scanFilter = new ScanFilter();
      scanFilter.AddCondition("OwnerId", ScanOperator.Equal, ownerId);

      var search = petTable.Scan(scanFilter);

      //todo add pagination
      var documentList = new List<Document>();
      do
      {
        documentList = await search.GetNextSetAsync();
      } while (!search.IsDone);

      //Map results  to response
      return PetResponseBuilder.CreateList(documentList);
    }
    #endregion
  }
}
