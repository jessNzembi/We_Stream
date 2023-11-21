using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csharpserver;

class Echo: WebSocketBehavior {
    protected override void OnMessage(MessageEventArgs e) {
        Console.WriteLine("Received Message From Client: " + e.Data);
        Send(e.Data);
    }
}

class Program{
    static void Main(string[] args) {
        WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

        wssv.Start();
        Console.WriteLine("Started WebSocket Server at 127.0.0.1:7890");

        // adding servives or behaviours as routes
        wssv.AddWebSocketService<Echo>("/Echo");

        Console.ReadKey();
        wssv.Stop();

        Console.WriteLine("Stopped the Server");
    }
}