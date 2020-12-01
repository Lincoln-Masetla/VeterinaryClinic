using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Owners
{
  public class GetOwners : VerifiedDomainCommand<List<Owner>>
  {
    public GetOwners(DomainContext domainContext)
             : base(domainContext){}

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<List<Owner>> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.GetOwners();
    }

  }
}
