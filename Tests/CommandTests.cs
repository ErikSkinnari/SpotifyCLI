using SpotifyCLI.Commands;
using FluentAssertions;
using FakeItEasy;
using System;
using Xunit;
using SpotifyAPI.Web;

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
    }
}
