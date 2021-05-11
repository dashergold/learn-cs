using System;
using System.Collections.Generic;

namespace Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            var accs=Accounts.loadAccounts("accounts.txt");
            
            ShowMenu();
            var validCommand = GetChoice();
            DispatchCommand(validCommand, accs);
            // DeleteAccount();
            // CreateAccount();
            // Quit();

        }

        private static void DispatchCommand(string validCommand, Dictionary<string, Account> accounts)
        {
            if (String.Compare(validCommand, "C", ignoreCase:true)==0)
            {
                var acc = CreateAccount();
                accounts.Add(acc.Username, acc);
            }
        }

        private static Account CreateAccount()
        {
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Balance: ");
            var balance = Console.ReadLine();
            var acc = new Account();
            acc.Username = username;
            acc.Password =  password;
            acc.Name = name;
            acc.Balance = Convert.ToDecimal(balance);
            return acc;    
        }
        

        private static string GetChoice()
        {
            var command = Console.ReadLine();
            
            while (!isValidCommand(command))
            {
                Console.WriteLine("invalid command");
                ShowMenu();
                command=Console.ReadLine();
            }
            return command;
        }

        private static bool isValidCommand(string command)
        {
            if (string.Compare(command, "L", ignoreCase:true)==0)
            {
                return true;
            }
             if (string.Compare(command, "C", ignoreCase:true)==0)
            {
                return true;
            }
             if (string.Compare(command, "D", ignoreCase:true)==0)
            {
                return true;
            }
             if (string.Compare(command, "Q", ignoreCase:true)==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("L - list users");
            Console.WriteLine("C - Create an account");
            Console.WriteLine("D - delete an account");
            Console.WriteLine("Q - quit");
        }
    }
}
