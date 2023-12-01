using Gtk;
using NetCoreAudio;

namespace Westream
{
  public class WestreamGui
  {
    private Window mWindowInstance;
    private Builder mBuilder;
    private Server? mServer;
    private Client? mClient;
    private Thread? mComThread;

    public WestreamGui()
    {
      Application.Init();
      mBuilder = new Builder();
      mBuilder.AddFromFile("Ui/templates/login.ui");
      mBuilder.Autoconnect(this);
      mWindowInstance = (Window)mBuilder.GetObject("window");

      mWindowInstance.DeleteEvent += (o, e) => Application.Quit();
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
        return;
      }

      // Create a client
      mClient = new Client(usernameEntry.Text.Trim(), roomEntry.Text.Trim());
    }

    public void onCreateServer(object sender, EventArgs e)
    {

      mServer = new Server();
      mServer.start();
      mComThread = new Thread(new ThreadStart(mServer!.ListenAndServe));
    }

    public void onJoinServerClicked(object sender, EventArgs e) { }

    public void onPlayClicked(object o, EventArgs e)
    {
      var player = new Player();

      while (true)
      {
        player.Play("/home/erick/Music/Samurai Japanese Lofi HipHop Mix.mp3");
      }
    }

    public void Run() { Application.Run(); }
  }
}
