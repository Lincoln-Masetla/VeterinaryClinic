using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Services
{
  public interface IDynamoDbTableService
  {
    Task CreateOwnerTable();
    Task CreatePetTable();
  }
}
