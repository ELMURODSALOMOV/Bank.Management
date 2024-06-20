//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Brokers.Storages;
using Bank.Management.Console.Brokers.Storages.BankStorage;
using Bank.Management.Console.Brokers.Storages.RegistrStorage;
using Bank.Management.Console.Models;
using Bank.Management.Console.Services.BankProcessings;
using Bank.Management.Console.Services.Foundations.Banks;
using Bank.Management.Console.Services.Foundations.Customers;
using Bank.Management.Console.Services.Foundations.Registrs;
using Bank_management.Services.Foundation.Banks.Customers;

SelectDBMenu();
Console.Write("Enter command: ");
int command = Convert.ToInt32(Console.ReadLine());

if(command == 1)
{
    BankProcessingService processingService = BankManagmentProject();
    User userLogIn = InputRegistrUserInfo();
    bool isThere = processingService.LogInUser(userLogIn);
    if(isThere is false)
    {
        User userSignUp = InputRegistrUserInfo();
        processingService.PostUser(userSignUp);
    }
    else
    {
        SelectBankFunction();
    }
}
else if (command == 2)
{
    BankProcessingService bankProcessingService = BankManagmentProject();
    User userSinnUp = InputRegistrUserInfo();
    User userInfo = bankProcessingService.PostUser(userSinnUp);
    if(userInfo == userSinnUp)
    {
        bankProcessingService.LogInUser(userInfo);
    }
    else
    {
        SelectBankFunction();
    }
}

bool isContinue = true;
BankProcessingService bankProcessingService1 = BankManagmentProject();
do
{
    Console.Write("Enter command: ");
    int commands = Convert.ToInt32(Console.ReadLine());
    
    if(commands == 1)
    {
        Console.Write("Enter the AccountNumber: ");
        decimal accountNumber = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter the balance: ");
        decimal balance = Convert.ToDecimal(Console.ReadLine());
        bankProcessingService1.PostDeposit(accountNumber, balance);
    }
    if(commands == 2)
    {
        Console.Write("Enter the AccountNumber: ");
        decimal accountNumber = Convert.ToDecimal(Console.ReadLine());
        bankProcessingService1.DeleteForClient(accountNumber);
    }
    if(commands == 3)
    {
        Console.Write("Enter the firstAccountNumber: ");
        decimal firstAccountNumber = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter the secondAccountNumber: ");
        decimal secondAccountNumber = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter the money: ");
        decimal money = Convert.ToDecimal(Console.ReadLine());
        bankProcessingService1.TransferMoneyBetweenAccountsForClient(firstAccountNumber, secondAccountNumber, money);
    }
    if(commands == 4)
    {
        Console.Write("Enter the accountNumberForBank: ");
        decimal accountNumberForBank = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter the balance: ");
        decimal balance =   Convert.ToDecimal(Console.ReadLine());
        decimal balanceForBank = bankProcessingService1.GetMoney(accountNumberForBank, balance);
        Console.WriteLine(balanceForBank);
    }
    if(commands == 5)
    {
        Console.Write("Enter the accountNumberForBank: ");
        decimal accountNumberForBank = Convert.ToDecimal(Console.ReadLine());
        decimal balanceForBank = bankProcessingService1.GetBalance(accountNumberForBank);
        Console.WriteLine(balanceForBank);
    }
    if(commands == 6)
    {
        Customer customer = new Customer();
        Console.Write("Enter the Name: ");
        customer.Name = Console.ReadLine();
        Console.Write("Enter the accountNumber: ");
        customer.AccountNumber = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter the balance: ");
        customer.Balance = Convert.ToDecimal(Console.ReadLine());
        bankProcessingService1.PostForClient(customer);
    }
} while (isContinue is true);

static BankProcessingService BankManagmentProject()
{
    IRegistrService registrService = new RegistrService();
    IBankService bankService = new BankService();
    ICustomerService customerService = new CustomerService();

    BankProcessingService bankProcessingService = 
        new BankProcessingService(registrService, bankService, customerService);

    return bankProcessingService;
}

static void SelectDBMenu()
{
    Console.WriteLine("================== Welcome to BANK =================");
    Console.WriteLine("1. LogIn. ");
    Console.WriteLine("2. SignUp. ");
}

static User InputRegistrUserInfo()
{
    User user = new User();
    Console.Write("Enter the Name: ");
    user.Name = Console.ReadLine();
    Console.Write("Enter the password: ");
    user.Password = (Console.ReadLine());

    return user;
}

static void SelectBankFunction()
{
    Console.WriteLine("1. Post Deposit");
    Console.WriteLine("2. Delete Account");
    Console.WriteLine("3. Transfer Money Between Accounts For Client");
    Console.WriteLine("4. Get Money");
    Console.WriteLine("5. Get balance");
    Console.WriteLine("6. Post For Client");
}