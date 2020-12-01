using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Domain.Mappings.RequestBuilders
{
  public static class OwnerRequestBuilder
  {
    public static Document Create(Owner owner)
    {
      return new Document
      {
        ["OwnerId"] = owner.OwnerId,
        ["Name"] = owner.Name,
        ["Email"] = owner.Email,
        ["CellNo"] = owner.CellNo,
        ["Address"] = owner.Address,
      };
    }

    public static GetItemRequest Get()
    {
      return new GetItemRequest
      {
        TableName = nameof(Owner),
      };
    }
  }


}
