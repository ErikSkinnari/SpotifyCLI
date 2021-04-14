using System;
using FakeItEasy;
using FluentAssertions;
using SpotifyAPI.Web;
using SpotifyCLI.Utilities;
using Xunit;

namespace SpotifyCLI.Tests {
    public class ExtensionTests {
        [Fact] 
        public void Token_expiration_should_be_correct() {
            // Arrange 
            var sut = new PKCETokenResponse();
            sut.CreatedAt = DateTime.UtcNow.AddDays(-14);
            sut.ExpiresIn = 1;

            // Act
            var result = sut.HasExpired();

            // Assert
            result.Should().BeTrue();
        }
    }
}