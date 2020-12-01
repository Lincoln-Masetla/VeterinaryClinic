using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Pets
{

  public class GetPetByKey : VerifiedDomainCommand<Pet>
  {
    public int PetId { get; set; }
    public int OwnerId { get; set; }

    public GetPetByKey(DomainContext domainContext)
             : base(domainContext) { }

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<Pet> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.GetPet(PetId, OwnerId);
    }

  }
}
