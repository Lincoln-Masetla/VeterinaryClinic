using Amazon.DynamoDBv2.DataModel;
using System;

namespace VeterinaryClinic.Domain.Entities
{
  public class Pet
  {
    [DynamoDBHashKey]
    public int PetId { get; set; }
    [DynamoDBRangeKey]
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string SpeciesType { get; set; }
    public DateTime BirthDate { get; set; }
    public string Colour { get; set; }
    public string Notes { get; set; }
  }
}
