using Amazon.DynamoDBv2.DocumentModel;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Mappings.RequestBuilders
{
  public static class PetRequestBuilder
  {
    public static Document Create(Pet pet)
    {
      return new Document
      {
        ["PetId"] = pet.PetId,
        ["Name"] = pet.Name,
        ["SpeciesType"] = pet.SpeciesType,
        ["Notes"] = pet.Notes,
        ["Colour"] = pet.Colour,
        ["BirthDate"] = pet.BirthDate,
        ["OwnerId"] = pet.OwnerId,
      };
    }
  }
}

