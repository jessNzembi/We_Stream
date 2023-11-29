using NetCoreAudio;
using System.Diagnostics;

namespace MusicPlayer
{
  public class MusicPlayer
  {
    private Player player = new Player();
    public void setPlayBackFinish()
    {
      player.PlaybackFinished += OnPlaybackFinished;
    }

    public void PlayMusic(string path) { player.Play(path).Wait(); }

    public void PauseMusic() { player.Pause().Wait(); }

    public void ResumeMusic() { player.Resume().Wait(); }

    public void AdjustVolume(string percentage)
    {
      byte volume = Convert.ToByte(percentage);
      player.SetVolume(volume).Wait();
    }

    private void OnPlaybackFinished(object sender, EventArgs e)
    {
      // be used to play next songs that are added to the list
      Debug.WriteLine("Playback finished");
    }
  }
}
