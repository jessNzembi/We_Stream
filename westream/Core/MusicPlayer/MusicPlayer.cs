using NetCoreAudio;
using System;

namespace MusicPlayer
{
    public class MusicPlayer
    {
        private var player = new Player();
        public static void setPlayBackFinish() {
            player.PlaybackFinished += OnPlaybackFinished;
        }

        public static void PlayMusic(string filename) {
            player.Play(fileName).Wait();
        }

        public static void PauseMusic() {
            player.Pause().Wait();
        }

        public static void ResumeMusic() {
            player.Resume().Wait();
        }

        public static void AdjustVolume(string percentage) {
            byte volume = Convert.ToByte(percentage);
            player.SetVolume(volume).Wait();
        }

        private static void OnPlaybackFinished(object sender, EventArgs e)
        {
            // be used to play next songs that are added to the list
            Console.WriteLine("Playback finished");
        }
    }
}