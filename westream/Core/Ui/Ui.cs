using Gtk;

namespace Westream
{
  public class WestreamGui
  {
    private Window mWindowInstance;

    public WestreamGui()
    {
      Application.Init();
      mWindowInstance = new Window(Constants.APPNAME);
      mWindowInstance.DeleteEvent += (o, e) => Application.Quit();

      var button = new Button("Click me");
      button.Clicked += (o, e) => Console.WriteLine("Button clicked");

      mWindowInstance.Add(button);
      mWindowInstance.ShowAll();
    }

    public void Run() { Application.Run(); }
  }
}
