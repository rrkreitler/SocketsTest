using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SockTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = string.Empty;

            do
            {
                Console.Write("Message to send: ");
                msg = Console.ReadLine();
                SynchronousSocketClient.SendMessage(msg);

            } while (!string.IsNullOrWhiteSpace(msg));

            


            Console.Write("\nHit any key...");
            Console.ReadKey();
        }
    }
}
