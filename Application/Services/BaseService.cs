using System;
using AbcloudzWebAPI.Contracts.Exceptions;

namespace AbcloudzWebAPI.Application.Services;

public abstract class BaseService(ILogger logger)
{
    protected T ExecuteSave<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error executing action: {ex.Message}");
            throw new ApplicationException("An error occurred while processing the request.", ex);
        }
    }
}