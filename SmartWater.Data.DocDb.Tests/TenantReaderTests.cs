using Moq;
using Should;
using SmartWater.Domain.Core.Contracts.Readers;
using System.Threading.Tasks;
using Xunit;

namespace SmartWater.Data.DocDb.Tests
{
    public class TenantReaderTests 
    {
        private readonly Mock<IConfigurationReader> _configurationReaderMock;

        private TenantReader Instance { get;  set; }

        public TenantReaderTests()
        {
            _configurationReaderMock = new Mock<IConfigurationReader>();

            ConfigureDocumentDbSettings();

            Instance = new TenantReader(_configurationReaderMock.Object);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ResultIsAnArea()
        {
            // Arrange
            var tenantId = "trondheim";

            _configurationReaderMock.Object["DocumentDb.DatabaseName"].ShouldEqual("SmartWater");            

            // Act
            var result = await Instance.GetByIdAsync(tenantId);

            // Assert
            result.ShouldNotBeNull();
        }


        private void ConfigureDocumentDbSettings()
        {
            _configurationReaderMock.SetupGet(c => c["DocumentDb.AuthorizationKey"]).Returns("RfG8gOY0t9pdJFYaHMdQbHXUyjAPOmQs9YXJmGqPyaDojWtxB2bOoNgybHbkJriuS4ZgYIJD7bdxn7qba1icBQ==");
            _configurationReaderMock.SetupGet(c => c["DocumentDb.DatabaseName"]).Returns("SmartWater");
            _configurationReaderMock.SetupGet(c => c["DocumentDb.DatabaseUri"]).Returns("https://ci-water-powel.documents.azure.com:443/");
        }
    }
}
