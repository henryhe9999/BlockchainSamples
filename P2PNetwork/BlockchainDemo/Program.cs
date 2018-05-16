using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        public static int Port = 0;
        public static P2PServer Server = null;
        public static P2PClient Client = new P2PClient();
        public static Blockchain PhillyCoin = new Blockchain();
        public static string name = "Unknown";

        static void Main(string[] args)
        {
            PhillyCoin.InitializeChain();

            if (args.Length >= 1)
                Port = int.Parse(args[0]);
            if (args.Length >= 2)
                name = args[1];

            if (Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            if (name != "Unkown")
            {
                Console.WriteLine($"Current user is {name}");
            }

            int selection = 0;
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Please enter the server URL (enter 0 to cancel the operation)");
                        string serverURL = Console.ReadLine();
                        if (serverURL == "0")
                            break;
                        Client.Connect($"{serverURL}/Blockchain");
                        break;
                    case 2:
                        Console.WriteLine("Please enter the receiver name (enter 0 to cancel the operation)");
                        string receiverName = Console.ReadLine();
                        if (receiverName == "0")
                            break;
                        Console.WriteLine("Please enter the amount (enter 0 to cancel the operation)");
                        string amount = Console.ReadLine();
                        if (amount == "0")
                            break;
                        PhillyCoin.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                        PhillyCoin.ProcessPendingTransactions(name);
                        Client.Broadcast(JsonConvert.SerializeObject(PhillyCoin));
                        break;
                    case 3:
                        Console.WriteLine("Blockchain");
                        Console.WriteLine(JsonConvert.SerializeObject(PhillyCoin, Formatting.Indented));
                        break;

                }

                Console.WriteLine("=========================");
                Console.WriteLine("1. Connect to a server");
                Console.WriteLine("2. Add a transaction");
                Console.WriteLine("3. Display Blockchain");
                Console.WriteLine("4. Exit");
                Console.WriteLine("=========================");
                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }

            if (Client != null)
            {
                try
                {
                    Client.Close();
                }
                finally
                {
                    Client = null;
                }
            }
        }
    }
}
