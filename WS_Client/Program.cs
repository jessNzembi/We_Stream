using  WebSocketSharp;

class Program {
    static void Main(string[] args) {
        using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/EchoAll")) {
            ws.OnMessage += Ws_OnMessage;

            ws.Connect();
            ws.Send("Hello From this Client");

            Console.ReadKey();
        }
    }

    private static void Ws_OnMessage(object sender, MessageEventArgs e) {
        Console.WriteLine("Received from the server: " + e.Data);
    }
}