/*
 * File: Server.cs
 * Description: Contains the program's server code to stream content
 * Author: <hearteric57@gmail.com>
 */
using System;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace Westream
{
  public class Server
  {
    // Privates
    private TcpListener mServerInstance;

    /*
     * Initializes the server and starts it for listening
     * @params  nothing
     * returns nothing - just a running instance of the server
     */
    public Server()
    {
      Debug.WriteLine("[+] Attempting to instanciate server ..");
      mServerInstance = new TcpListener(IPAddress.Any, Constants.PORT);
      Debug.WriteLine("[+] Server setup success");
    }

    /*
     * Starts the server
     * @params nothing
     * returns nothing
     */
    public void start()
    {
      Debug.WriteLine("[+] Attempting to start server");
      mServerInstance.Start();
    }

    public void listenAndServe()
    {
      Debug.WriteLine("[+] Client Connected and Ready");
      TcpClient client = mServerInstance.AcceptTcpClient();
      NetworkStream ns = client.GetStream();

      // Echo back content
      while (client.Connected)
      {
        byte[] msg = new byte[Constants.PACKET_SIZE];
        ns.Read(msg, 0, msg.Length);
        ns.Write(msg, 0, msg.Length);
      }
    }
  }
}
