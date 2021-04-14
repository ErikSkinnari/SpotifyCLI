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
        public void Skip_should_jump_in_correct_direction() {
            // Arrange
            var client = A.Fake<ISpotifyClient>();
            A.CallTo(() => client.Player.SkipNext());

            var sut = new SkipCommand(client);
            var args = new Dictionary<string, string>();
            args.Add("fwd", "false");
            args.Add("bck", "true");

            sut.SetArguments(args);

            // Act
            sut.RunCommand();

            // Assert
            A.CallTo(() => client.Player.SkipPrevious()).MustHaveHappenedOnceExactly();
        }
    }
}
