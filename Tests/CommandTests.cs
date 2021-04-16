using SpotifyCLI.Commands;
using FluentAssertions;
using FakeItEasy;
using System;
using Xunit;
using SpotifyAPI.Web;
using System.Collections.Generic;

namespace SpotifyCLI.Tests
{
    public class CommandTests {

        [Fact]
        public void Change_play_status_should_attempt_to_change_playback_status() {
            // Arrange
            var playingClient = A.Fake<ISpotifyClient>();
            var pausedClient = A.Fake<ISpotifyClient>();

            A.CallTo(() => playingClient.Player.GetCurrentPlayback()).Returns(new CurrentlyPlayingContext() { IsPlaying = true });
            A.CallTo(() => pausedClient.Player.GetCurrentPlayback()).Returns(new CurrentlyPlayingContext() { IsPlaying = false });

            var playingSut = new ChangePlayStatusCommand(playingClient);
            var pausedSut = new ChangePlayStatusCommand(pausedClient);
        
            // Act
            playingSut.RunCommand();
            pausedSut.RunCommand();
        
            // Assert
            A.CallTo(() => playingClient.Player.PausePlayback()).MustHaveHappenedOnceExactly();
            A.CallTo(() => pausedClient.Player.ResumePlayback()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Next_should_be_called_properly() {
            // Arrange
            var client = A.Fake<ISpotifyClient>();
            A.CallTo(() => client.Player.SkipNext()).Returns(true);

            var sut = new NextCommand(client);

            // Act
            sut.RunCommand();

            // Assert
            A.CallTo(() => client.Player.SkipNext()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Previous_should_be_called_properly() {
            // Arrange
            var client = A.Fake<ISpotifyClient>();
            A.CallTo(() => client.Player.SkipPrevious()).Returns(true);

            var sut = new PreviousCommand(client);

            // Act
            sut.RunCommand();

            // Assert
            A.CallTo(() => client.Player.SkipPrevious()).MustHaveHappenedOnceExactly();
        }
    }
}
