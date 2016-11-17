using SmartWater.Domain.Core.Contracts.Writers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartWater.Data.Fakes
{
    public class FakeLogger : ILogger
    {
        public void LogException(Exception ex, Func<string> message)
        {
            Debug.WriteLine(message.Invoke());
        }

        public async Task LogExceptionAsync(Exception ex, string message)
        {
            await Task.Run(() => Debug.WriteLine(message));
        }
    }
}
