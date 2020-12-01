using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Mappings.ResponseBuilders
{
  public class PetResponseBuilder
  {
    public static Pet Create(Document result)
    {
      return new Pet
      {
        PetId = int.Parse(result["PetId"]),
        Colour = result["Colour"],
        BirthDate = DateTime.Parse(result["BirthDate"]),
        Notes = result["Notes"],
        Name = result["Name"],
        OwnerId = int.Parse(result["OwnerId"]),
        SpeciesType = result["Name"],
      };
    }

    public static List<Pet> CreateList(List<Document> results)
    {
      return results.Select(result => new Pet
      {
        PetId = int.Parse(result["PetId"]),
        Colour = result["Colour"],
        BirthDate = DateTime.Parse(result["BirthDate"]),
        Notes = result["Notes"],
        Name = result["Name"],
       OwnerId = int.Parse(result["OwnerId"]),
        SpeciesType = result["SpeciesType"],
      }).ToList();
    }

  }
}
