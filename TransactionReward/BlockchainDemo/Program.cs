using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain phillyCoin = new Blockchain();
            phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 10));
            phillyCoin.ProcessPendingTransactions("Bill");

            //phillyCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 5));
            //phillyCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 5));
            //phillyCoin.ProcessPendingTransactions("Bill");

            Console.WriteLine("=========================");
            Console.WriteLine($"Henry' balance: {phillyCoin.GetBalance("Henry")}");
            Console.WriteLine($"MaHesh' balance: {phillyCoin.GetBalance("MaHesh")}");
            Console.WriteLine($"Bill' balance: {phillyCoin.GetBalance("Bill")}");

            Console.WriteLine("=========================");
            Console.WriteLine($"phillyCoin");
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));

             Console.ReadKey();
        }
    }
}
