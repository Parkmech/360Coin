using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;


namespace _360Coin
{
    class Program
    {
        static void Main(string[] args)
        {

            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain threeSixtyCoin = new Blockchain(8, 100);

            Console.WriteLine("Start the Miner.");

            threeSixtyCoin.MinePendingTransactions(wallet1);

            Console.WriteLine("\nBalance of wallet is $" + threeSixtyCoin.GetBalanceOfWallet(wallet1).ToString());


            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            threeSixtyCoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the Miner.");
            threeSixtyCoin.MinePendingTransactions(wallet2);

            Console.WriteLine("\nBalance of wallet is $" + threeSixtyCoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet is $" + threeSixtyCoin.GetBalanceOfWallet(wallet2).ToString());


            string blockJSON = JsonConvert.SerializeObject(threeSixtyCoin, Formatting.Indented);

            Console.WriteLine(blockJSON);

            //This line will break the blockchain for testing purposes
            //threeSixtyCoin.GetLatestBlock().PreviousHash = "123434234";


            if (threeSixtyCoin.IsChainValid())
            {
                Console.WriteLine("Three Sixty Coin's Blockchain is valid!");
            }
            else
            {
                Console.WriteLine("Current line is NOT valid!");
            }
        }
    }
}
