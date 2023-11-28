/*
 * File: Main.cs
 * Description: Entry Point of the program
 * Author: The group members
 */

using Westream;

public class Program
{
  public static async Task Main()
  {
    Console.WriteLine("Hello from we stream :)\n");

    // Create server instance
    Server westream = new Server();
    westream.start();
    await westream.listenAndServe();
  }
}
