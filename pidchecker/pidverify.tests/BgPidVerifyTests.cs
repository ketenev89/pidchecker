using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace pidverify.tests
{
    public class BgPidVerifyTests
    {
        [Fact]
        public void Handle_CorrectPid()
        {
            // Arrange
            string pid = "6101057509";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Handle_CorrectPid_LastDigitEquals0Remainder0()
        {
            // Arrange
            // 6*2 + 1*4 + 0*8 + 1*5 + 1*10 + 5*9 + 7*7 + 6*3 + 0*6 = 143 remainder is 0
            string pid = "6101157600";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Handle_CorrectPid_LastDigitEquals0Remainder10()
        {
            // Arrange
            // 6*2 + 1*4 + 0*8 + 1*5 + 0*10 + 5*9 + 7*7 + 5*3 + 2*6 = 142 remainder is 10
            string pid = "6101057520";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Handle_CorrectPid_YearOver1999()
        {
            // Arrange
            string pid = "6141057508";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Handle_CorrectPid_YearBefore1900()
        {
            // Arrange
            string pid = "6121057503";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Handle_IncorrectPid_LastDigit()
        {
            // Arrange
            string pid = "6101057500";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_YearOver1999_LastDigit()
        {
            // Arrange
            string pid = "6141057500";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_YearBefore1900_LastDigit()
        {
            // Arrange
            string pid = "6121057500";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_IncorrectLength_LessThan10Characters()
        {
            // Arrange
            string pid = "612105750";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_IncorrectLength_MoreThan10Characters()
        {
            // Arrange
            string pid = "61210575033";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_NotAllCharactersAreDigits()
        {
            // Arrange
            string pid = "61a1057509";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Handle_IncorrectPid_BirthdateNotParsable()
        {
            // Arrange
            string pid = "6161057509";

            // Act
            var result = BgPidVerify.Verify(pid);

            // Assert
            result.ShouldBeFalse();
        }
    }
}
