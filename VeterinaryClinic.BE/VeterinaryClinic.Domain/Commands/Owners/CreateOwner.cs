using System;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Services;

namespace VeterinaryClinic.Domain.Domain.Commands.Owners
{
  public class CreateOwner : VerifiedDomainCommand<Owner>
  {

    public string Name { get; set; }
    public string Email { get; set; }
    public string CellNo { get; set; }
    public string Address { get; set; }

    public CreateOwner(DomainContext domainContext)
             : base(domainContext){}

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<Owner> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.CreateOwner(new Owner 
      {
        Name = Name,
        Address = Address,
        CellNo = CellNo,
        Email =Email,
      }); 
    }
  }
}
