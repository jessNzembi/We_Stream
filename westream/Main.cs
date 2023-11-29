/*
 * File: Main.cs
 * Description: Entry Point of the program
 * Author: The group members
 */

using Westream;

public class Program
{
  public static void Main(String[] args)
  {
    Console.WriteLine("Hello from we stream :)\n");

    // Create server instance
    // Server westream = new Server();
    // westream.start();
    // await westream.listenAndServe();
    WestreamGui gui = new WestreamGui();
    gui.Run();
  }
}
