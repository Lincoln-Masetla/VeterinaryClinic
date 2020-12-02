using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Services
{
  public interface IDynamoDbService
  {
    Task<List<Owner>> GetOwners();

    Task<Owner> GetOwner(int id);

    Task<Owner> CreateOwner(Owner owner);

    Task<Owner> UpdateOwner(Owner owner);

    Task<Pet> CreatePet(Pet pet);

    Task<Pet> UpdatePet(Pet pet);

    Task<Pet> GetPet(int id, int ownerId);

    Task<List<Pet>> GetPets(int ownerId);

    Task<Pet> DeletePet(int id, int ownerId);
    
  }
}
