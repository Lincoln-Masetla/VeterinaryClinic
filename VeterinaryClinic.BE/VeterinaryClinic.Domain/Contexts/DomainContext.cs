using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Domain.Services;

namespace VeterinaryClinic.Domain.Contexts
{
  /// <summary>
  /// Encapsulates all domain specific services and information.
  /// </summary>
  public sealed class DomainContext
  {
    #region Constructors
    public DomainContext(IDynamoDbService dynamoDbService, IDynamoDbTableService dynamoDbTableService)
    {
      DynamoDbService = dynamoDbService;
      DynamoDbTableService = dynamoDbTableService;
    }

    internal DomainContext() { }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the IDynamoDbService instance.
    /// </summary>
    public IDynamoDbService DynamoDbService { get; private set; }

    /// <summary>
    /// Gets the IDynamoDbTableService instance.
    /// </summary>
    public IDynamoDbTableService DynamoDbTableService { get; private set; }
    #endregion
  }
}
