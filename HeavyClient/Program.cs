using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using HeavyClient.RoutingService;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace HeavyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RoutingServiceClient routingServiceClient = new RoutingServiceClient();
            Contract[] contractNames = JsonSerializer.Deserialize<Contract[]>(routingServiceClient.GetContracts());

            string welcome_message =
                "Welcome to this terminal !\n" +
                "This terminal is heavyclient to a JCDecaux and OpenRoute web service.\n\n" +
                "---------------\n\n" +
                "Type \"path\" to use path service.\n\n" +
                "Type \"contracts\" to view contracts name.\n\n" +
                "Type \"contractStatus\" to create an excel file with the stations of a specific contract.\n\n" +
                "---------------\n\n" +
                "Type \"help\" to display this message again.\n" +
                "Type \"exit\" at any point to exit.\n";
            Console.WriteLine(welcome_message);
            string input = "";
            while (input != "exit")
            {
                Console.Write("$");
                input = Console.ReadLine();
                switch (input)
                {
                    case "path":
                        Path();
                        break;

                    case "contracts":
                        Contracts(contractNames);
                        break;

                    case "contractStatus":
                        ContractStatus(contractNames);
                        break;

                    case "help":
                        Help(welcome_message);
                        break;

                    case "exit":
                        break;
         
                    default:
                        Console.WriteLine("Unknown command - Type \"help\" to view all availables command");
                        break;
                }
            }
        }

        private static void ContractStatus(Contract[] contracts)
        {
            Console.WriteLine("Write the name of the contract you want to inspect.");
            Console.Write("$");
            string input = Console.ReadLine().ToLower();
            Boolean isValid = false;
            foreach (Contract contract in contracts)
            {
                if (contract.name == input) {
                    isValid = true;
                    break;
                }
            }
            if (isValid)
            {
                RoutingServiceClient routingServiceClient = new RoutingServiceClient();
                StationStatus[] statuses = JsonSerializer.Deserialize<StationStatus[]>(routingServiceClient.GetStationsStatusFromContract(input));
                Console.WriteLine(JsonSerializer.Serialize<StationStatus[]>(statuses)); //temporary

                // Excel time
                Excel.Application myexcelApplication = new Excel.Application();
                if (myexcelApplication != null)
                {
                    Excel.Workbook myexcelWorkbook = myexcelApplication.Workbooks.Add();
                    Excel.Worksheet myexcelWorksheet = (Excel.Worksheet)myexcelWorkbook.Sheets.Add();
                    int i = 2;
                    myexcelWorksheet.Cells[1, 1] = "name";
                    myexcelWorksheet.Cells[1, 2] = "address";
                    myexcelWorksheet.Cells[1, 3] = "status";
                    myexcelWorksheet.Cells[1, 4] = "bikes";
                    myexcelWorksheet.Cells[1, 5] = "stands";
                    foreach (StationStatus stationStatus in statuses)
                    {
                        myexcelWorksheet.Cells[i, 1] = stationStatus.name;
                        myexcelWorksheet.Cells[i, 2] = stationStatus.address;
                        myexcelWorksheet.Cells[i, 3] = stationStatus.status;
                        myexcelWorksheet.Cells[i, 4] = stationStatus.totalStands.availabilities.bikes;
                        myexcelWorksheet.Cells[i, 5] = stationStatus.totalStands.availabilities.stands;
                        i++;
                    }
                    myexcelApplication.ActiveWorkbook.SaveAs(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + input + ".xls", Excel.XlFileFormat.xlWorkbookNormal);
                    myexcelWorkbook.Close();
                    myexcelApplication.Quit();
                    Console.WriteLine("\nExcel file generated successfully for contract " + input + "\nLocation: " + (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + input + ".xls\n"));
                }
            } else {
                Console.WriteLine("Wrong contract name, type \"contracts\" to view contracts names");
            }
        }

        private static void Contracts(Contract[] contracts)
        {
            foreach (Contract contract in contracts)
            {
                Console.WriteLine(contract.name);
            }
        }

        private static void Help(string message)
        {
            Console.WriteLine(message);
        }

        private static void Path()
        {
            RoutingServiceClient routingServiceClient = new RoutingServiceClient();
            Console.WriteLine("\nType one address, then press Enter.\n" + "Type a second address, then press Enter.");
            string address1 = null;
            string address2 = null;
            string res = null;
            Console.WriteLine("Write your starting address:");
            Console.Write("$");
            address1 = Console.ReadLine();
            Console.WriteLine("Write your goal address:");
            Console.Write("$");
            address2 = Console.ReadLine(); 
            try {
                res = routingServiceClient.GetPaths(address1, address2);
                Console.WriteLine(res);
            } catch (Exception e)
            {
                Console.WriteLine("One address cannot be located or the starting address is not reachable by bike, please try again with another syntax");
            }
        }
    }
}
