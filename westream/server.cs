using System;
using System.Net;
using System.Text;
// using WebSocketSharp;
// using WebSocketSharp.Server;
using System.Net.Sockets;

namespace csharpserver;

// class Echo: WebSocketBehavior {
//     protected override void OnMessage(MessageEventArgs e) {
//         Console.WriteLine("Received Message From Client: " + e.Data);
//         Send(e.Data);
//     }
// }

// class EchoAll: WebSocketBehavior {
//     protected override void OnMessage(MessageEventArgs e) {
//         Console.WriteLine("Received Message From EchoAll Client: " + e.Data);
//         Sessions.Broadcast(e.Data);
//     }
// }

class Program{
    // static void Main(string[] args) {
    //     WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

    //     wssv.Start();
    //     Console.WriteLine("Started WebSocket Server at 127.0.0.1:7890");

    //     // adding servives or behaviours as routes
    //     wssv.AddWebSocketService<Echo>("/Echo");
    //     wssv.AddWebSocketService<EchoAll>("/EchoAll");

    //     Console.ReadKey();
    //     wssv.Stop();

    //     Console.WriteLine("Stopped the Server");
    // }

    public static void StartServer() {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        try {
            // Create a Socket that will use Tcp protocol
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // A Socket must be associated with an endpoint using the Bind method
            listener.Bind(localEndPoint);

            // loop used to keep the connection alive
            while(true){
                 // puts the socket in a listening mode
                listener.Listen();

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                while (true) {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg); // implement broadcasting here
            }
           

            // handler.Shutdown(SocketShutdown.Both); // disables sends and receives from on a Socket
            // handler.Close(); // Closes the connection to be implemented as a different method
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\n Press any key to continue...");
        Console.ReadKey();
    }
}