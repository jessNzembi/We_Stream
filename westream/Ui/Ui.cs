using Gtk;
using NAudio.Wave;

namespace Westream
{
  public class WestreamGui
  {
    private Window mWindowInstance;
    private Builder mBuilder;

    public WestreamGui()
    {
      Application.Init();
      mBuilder = new Builder();
      mBuilder.AddFromFile("Ui/templates/login.ui");
      mBuilder.Autoconnect(this);
      mWindowInstance = (Window)mBuilder.GetObject("window");

      mWindowInstance.DeleteEvent += (o, e) => Application.Quit();
      //
      // var button = new Button("Click me");
      // button.Clicked += (o, e) => Console.WriteLine("Button clicked");

      // mWindowInstance.Add(button);
      mWindowInstance.ShowAll();
    }

    public void onLoginButtonClicked(object sender, EventArgs e)
    {
      var usernameEntry = (Entry)mBuilder.GetObject("username");
      var roomEntry = (Entry)mBuilder.GetObject("roomID");

      if (usernameEntry.Text == "" || roomEntry.Text == "")
      {
        MessageDialog wd = new MessageDialog(
            mWindowInstance, DialogFlags.Modal, Gtk.MessageType.Error,
            ButtonsType.Ok,
            "Please ensure you fill in your username and password");
        wd.Title = "Form Error";
        wd.Run();
        wd.Destroy();
      }
    }

    public void onPlayClicked(object o, EventArgs e)
    {
      using (
          var audioFile = new AudioFileReader(
              "/home/erick/Music/Lofi/samurai-127302.mp3")) using (var outputDevice =
                                                                       new WaveOutEvent())
      {
        outputDevice.Init(audioFile);
        outputDevice.Play();
        Console.ReadLine();
      }
    }

    public void Run() { Application.Run(); }
  }
}