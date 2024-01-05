using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace csharpclient;
// Client app is the one sending messages to a Server/listener.
// Both listener and client can send messages back and forth once a
// communication is established.
class SocketClient {
    public static void StartClient() {
        byte[] bytes = new byte[1024];

        try {
            // Connect to a Remote server
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses

            // convert this section to be dynamic user gives the values
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try {
                // Connect to Remote EndPoint
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.
                // while (true) {
                //     int bytesRec = sender.Receive(bytes);
                //     Console.WriteLine("Echoed test = {0}",
                //     Encoding.ASCII.GetString(bytes, 0, bytesRec));
                // }
                // Buffer to store received data.
                byte[] buffer = new byte[1024];
                int bytesReceived;

                // Write the received data to a file.
                using (FileStream fs = File.Create("receivedFile.mp3")) {
                    while ((bytesReceived = sender.Receive(buffer)) > 0) {
                        fs.Write(buffer, 0, bytesReceived);
                    }
                }
        
                // Release the socket.
                // sender.Shutdown(SocketShutdown.Both); // to be implemented as functions
                // sender.Close(); // to be implemented as functions

            }
            catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se) {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }
}