using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csharpserver;

class Program{
    static void Main(string[] args) {
        WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

        wssv.Start();
        Console.WriteLine("Started WebSocket Server at 127.0.0.1:7890");

        Console.ReadKey();
        wssv.Stop();

        Console.WriteLine("Stopped the Server");
    }
}