using System.Net.Sockets;
using System.Diagnostics;
using System.Text.Json;

namespace Westream
{
  public class Client
  {
    String mUsername;
    String mServerAddress;
    TcpClient mClientInstance;
    bool mIsconnected;

    public Client(String username, String address)
    {
      Debug.WriteLine("[*] Attempting to create client");
      mUsername = username;
      mServerAddress = address;
      mClientInstance = new TcpClient();
      mClientInstance.Connect(mServerAddress, Constants.PORT);
      Debug.WriteLine("[+] Client created and instanciated successfully");

      SendJoinDetails();
    }

    public void SendJoinDetails()
    {
      Message message = new Message();
      message.text = mUsername;
      var ns = mClientInstance.GetStream();

      var msg = System.Text.Encoding.Default.GetBytes(
          JsonSerializer.Serialize(message));

      ns.Write(msg, 0, msg.Length);

      Debug.WriteLine("[+] Client Connected!");
    }
  }
}
