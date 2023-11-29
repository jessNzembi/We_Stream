using NetCoreAudio;
using System;

namespace MusicPlayer
{
    public class MusicPlayer
    {
        public static Player createPlayer() {
            var player = new Player();
            player.PlaybackFinished += OnPlaybackFinished;
            return player
        }

        public static void PlayMusic(Player musicplayer, string filename) {
            musicplayer.Play(fileName).Wait();
        }

        public static void PauseMusic(Player musicplayer) {
            musicplayer.Pause().Wait();
        }

        public static void ResumeMusic(Player musicPlayer) {
            player.Resume().Wait();
        }

        public static void AdjustVolume(Player musicplayer) {
            byte volume = Convert.ToByte(Console.ReadLine());
            player.SetVolume(volume).Wait();
        }

        private static void OnPlaybackFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Playback finished");
        }
    }
}
