using Gtk;
using NetCoreAudio;
using System;

namespace Westream
{
  public class WestreamGui
  {
    private Window mWindowInstance;
    private Builder mBuilder;
	private Player player;

    public WestreamGui()
    {
      Application.Init();
	  player = new Player();
      mBuilder = new Builder();
      mBuilder.AddFromFile("UI/templates/player.ui");
      mBuilder.Autoconnect(this);
      mWindowInstance = (Window)mBuilder.GetObject("window");

      //mWindowInstance.DeleteEvent += (o, e) => Application.Quit();
	  mWindowInstance.DeleteEvent += Window_DeleteEvent;
      mWindowInstance.ShowAll();
    }


	public void onPlayClicked(object sender, EventArgs e)
	{
		//Console.WriteLine("Play button Clicked");
		var SongPath = (Entry)mBuilder.GetObject("Song");

		if (SongPath.Text == "")
		{
		  MessageDialog wd = new MessageDialog(
			mWindowInstance, DialogFlags.Modal, Gtk.MessageType.Error,
			ButtonsType.Ok,
			"Please enter the full path to the song");
		  wd.Title = "Form Error";
		  wd.Run();
		  wd.Destroy();
          return;
		}

		while (true)
		{
			player.Play(SongPath.Text).Wait();
			Console.WriteLine(player.Playing ? "Playback started" : "Could not start the playback");
			break;
		}
		
	}

	public void onPauseClicked(object o, EventArgs e)
	{
		player.Pause().Wait();
		Console.WriteLine(player.Paused ? "Playback paused" : "Could not pause playback");
	}

	public void onResumeClicked(object o, EventArgs e)
	{
		player.Resume().Wait();
		Console.WriteLine(player.Playing && !player.Paused ? "Playback resumed" : "Could not resume playback");
	}

	public void onQuitClicked(object sender, EventArgs e)
	{
		player.Stop().Wait();
		Console.WriteLine(!player.Playing ? "Playback stopped" : "Could not stop the playback");
	}

	private void Window_DeleteEvent(object o, DeleteEventArgs args)
	{
		player.Stop().Wait();
		Console.WriteLine(!player.Playing ? "Playback stopped" : "Could not stop the playback");
		Application.Quit();
	}

    public void Run() { Application.Run(); }
  }
}