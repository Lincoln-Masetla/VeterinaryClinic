using Amazon.DynamoDBv2.DataModel;

namespace VeterinaryClinic.Domain.Entities
{
  public class Owner
  {
    [DynamoDBHashKey]
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CellNo { get; set; }
    public string Address { get; set; }
  }
}
