using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Services;
using VeterinaryClinic.Lambda.WebApi;

namespace VeterinaryClinic.Domain.Tests.Services
{
  [SetUpFixture]
  public class Testing
  {
    private static IConfiguration Configuration;
    private static IServiceScopeFactory ScopeFactory;
    private static Checkpoint RespawnCheckpoint;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", true, true)
       .AddEnvironmentVariables();

      Configuration = builder.Build();

      var services = new ServiceCollection();

      var startup = new Startup(Configuration);

      services.AddSingleton(Mock.Of<IWebHostEnvironment>(w => w.ApplicationName == "VeterinaryClinic.Lambda.WebApi"));

      startup.ConfigureServices(services);

      ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

    }

    #region Owner
    public async static Task<Owner> CreateOwner(Owner owner)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.CreateOwner(owner);
    }

    public async static Task<Owner> UpdateOwner(Owner owner)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.UpdateOwner(owner);
    }

    public async static Task<List<Owner>> GetOwners()
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.GetOwners();
    }

    public async static Task<Owner> GetOwner(int id)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.GetOwner(id);
    }
    #endregion

    #region Pet

    public async static Task<Pet> CreatePet(Pet pet)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.CreatePet(pet);
    }

    public async static Task<Pet> UpdatePet(Pet pet)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.UpdatePet(pet);
    }

    public async static Task<List<Pet>> GetPetByOwner(int id)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.GetPets(id);
    }

    public async static Task<Pet> GetPet(int petId, int ownerId)
    {
      using var scope = ScopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetService<DomainContext>();

      return await context.DynamoDbService.GetPet(petId, ownerId);
    }

    #endregion
  }
}
