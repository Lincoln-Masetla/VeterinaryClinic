using System;
using System.Threading.Tasks;
using VeterinaryClinic.Domain.Contexts;

namespace VeterinaryClinic.Domain.Domain.Commands
{
  /// <summary>
  /// Represents a base domain activity that must be verified in order to execute.
  /// </summary>
  /// <typeparam name="TReturn"></typeparam>
  public abstract class VerifiedDomainCommand<T>
  {
    protected DomainContext domainContext { get; private set; }

    protected bool IsVerified { get; private set; }

    protected VerifiedDomainCommand(DomainContext _domainContext)
    {
      domainContext = _domainContext;
    }

    public Task<T> ExecuteAsync()
    {
      return Task.Run(async () =>
      {
        try
        {
          VerifyCommandState();
          return await ExecuteInternal();
        }
        catch (CommandStateVerificationException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          throw new CommandExecutionException($"An error occurred executing the command", ex);
        }
      });
    }

    /// <summary>
    /// Will help with Verifying business rules 
    /// </summary>
    private void VerifyCommandState()
    {
      if (!IsVerified)
      {
        if (!VerifyStateInternal())
        {
          throw new CommandStateVerificationException($"An error occurred verifying the {GetType()} state.");
        }

        IsVerified = true;
      }
    }

    protected abstract bool VerifyStateInternal();

    protected abstract Task<T> ExecuteInternal();

    /// <summary>
    /// Represents an exception that is thrown by a state verification command when encountering an exception within the scope of execution
    /// </summary>
    public class CommandStateVerificationException : Exception
    {
      public CommandStateVerificationException(string message)
      : base($"{message}")
      {
      }
    }

    /// <summary>
    /// Represents an exception that is thrown by a domain command when encountering an exception within the scope of execution
    /// </summary>
    public class CommandExecutionException : Exception
    {
      public CommandExecutionException(string message, Exception innerException)
          : base($"{message}", innerException)
      {
      }
    }
  }
}
