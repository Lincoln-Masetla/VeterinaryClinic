using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using VeterinaryClinic.Domain.Entities;
using System.Linq;

namespace VeterinaryClinic.Domain.Mappings.ResponseBuilders
{
  public class OwnerResponseBuilder
  {
    public static Owner Create(Document result)
    {
      return new Owner
      {
        OwnerId = int.Parse(result["OwnerId"]),
        Address = result["Address"] ?? string.Empty,
        CellNo = result["CellNo"] ?? string.Empty,
        Email = result["Email"] ?? string.Empty,
        Name = result["Name"] ?? string.Empty,
      };
    }

    public static List<Owner> CreateList(List<Document> results)
    {
      return results.Select(result => new Owner
      {
        OwnerId = int.Parse(result["OwnerId"]),
        Address = result["Address"] ?? string.Empty,
        CellNo = result["CellNo"] ?? string.Empty,
        Email = result["Email"] ?? string.Empty,
        Name = result["Name"] ?? string.Empty,
      }).ToList();
    }

   
  }
}
