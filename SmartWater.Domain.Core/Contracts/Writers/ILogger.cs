using System;
using System.Threading.Tasks;

namespace SmartWater.Domain.Core.Contracts.Writers
{
    public interface ILogger
    {
        void LogException(Exception ex, Func<string> messageString);


        Task LogExceptionAsync(Exception ex, string message);
    }
}
