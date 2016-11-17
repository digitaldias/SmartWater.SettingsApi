using SmartWater.Domain.Core.Contracts.Managers;
using SmartWater.Domain.Core.Contracts.Writers;
using System;
using System.Threading.Tasks;

namespace SmartWater.Business.Core.Managers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger logger)
        {
            _logger = logger; 
        }


        public TEntity Get<TEntity>(Func<TEntity> unsafeFunction, Func<string> messageGenerator= null)
        {
            if (unsafeFunction == null)
                return default(TEntity);

            try
            {
                return unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                LogUsingGeneratorOrFallback(ex, messageGenerator);
            }
            return default(TEntity );
        }


        public async Task<TEntity> GetAsync<TEntity>(Func<Task<TEntity>> unsafeFunction, Func<string> messageGenerator = null)
        {
            if (unsafeFunction == null)
                return await Task.FromResult(default(TEntity));

            try
            {
                return await unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                LogUsingGeneratorOrFallback(ex, messageGenerator);
            }
            return default(TEntity);
        }


        public void Run(Action unsafeAction, Func<string> messageGenerator = null)
        {
            if (unsafeAction == null)
                return;

            try
            {
                unsafeAction.Invoke();
            }
            catch(Exception ex)
            {
                LogUsingGeneratorOrFallback(ex, messageGenerator);
            }
        }


        public async Task RunAsync(Func<Task> unsafeFunction, Func<string> messageGenerator = null)
        {
            if (unsafeFunction == null)
                return;

            try
            {
                await unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                LogUsingGeneratorOrFallback(ex, messageGenerator);
            }
        }


        private void LogUsingGeneratorOrFallback(Exception ex, Func<string> messageGenerator = null)
        {
            if (messageGenerator != null)
            {
                _logger.LogExceptionAsync(ex, Get(messageGenerator));
                return;
            }
            _logger.LogExceptionAsync(ex, ex.Message);
        }
    }
}
