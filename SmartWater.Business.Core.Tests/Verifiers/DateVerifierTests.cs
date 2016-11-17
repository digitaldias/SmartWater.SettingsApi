using Should;
using SmartWater.Business.Verifiers;
using SmartWater.CrossCutting.TestHelpers;
using System;
using Xunit;

namespace SmartWater.Business.Core.Tests.Verifiers
{
    public class DateVerifierTests : TestsFor<DateVerifier>
    {
        [Fact]
        public void IsValid_DateFromHasNoValue_ResultIsFalse()
        {
            // Act           
            var result = Instance.IsValid(DateTime.Now);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_DateToHasNoValue_ResultIsFalse()
        {
            // Act           
            var result = Instance.IsValid(DateTime.Now);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_DateIsWithinRange_ResultIsTrue()
        {
            // Arrange            
            var date = DateTime.Now;
            var minDate = date.AddHours(-1);
            var maxDate = date.AddHours(1);

            // Act           
            var result = Instance.IsValid(date, minDate, maxDate);

            // Assert
            result.ShouldBeTrue();
        }


        [Fact]
        public void IsValid_MinDateIsAfterMaxDate_ResultIsFalse()
        {
            // Arrange            
            var date = DateTime.Now;
            var minDate = date.AddHours(1);
            var maxDate = date.AddHours(-1);

            // Act           
            var result = Instance.IsValid(date, minDate, maxDate);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_DateIsBeforeMinDate_ResultIsFalse()
        {
            // Arrange            
            var date = DateTime.Now;
            var minDate = date.AddHours(1);
            var maxDate = date.AddHours(3);

            // Act           
            var result = Instance.IsValid(date, minDate, maxDate);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValid_DateIsAfterMaxDate_ResultIsFalse()
        {
            // Arrange            
            var date = DateTime.Now;
            var minDate = date.AddHours(-10);
            var maxDate = date.AddHours( -5);

            // Act           
            var result = Instance.IsValid(date, minDate, maxDate);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValidRange_DateFromHasNoValue_ResultIsFalse()
        {
            // Act           
            var result = Instance.IsValidRange();

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValidRange_DateToHasNoValue_ResultIsFalse()
        {
            // Act           
            var result = Instance.IsValidRange(validFrom: DateTime.Now);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValidRange_ValidFromComesAfterValidTo_ResultIsFalse()
        {
            // Arrange            
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(-1);

            // Act           
            var result = Instance.IsValidRange(from, to);

            // Assert
            result.ShouldBeFalse();
        }


        [Fact]
        public void IsValidRange_ValidRangeFromAndTo_ResultIsTrue()
        {
            // Arrange            
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now.AddDays(1);

            // Act           
            var result = Instance.IsValidRange(from, to);

            // Assert
            result.ShouldBeTrue();
        }
    }
}
