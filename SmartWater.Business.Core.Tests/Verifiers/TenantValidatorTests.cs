using SmartWater.Business.Verifiers;
using SmartWater.CrossCutting.TestHelpers;
using Should;
using Xunit;

namespace SmartWater.SettingsApi.Business.Verifiers
{
    public class TenantValidatorTests : TestsFor<TenantValidator>
    {
        [Fact]
        public void IsValid_TenantIdIsEmptyString_ReturnsFalse()
        {
            // Act
            var result = Instance.Validate(string.Empty);

            // Assert
            result.Passed.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_TenantIdIsNull_ReturnsFalse()
        {
            // Act
            var result = Instance.Validate(null);

            // Assert
            result.Passed.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_TenantIdIsOneCharacter_ReturnsFalse()
        {
            // Act
            var result = Instance.Validate("Å");

            // Assert
            result.Passed.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_TenantIdIsTwoCharacters_ReturnsTrue()
        {
            // Act
            var result = Instance.Validate("Re");

            // Assert
            result.Passed.ShouldBeTrue();
        }


        [Fact]
        public void IsValid_TenantIdIs26Characters_ReturnsFalse()
        {
            // Arrange
            var twentySixCharacters = new string('a', 26);

            // Act
            var result = Instance.Validate(twentySixCharacters);

            // Assert
            result.Passed.ShouldBeFalse();
        }
    }
}
