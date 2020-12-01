using System;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Pets
{
  public class CreatePet : VerifiedDomainCommand<Pet>
  {
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string SpeciesType { get; set; }
    public DateTime BirthDate { get; set; }
    public string Colour { get; set; }
    public string Notes { get; set; }

    public CreatePet(DomainContext domainContext)
             : base(domainContext) { }

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<Pet> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.CreatePet(new Pet
      {
        Name = Name,
        OwnerId = OwnerId,
        BirthDate = BirthDate,
        Colour = Colour,
        SpeciesType = SpeciesType,
        Notes = Notes
      });
    }
  }
}
