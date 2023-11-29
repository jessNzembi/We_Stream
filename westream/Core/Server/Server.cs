/*
 * File: Server.cs
 * Description: Contains the program's server code to stream content
 * Author: <hearteric57@gmail.com>
 */
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Text.Json;

namespace Westream
{
  public class Server
  {
    Dictionary<string, TcpClient> mClients;
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
      mClients = new Dictionary<String, TcpClient>();
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

    public async Task handleNewClients()
    {
      if (mServerInstance.Pending())
      {
        // Accept a client
        TcpClient client = mServerInstance.AcceptTcpClient();

        // Read the username
        NetworkStream ns = client.GetStream();
        var msg = new byte[Constants.PACKET_SIZE];
        var bytesRead = await ns.ReadAsync(msg, 0, msg.Length);

        string jsonString = System.Text.Encoding.UTF8.GetString(msg);
        var message = JsonSerializer.Deserialize<Message>(jsonString);

        // Append the user to the clientm
        mClients.Add(message?.text!, client);
        Message okMsg = new Message();
        okMsg.type = MessageType.Ping;
        okMsg.text = $"Hi, {message?.text!.Trim()}";

        msg = System.Text.Encoding.Default.GetBytes(
            JsonSerializer.Serialize(okMsg));
        await ns.WriteAsync(msg, 0, msg.Length);

        // Alert the others
        Message pingMsg = new Message();
        pingMsg.type = MessageType.Ping;
        pingMsg.text = $"Say Hi to, {message?.text!.Trim()}";

        foreach (var c in mClients)
        {
          if (!c.Value.Equals(c))
          {
            msg = System.Text.Encoding.Default.GetBytes(
                JsonSerializer.Serialize(pingMsg));
            await c.Value.GetStream().WriteAsync(msg, 0, msg.Length);
          }
        }
        Debug.WriteLine("[+] Client Connected and Ready");
      }
    }

    public async Task handleRequests()
    {
      foreach (var client in mClients)
      {

        var stream = client.Value.GetStream();

        if (stream.DataAvailable)
        {
          var msg = new byte[Constants.PACKET_SIZE];
          stream.Read(msg, 0, msg.Length);

          string jsonString = System.Text.Encoding.UTF8.GetString(msg);
          var message = JsonSerializer.Deserialize<Message>(jsonString);

          if (message?.type == MessageType.Ping)
          {

            foreach (var c in mClients)
            {
              var netStream = client.Value.GetStream();
              await netStream.WriteAsync(msg, 0, msg.Length);
            }
          }
        }
      }
    }

    public async Task listenAndServe()
    {
      while (true)
      {
        await handleNewClients();
        await handleRequests();
      }
    }
  }
}
