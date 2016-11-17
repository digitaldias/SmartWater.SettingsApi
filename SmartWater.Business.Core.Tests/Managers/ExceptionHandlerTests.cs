using Moq;
using Should;
using SmartWater.Business.Core.Managers;
using SmartWater.CrossCutting.TestHelpers;
using SmartWater.Domain.Core.Contracts.Writers;
using SmartWater.Domain.Core.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartWater.Business.Core.Tests.Managers
{
    public class ExceptionHandlerTests : TestsFor<ExceptionHandler>
    {
        [Fact]
        public void Get_FunctionIsNull_DoesNotInvokeLogger()
        {
            // Arrange            
            Func<int> nullFunction = null;

            // Act           
            Instance.Get(nullFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public void Get_FunctionIsNull_ResultIsDefaultForType()
        {
            // Arrange            
            Func<ValidationResult> nullFunction = null;

            // Act           
            var result = Instance.Get(nullFunction);

            // Assert
            result.ShouldEqual(default(ValidationResult));
        }


        [Fact]
        public void Get_FunctionThrowsException_LoggerIsInvoked()
        {
            // Arrange
            var badException = new Exception("I'm bad");
            Func<int> unsafeAction = () => { throw badException; };

            // Act
            var result = Instance.Get(unsafeAction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, "I'm bad"), Times.Once());
        }


        [Fact]
        public void Get_FunctionWorks_NoLoggingHappens()
        {
            // Arrange
            Func<int> workingMethod = () => 13;

            // Act
            var result = Instance.Get(workingMethod, null);

            // Assert
            result.ShouldEqual(13);
        }


        [Fact]
        public void Get_FunctionThrowsAndMessageExpressionThrows_LoggerIsCalledTwice()
        {
            // Arrange
            var exception = new Exception("I'm bad");
            var stringException = new ArgumentException("I ran out of arguments!");

            Func<int> unsafeAction = () => { throw exception; };
            Func<string> badMessage = () => { throw stringException; };

            // Act
            var result = Instance.Get(unsafeAction, badMessage) ;

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Exactly(2));
        }


        [Fact]
        public async Task GetAsync_FunctionIsNull_ReturnsWithoutLogging()
        {
            // Arrange
            Func<Task<int>> noFunction = null;

            // Act           
            var results = await Instance.GetAsync<int>(noFunction);

            // Assert
            GetMockFor<ILogger>().Verify(l => l.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public async Task GetAsync_FunctionIsValid_ReturnsFunctionResult()
        {
            // Arrange
            Func<Task<int>> simpleFunction = () => Task.FromResult<int>(1313);

            // Act           
            var results = await Instance.GetAsync(simpleFunction);

            // Assert
            results.ShouldEqual(1313);
        }


        [Fact]
        public async Task GetAsync_FunctionThrows_LoggerIsInvoked()
        {
            // Arrange          
            var badException = new Exception();
            Func<Task<int>> badFunction = () => { throw badException; };

            // Act           
            await Instance.GetAsync(badFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task GetAsync_FunctionThrowsWithoutMessageFunction_LoggerIsInvoked()
        {
            // Arrange          
            var exceptionMessage = "I'm bad";
            var badException = new Exception(exceptionMessage);
            Func<Task<int>> badFunction = () => { throw badException; };

            // Act           
            await Instance.GetAsync(badFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.AtLeastOnce());
        }


        [Fact]
        public async Task GetAsync_FunctionThrowsWithMessageFunction_LoggerIsInvoked()
        {
            // Arrange          
            var exceptionMessage = "I'm bad";
            var badException = new Exception(exceptionMessage);

            string worseMessage = "This is really no better";
            Func<string> messageFunction = () => worseMessage;
            Func<Task<int>> badFunction = () => { throw badException; };

            // Act           
            await Instance.GetAsync(badFunction, messageFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, worseMessage), Times.AtLeastOnce());
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.Never());
        }


        [Fact]
        public async Task GetAsync_FunctionThrows_ResultIsDefaultType()
        {
            // Arrange          
            var badException = new Exception();
            Func<Task<ValidationResult>> badFunction = () => { throw badException; };

            // Act           
            var result = await Instance.GetAsync(badFunction);

            // Assert
            result.ShouldEqual(default(ValidationResult));
        }


        [Fact]
        public void Run_ActionIsNull_DoesNotInvokeLogger()
        {
            // Act           
            Instance.Run(null);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public void Run_ActionIsNice_DoesNotInvokeLogger()
        {
            // Arrange
            Action action = () => { int i = 1 + 0; i++; };

            // Act           
            Instance.Run(action);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public void Run_ActionThrows_InvokesLoggerWithExceptionMessage()
        {
            // Arrange
            var exceptionMessage = "I'm bad";
            var badException = new Exception(exceptionMessage);
            Action action = () => { throw badException; };

            // Act           
            Instance.Run(action);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.Once());
        }


        [Fact]
        public void Run_ActionThrowsWithMessageGenerator_InvokesLoggerWithGeneratedMessage()
        {
            // Arrange
            var exceptionMessage = "I'm bad";
            var generatedMessage = "I'm worse";
            Func<string> generator = () => generatedMessage;
            var badException = new Exception(exceptionMessage);
            Action action = () => { throw badException; };

            // Act           
            Instance.Run(action, generator);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, generatedMessage), Times.Once());
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.Never());
        }


        [Fact]
        public async Task RunAsync_unsafeFunctionIsNull_DoesNotLog()
        {
            // Arrange          
            Func<Task<int>> nullFunction = null;

            // Act           
            await Instance.RunAsync(nullFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public async Task RunAsync_unsafeFunctionIsOk_DoesNotLog()
        {
            // Arrange          
            Func<Task<int>> okFunction = async () => await Task.FromResult(1313);

            // Act           
            await Instance.RunAsync(okFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
        }


        [Fact]
        public async Task RunAsync_unsafeFunctionThrowsWithoutMessageGenerator_LogsExceptionUsingExceptionMessage()
        {
            // Arrange          
            var exceptionMessage = "I'm bad";
            var badException = new Exception(exceptionMessage);
            Func<Task<int>> badFunction = async () => { await Task.FromResult(13); throw (badException); };

            // Act           
            await Instance.RunAsync(badFunction);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.Once());
        }


        [Fact]
        public async Task RunAsync_unsafeFunctionThrowsWithtMessageGenerator_LogsExceptionUsingGenerator()
        {
            // Arrange          
            var exceptionMessage = "I'm bad";
            var badException = new Exception(exceptionMessage);

            var generatorMessage = "I'm worse!";
            Func<string> generator = () => generatorMessage;
            Func<Task<int>> badFunction = async () => { await Task.FromResult(13); throw (badException); };

            // Act           
            await Instance.RunAsync(badFunction, generator);

            // Assert
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, exceptionMessage), Times.Never());
            GetMockFor<ILogger>().Verify(o => o.LogExceptionAsync(badException, generatorMessage), Times.Once());
        }
    }
}
