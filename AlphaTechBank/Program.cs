using AlphaTechBank.Models;
using AlphaTechBank.Services;

List<User> users = new();
List<Account> accounts = new();
AccountService accountService = new(accounts);
UserService userService = new(users);
while (true)
{
    Console.WriteLine("1. Login to your user account\n2. Create a new user account\n0. Exit\nChoose an option");
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
        case 0:
            return;
        default:
            Console.WriteLine("Wrong input. Select 1, 2 or 0");
            break;
    }
}

void BankingApp(string id)
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

void BankingFeateures(Account account)
{
    while(true)
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
            default: Console.WriteLine("Invalid input");
                break;
        }
    }
}

void Create()
{
    Console.WriteLine("\n******** Create User Account *******\n");
    userService.CreateNewUser();
}

string Login()
{
    Console.WriteLine("\n******** Login *******\n");
    return userService.LoggedInUser(); 
}

void CreateBankAccount(string id)
{
    Console.WriteLine("\n******** Create Bank Account *******\n");
    accountService.CreateNewAccount(id);
}