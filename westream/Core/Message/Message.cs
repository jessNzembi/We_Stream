/*
 * File: Message.cs
 * Description: Contains the program's Message types
 * Author: <hearteric57@gmail.com>
 */

namespace Westream
{
  class Message
  {
    public MessageType type;
    public String? text;
    public byte[]? data;
  }

  enum MessageType
  {
    Add,
    Ping,
    Command,
    Text,
    File,
  }
}
