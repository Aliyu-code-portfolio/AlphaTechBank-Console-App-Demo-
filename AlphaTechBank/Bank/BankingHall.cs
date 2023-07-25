using AlphaTechBank.Models;
using AlphaTechBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTechBank.Bank
{
    public class BankingHall
    {
        private List<User> users;
        private List<Account> accounts;
        private AccountService accountService;
        private UserService userService;

        public BankingHall()
        {
            users = new();
            accounts = new();
            accountService = new(accounts);
            userService = new UserService(users);
        }

        public void StartBanking()
        {
            while (true)
            {
                Console.WriteLine("1. Login to your user account\n2. Create a new user account\n3. Print all account in the bank\n0. Exit\nChoose an option");
                bool isValid = byte.TryParse(Console.ReadLine(), out byte option);
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Try again\n");
                    continue;
                }
                switch (option)
                {
                    case 1:
                        string id = Login();
                        BankingApp(id);
                        break;
                    case 2:
                        Create();
                        break;
                    case 3:
                        PrintAllBankDetails();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Wrong input. Select 1, 2 or 0");
                        break;
                }
            }
        }


        private void Create()
        {
            Console.WriteLine("\n******** Create User Account *******\n");
            userService.CreateNewUser();
        }

        private string Login()
        {
            Console.WriteLine("\n******** Login *******\n");
            return userService.LoggedInUser();
        }

        private void CreateBankAccount(string id)
        {
            Console.WriteLine("\n******** Create Bank Account *******\n");
            accountService.CreateNewAccount(id);
        }
        private void PrintAllBankDetails()
        {
            Console.WriteLine("\nFULL NAME".PadRight(25)+"ACCOUNT NUMBER".PadRight(20)
                +"ACCOUNT TYPE".PadRight(20)+"ACCOUNT BALANCE".PadRight(10)
                );
            foreach(Account account in accounts)
            {
                Console.WriteLine(account.FirstName+" "+account.LastName.PadRight(18) 
                    + account.AccountNumber.PadRight(20)
                + account.AccountType.PadRight(20) + account.Balance
                );
            }
            Console.WriteLine("\n");
        }

        private void BankingApp(string id)
        {

            while (!string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Choose an account");
                Account[] userAccounts = accounts.Where(a => a.UserId == id).ToArray();
                int i = 1;
                while (i <= userAccounts.Length)
                {
                    Console.WriteLine($"{i}. {userAccounts[i - 1].AccountNumber}");
                    i++;
                }
                Console.WriteLine($"{i}. Create a new bank account");
                Console.WriteLine($"0. Log out");
                bool isValid = byte.TryParse(Console.ReadLine(), out byte accountNumber);
                if (!isValid || accountNumber > i || accountNumber < 0)
                {
                    Console.WriteLine("Invalid input");
                }
                else if (accountNumber == i)
                {
                    CreateBankAccount(id);
                }
                else if (accountNumber == 0)
                {
                    return;
                }
                else
                {
                    BankingFeateures(userAccounts[i - 2]);
                }
            }
            Console.WriteLine("Log in or create an user account");
        }
       private void BankingFeateures(Account account)
        {
            while (true)
            {
                Console.WriteLine("1. Check balance\n2. Make Deposit\n3. Make withdrawal\n4. Make a transfer\n5. Account Statement\n0. Exit");
                bool isValid = byte.TryParse(Console.ReadLine(), out byte option);
                if (!isValid || option < 0)
                    continue;
                switch (option)
                {
                    case 1:
                        accountService.CheckBalance(account.AccountNumber);
                        break;
                    case 2:
                        accountService.DepositFunds(account.AccountNumber);
                        break;
                    case 3:
                        accountService.WithdrawFunds(account.AccountNumber);
                        break;
                    case 4:
                        accountService.TransferFunds(account.AccountNumber);
                        break;
                    case 5:
                        accountService.PrintAccountStatement(account.AccountNumber);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

    }
}
