/*
 * File: Server.cs
 * Description: Contains the program's server code to stream content
 * Author: <hearteric57@gmail.com>
 */

namespace Westream
{
  public class Constants
  {
    public const string APPNAME = "We stream";
    public const short PORT = 5000; // The port to be binded to
    public const short PACKET_SIZE =
        1024; // The packet size used for send and recieve
    public const short ACKNOWLEDGEMENT = 100;
    public const short USERNAME_SIZE = 60; // Default size for username
  }
}
