using AlphaTechBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlphaTechBank.Services
{
    public class AccountService
    {
        public List<Account> Accounts;
        public AccountService(List<Account> accounts)
        {
            Accounts = accounts;
            Accounts.Add(new Account()
            {
                FirstName = "Aliyu",
                LastName = "Abdullahi",
                UserId = "DD44-445KIG",
                Email = "aliyu@gmail.com",
                TransactionPin = 3445,
                PhoneNumber = "1234567890",
                AccountNumber = "1234567890",
                AccountType="savings",
                Balance=56009
            });
            Accounts.Add(new Account()
            {
                FirstName = "Kalid",
                LastName = "Miracle",
                UserId = "DD44223-45KIG",
                Email = "kalid@gmail.com",
                TransactionPin = 3445,
                PhoneNumber = "1234567890",
                AccountNumber = "1236567890",
                AccountType = "current",
                Balance = 800
            });
        }

        public void CheckBalance(string accountNumber)
        {
            Account account = Accounts.Where(a=>a.AccountNumber == accountNumber).FirstOrDefault();
            Console.WriteLine($"\nAccount Number: {accountNumber}");
            Console.WriteLine($"Account Balance: {account.Balance}");
        }
        public void CreateNewAccount(string id)
        {
            Console.WriteLine("Enter your first and last name");
            string[] names = Console.ReadLine().Trim().Split(' ');
            if (names.Length < 2)
            {
                Console.WriteLine("Invalid. Enter first name and last name");
                return;
            }
            bool isValid = false;
            EmailAddressAttribute emailAddressAttribute = new();
            string email;
            do
            {
                Console.WriteLine("Enter a valid email address");
                email = Console.ReadLine();
                isValid = emailAddressAttribute.IsValid(email);
            }
            while (!isValid);
            Console.WriteLine("Enter your phone number");
            long.TryParse(Console.ReadLine(), out long phoneNumber);
            long accountNumber=0;
            while(Accounts.Where(a=>a.AccountNumber==accountNumber.ToString()).Any()||accountNumber==0)
            {
                accountNumber = new Random().Next(1122222223, 1992834584);
            }
            string accountType = "";
            do
            {
                Console.WriteLine("Enter account type. Format: S or C / savings or current");
                char ans = Console.ReadLine().Trim().ToLower().ToCharArray()[0];
                if (ans == 's' || ans == 'c')
                {
                    if (ans == 's' && !Accounts.Where(a => a.UserId == id && a.AccountType == "saving").Any())
                    {
                        Console.WriteLine("Cannot have more than one saving account");
                    }
                    else
                    {
                        accountType = ans == 's' ? "savings" : "current";
                    }
                }
            }
            while (string.IsNullOrWhiteSpace(accountType));
            Console.WriteLine("Enter your 4 digits transaction pin\n");
            bool isPin = int.TryParse(Console.ReadLine(), out int pin);
            if(!isPin)
            {
                Console.WriteLine("Invalid PIN try again later");
                return;
            }
            Account account = new()
            {
                UserId = id,
                FirstName = names[0],
                LastName = names[1],
                AccountNumber = accountNumber.ToString(),
                AccountType = accountType,
                PhoneNumber = phoneNumber.ToString(),
                Email = email
            };
            Accounts.Add(account);
        }
        public void DepositFunds(string accountNumber)
        {
            Account account = Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            Console.WriteLine("Please enter a depositorName");
            string depositorName = Console.ReadLine();
            Console.WriteLine("Please enter the amount to transfer");
            bool convertableAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (convertableAmount)
            {
                if (amount < 1)
                    Console.WriteLine("Cannot deposit less than 1 naira");
                else
                {
                    account.Balance += amount;
                    account.Transations.Add(new Transations()
                    {
                        AccountNumber = accountNumber,
                        TransactionType = "deposit",
                        Amount = amount,
                        Description = depositorName
                    });
                    Console.WriteLine($"Dear customer, {amount} has been successfully been credited into your account {account.AccountNumber} by {depositorName}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount entered");
            }
        }
        public void WithdrawFunds(string accountNumber)
        {
            Account account = Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            Console.WriteLine("Please enter the amount to withdraw");
            bool convertableAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (convertableAmount)
            {
                if (amount < 1)
                    Console.WriteLine("Cannot withdraw less than 50 naira");
                else
                {
                    Console.WriteLine("Enter your pin");
                    bool convertable = int.TryParse(Console.ReadLine(), out int pinEntered);
                    if (convertable && account.TransactionPin == pinEntered)
                    {
                        if (account.Balance - amount < account.minBalance)
                        {
                            Console.WriteLine("Insufficient funds");
                        }
                        else
                        {
                            account.Transations.Add(new Transations()
                            {
                                AccountNumber = accountNumber,
                                TransactionType = "withdrawal",
                                Amount = amount
                            });
                            account.Balance -= amount;
                            Console.WriteLine($"Dear customer, {amount} has been successfully been debited from your account {account.AccountNumber}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect pin entered");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid amount");
            }
        }
        public void TransferFunds(string accountNumber)
        {
            Account account = Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
            Console.WriteLine("Please enter the receiver account number");
            string receiverAccountNumber = Console.ReadLine();
            Console.WriteLine("Please enter a description");
            string description = Console.ReadLine();
            Console.WriteLine("Please enter the amount to transfer");
            bool convertableAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
            if (long.TryParse(receiverAccountNumber, out long _) && convertableAmount)
            {
                if (amount < 1)
                    Console.WriteLine("Cannot Transfer less than 50 naira");
                else
                {
                    Console.WriteLine("Enter your pin");
                    bool convertable = int.TryParse(Console.ReadLine(), out int pinEntered);
                    if (convertable && account.TransactionPin == pinEntered)
                    {
                        if (account.Balance - amount < account.minBalance)
                        {
                            Console.WriteLine("Insufficient funds");
                        }
                        else
                        {
                            Account receiver = Accounts.Where(a => a.AccountNumber == receiverAccountNumber).FirstOrDefault();
                            if (receiver == null||receiver.AccountNumber==accountNumber)
                            {
                                Console.WriteLine("Account number not found");
                            }
                            else
                            {
                                account.Transations.Add(new Transations()
                                {
                                    AccountNumber = accountNumber,
                                    TransactionType = "transfer",
                                    Amount = amount,
                                    ReceiverName = $"{receiver.FirstName} {receiver.LastName}",
                                    ReceiverAccountNumber = receiverAccountNumber,
                                    Description = description
                                });
                                account.Balance -= amount;
                                receiver.Balance += amount;
                                receiver.Transations.Add(new Transations()
                                {
                                    TransactionType = "credit transfer",
                                    Amount = amount,
                                    Description = $"Credited from {account.FirstName} {account.LastName}. {description}"
                                });
                                Console.WriteLine($"Dear customer, {amount} has been successfully been transfer from your account.  Transfer to {receiver.FirstName}\nDescription: {description}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect pin entered");
                    }
                }
            }
            else { Console.WriteLine("Invalid inputs provided"); }
        }
        public void PrintAccountStatement(string accountNumber)
        {
            List<Transations> transactions = Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault().Transations;
            Console.WriteLine($"ACCOUNT STATEMENT FOR {accountNumber}\n");
            foreach (Transations transaction in transactions)
            {
                Console.WriteLine($"Transaction type: {transaction.TransactionType}\nReceiver account number: " +
                    $"{transaction.ReceiverAccountNumber}\nReceiver name: {transaction.ReceiverName}\n" +
                    $"Amount: {transaction.Amount}\nDescription: {transaction.Description}\nDate: {transaction.DateCompleted}\n\n");
            }
        }
    }
}
