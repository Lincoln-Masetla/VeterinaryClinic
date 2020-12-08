using Amazon.DynamoDBv2;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Services;


//NB did now worry about managing state. Used unique values and linq queries to get the correct testting values
//Methods are written as follows WhatMethodToTest_Parameters_WhatYouAreTesting
namespace VeterinaryClinic.Domain.Tests.Services.DynamoDb
{
  using static Testing;

  public class DynamoDbServiceTests
  {

    #region Owner
    [Test]
    public async Task CreateOwner_Owner_ShouldCreateAnOwner()
    {
      // Arrange
      var req = new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      };

      // Act
      var owner = await CreateOwner(req);

      // Assert
      owner.Should().NotBeNull();
      owner.OwnerId.Should().NotBe(0);
      owner.Address.Should().Be(req.Address);
      owner.CellNo.Should().Be(req.CellNo);
      owner.Email.Should().Be(req.Email);
      owner.Name.Should().Be(req.Name);
    }

    [Test]
    public async Task UpdateOwner_Owner_ShouldUpdateAnOwner()
    {
      // Arrange
      var req = new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      };
      var owner = await CreateOwner(req);

      // update values
      owner.Address = Guid.NewGuid().ToString();
      owner.CellNo = Guid.NewGuid().ToString();
      owner.Email = $"{ Guid.NewGuid().ToString()}@owner.com";
      owner.Name = Guid.NewGuid().ToString();

      // Act
      var res = await UpdateOwner(owner);

      // Assert
      owner.Should().NotBeNull();
      owner.Address.Should().Be(res.Address);
      owner.CellNo.Should().Be(res.CellNo);
      owner.Email.Should().Be(res.Email);
      owner.Name.Should().Be(res.Name);
    }

    [Test]
    public async Task GetOwners_Empty_ShouldGetOwners()
    {
      // Arrange
      var req = new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      };
      var res = await CreateOwner(req);


      // Act
      var owners = await GetOwners();

      var owner = owners.FirstOrDefault(c => c.OwnerId == res.OwnerId);

      // Assert
      owners.Count.Should().BeGreaterThan(0);
      owner.OwnerId.Should().NotBe(0);
      owner.Address.Should().Be(res.Address);
      owner.CellNo.Should().Be(res.CellNo);
      owner.Email.Should().Be(res.Email);
      owner.Name.Should().Be(res.Name);
    }

    [Test]
    public async Task GetOwner_Empty_ShouldGetOwner()
    {
      // Arrange
      var req = new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      };
      var res = await CreateOwner(req);


      // Act
      var owner = await GetOwner(res.OwnerId);

      // Assert
      owner.OwnerId.Should().NotBe(0);
      owner.Address.Should().Be(res.Address);
      owner.CellNo.Should().Be(res.CellNo);
      owner.Email.Should().Be(res.Email);
      owner.Name.Should().Be(res.Name);
    }

    #endregion

    #region Pet

    [Test]
    public async Task CreatePet_Pet_ShouldCreateAPet()
    {
      // Arrange
      var owner = await CreateOwner(new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      });

      var req = new Pet
      {
        BirthDate = DateTime.Now.AddYears(-2),
        OwnerId = owner.OwnerId,
        Colour = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Notes = Guid.NewGuid().ToString(),
        SpeciesType = Guid.NewGuid().ToString()
      };

      // Act
      var pet = await CreatePet(req);

      // Assert
      pet.Should().NotBeNull();
      pet.OwnerId.Should().Be(owner.OwnerId);
      pet.Notes.Should().Be(req.Notes);
      pet.PetId.Should().NotBe(0);
      pet.SpeciesType.Should().Be(req.SpeciesType);
      pet.Name.Should().Be(req.Name);
    }

    [Test]
    public async Task UpdatePet_Pet_ShouldUpdateAPet()
    {
      // Arrange
      var owner = await CreateOwner(new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      });

      var req = new Pet
      {
        BirthDate = DateTime.Now.AddYears(-2),
        OwnerId = owner.OwnerId,
        Colour = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Notes = Guid.NewGuid().ToString(),
        SpeciesType = Guid.NewGuid().ToString()
      };

      var pet = await CreatePet(req);

      pet.BirthDate = DateTime.Now.AddYears(-1);
      pet.OwnerId = owner.OwnerId;
      pet.Colour = Guid.NewGuid().ToString();
      pet.Name = Guid.NewGuid().ToString();
      pet.Notes = Guid.NewGuid().ToString();
      pet.SpeciesType = Guid.NewGuid().ToString();

      // Act
      var res = await UpdatePet(pet);

      // Assert
      pet.Should().NotBeNull();
      pet.OwnerId.Should().Be(res.OwnerId);
      pet.Notes.Should().Be(res.Notes);
      pet.PetId.Should().Be(res.PetId);
      pet.SpeciesType.Should().Be(res.SpeciesType);
      pet.Name.Should().Be(res.Name);
    }

    [Test]
    public async Task GetPetByOwner_OwnerId_ShouldGetPets()
    {
      // Arrange
      var res = await CreateOwner(new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      });

      var pet = await CreatePet(new Pet
      {
        BirthDate = DateTime.Now.AddYears(-2),
        OwnerId = res.OwnerId,
        Colour = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Notes = Guid.NewGuid().ToString(),
        SpeciesType = Guid.NewGuid().ToString()
      });

      // Act
      var pets = await GetPetByOwner(res.OwnerId);

      var found = pets.FirstOrDefault(c => c.PetId == pet.PetId);

      // Assert
      pet.Should().NotBeNull();
      pet.OwnerId.Should().Be(found.OwnerId);
      pet.Notes.Should().Be(found.Notes);
      pet.PetId.Should().Be(found.PetId);
      pet.SpeciesType.Should().Be(found.SpeciesType);
      pet.Name.Should().Be(found.Name);
    }

    [Test]
    public async Task GetPet_PetIdAndOwnerId_ShouldGetPet()
    {
      // Arrange
      var res = await CreateOwner(new Owner
      {
        Address = Guid.NewGuid().ToString(),
        CellNo = Guid.NewGuid().ToString(),
        Email = $"{ Guid.NewGuid().ToString()}@owner.com",
        Name = Guid.NewGuid().ToString(),
      });

      var pet = await CreatePet(new Pet
      {
        BirthDate = DateTime.Now.AddYears(-2),
        OwnerId = res.OwnerId,
        Colour = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Notes = Guid.NewGuid().ToString(),
        SpeciesType = Guid.NewGuid().ToString()
      });

      // Act
      var found = await GetPet(pet.PetId, pet.OwnerId);

      // Assert
      pet.Should().NotBeNull();
      pet.OwnerId.Should().Be(found.OwnerId);
      pet.Notes.Should().Be(found.Notes);
      pet.PetId.Should().Be(found.PetId);
      pet.SpeciesType.Should().Be(found.SpeciesType);
      pet.Name.Should().Be(found.Name);
    }

    #endregion

  }
}
