using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Owners
{
  public class GetOwner : VerifiedDomainCommand<Owner>
  {
    public int Key { get; set; }

    public GetOwner(DomainContext domainContext)
             : base(domainContext){}

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<Owner> ExecuteInternal()
    {
      return  await domainContext.DynamoDbService.GetOwner(Key);
    }

  }
}
