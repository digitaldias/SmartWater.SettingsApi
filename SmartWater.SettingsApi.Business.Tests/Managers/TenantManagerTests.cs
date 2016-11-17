using Moq;
using SmartWater.Business.Core.Managers;
using SmartWater.CrossCutting.TestHelpers;
using SmartWater.Domain.Core.Contracts.Managers;
using SmartWater.Domain.Core.Contracts.Readers;
using SmartWater.Domain.Core.Contracts.Verifiers;
using SmartWater.Domain.Core.Contracts.Writers;
using SmartWater.Domain.Core.Entities;
using SmartWater.SettingsApi.Business.Managers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartWater.SettingsApi.Business.Tests.Managers
{
    public class TenantManagerTests : TestsFor<TenantManager>
    {
        public override void OverrideMocks()
        {
            var logger = GetMockFor<ILogger>().Object;
            Inject<IExceptionHandler>(new ExceptionHandler(logger));

            GetMockFor<ITenantValidator>().Setup(o => o.Validate(It.IsAny<string>())).Returns(new ValidationResult { Passed = true });
        }


        [Fact]
        public async Task GetByIdAsync_WhenCalled_VerifiesTenantIdBeforeCallingReader()
        {
            // Act
            var result = await Instance.GetByIdAsync(SomeTenantId);

            // Assert
            GetMockFor<ITenantValidator>().Verify(o => o.Validate(SomeTenantId), Times.Once());
        }


        [Fact]
        public async Task GetByIdAsync_TenantValidationFails_DoesNotInvokeReader()
        {
            // Arrange          
            GetMockFor<ITenantValidator>().Setup(o => o.Validate(It.IsAny<string>())).Returns(ValidationResult.Fail("It aint workin"));

            // Act           
            var results = await Instance.GetByIdAsync(SomeTenantId);

            // Assert
            GetMockFor<ITenantReader>().Verify(o => o.GetByIdAsync(SomeTenantId), Times.Never());
        }


        [Fact]
        public async Task GetByIdAsync_VerificationPasses_CallsIntoTheReader()
        {
            // Arrange
            TenantReaderReturnsValidTenant();

            // Act
            await Instance.GetByIdAsync(SomeTenantId);

            // Assert
            GetMockFor<ITenantReader>().Verify(o => o.GetByIdAsync(SomeTenantId), Times.Once());
        }


        [Fact]
        public async Task GetByIdAsync_ReaderThrowsException_ExceptionHandlerCatchesIt()
        {
            // Arrange
            var badException = new Exception("I'm so bad");
            GetMockFor<ITenantReader>().Setup(o => o.GetByIdAsync(SomeTenantId)).Throws(badException);

            // Act
            var result = await Instance.GetByIdAsync(SomeTenantId);

            // Assert
            GetMockFor<ILogger>().Verify(l => l.LogExceptionAsync(badException, It.IsAny<string>()), Times.Once());
        }


        private void TenantReaderReturnsValidTenant()
        {
            var tenant = new Tenant();
            GetMockFor<ITenantReader>().Setup(o => o.GetByIdAsync(SomeTenantId)).Returns(Task.FromResult(tenant));
        }


        public string SomeTenantId
        {
            get { return "trondheim"; }
        }
    }
}
