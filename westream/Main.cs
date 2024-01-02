using System;
using csharpserver;
using csharpclient;

namespace ProgramEntry;

class ProgramEntry{
    static void Main(string[] args) {
        Console.WriteLine("Welcome to WeStream");
        Console.WriteLine("Press 1: To Create Channel\nPress 2: To Join Channel");
        string? user_input = Console.ReadLine();
        switch(user_input) {
            case "1":
                Console.WriteLine("Creating Server");
                Program.StartServer();
                break;
            case "2":
                Console.WriteLine("Joining Channel");
                SocketClient.StartClient();
                break;
            default:
                Console.WriteLine("Opps Wrong Choice");
                break;
        }
    }
}
