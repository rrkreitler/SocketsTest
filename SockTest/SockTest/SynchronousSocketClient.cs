using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SockTest
{
    public class SynchronousSocketClient
    {
        public static void SendMessage(string msgToSend)
        {
            if (string.IsNullOrWhiteSpace(msgToSend))
            {
                msgToSend = "EndCall<EOF>";
            }

            //msgToSend += "<EOF>";

            // Buffer for incoming data
            byte[] bytes = new byte[1024];

            // Connect to remote device
            try
            {
                // Establish the remote endpoint for the socket.
                // this example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create TCP/IP socket.
                Socket sender = new Socket(
                    ipAddress.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                // Connect the socket to the remote endpoint and catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine($"Socket connected to {sender.RemoteEndPoint.ToString()}");

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(msgToSend);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Recieve the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine($"Echoed test = {Encoding.ASCII.GetString(bytes, 0, bytesRec)}");

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine($"ArgumentNullException: {ane.ToString()}");
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"SocketException: {se.ToString()}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected Exception: {e.ToString()}");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
