using System;
using System.Threading.Tasks;

namespace SmartWater.Domain.Core.Contracts.Managers
{
    public interface IExceptionHandler
    {
        TEntity Get<TEntity>(Func<TEntity> unsafeFunction, Func<string> messageGenerator = null);

        Task<TEntity> GetAsync<TEntity>(Func<Task<TEntity>> unsafeFunction, Func<string> messageGenerator = null);

        void Run(Action unsafeAction, Func<string> messageGenerator = null);

        Task RunAsync(Func<Task> unsafeFunction, Func<string> messageGenerator = null);
    }
}
