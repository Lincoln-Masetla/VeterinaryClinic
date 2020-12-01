using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Pets
{
  public class GetPetsByOwner : VerifiedDomainCommand<List<Pet>>
  {
    public int OwnerId { get; set; }

    public GetPetsByOwner(DomainContext domainContext)
             : base(domainContext) { }

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<List<Pet>> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.GetPets(OwnerId);
    }

  }
}
