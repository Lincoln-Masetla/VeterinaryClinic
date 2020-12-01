using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Commands.Owners
{

  public class UpdateOwner : VerifiedDomainCommand<Owner>
  {
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CellNo { get; set; }
    public string Address { get; set; }

    public UpdateOwner(DomainContext domainContext)
             : base(domainContext) { }

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override async Task<Owner> ExecuteInternal()
    {
      return await domainContext.DynamoDbService.UpdateOwner(new Owner
      {
        OwnerId = OwnerId,
        Name = Name,
        Address = Address,
        CellNo = CellNo,
        Email = Email,
      });
    }
  }
}
