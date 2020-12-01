using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;
using VeterinaryClinic.Domain.Domain.Commands;

namespace VeterinaryClinic.Domain.Commands.Tables
{

  public class CreateTable : VerifiedDomainCommand<string>
  {

    public CreateTable( DomainContext domainContext)
            : base(domainContext)
    {

    }

    protected override bool VerifyStateInternal()
    {
      return true;
    }

    protected override Task<string> ExecuteInternal()
    {
      domainContext.DynamoDbTableService.CreateOwnerTable();
      domainContext.DynamoDbTableService.CreatePetTable();
      return Task.FromResult("Successfully created Table");
    }

  }
}
