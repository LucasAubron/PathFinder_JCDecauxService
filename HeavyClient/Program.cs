using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeavyClient.RoutingService;

namespace HeavyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Try it with (for example):
            //startingPos = 80 Boulevard Françoise Duparc, 13004 Marseille, France
            //endingPos = 2 Grand Rue, 13002 Marseille, France
            string welcome_message =
                "Welcome to this terminal\n" +
                "This terminal is heavyclient to a service that computes a path you can take by using JCDecaux bike services.\n\n" +
                "Type one address, then press Enter\n" +
                "Type a second address, then press Enter\n\n" +
                "---------------\n\n" +
                "Type \"exit\" at any point to exit.\n";
            Console.WriteLine(welcome_message);
            RoutingServiceClient routingServiceClient = new RoutingServiceClient();
            string address1 = null;
            string address2 = null;
            string res = null;
            while (address1 != "exit" && address2!="exit")
            {
                Console.WriteLine("Write your starting address:");
                address1 = Console.ReadLine();
                if (address1 == "exit")
                {
                    break;
                }
                Console.WriteLine("Write your goal address:");
                address2 = Console.ReadLine();
                if (address2 == "exit")
                {
                    break;
                }
                res = routingServiceClient.GetPaths(address1, address2);
                Console.WriteLine(res);
            }
            
        }
    }
}
