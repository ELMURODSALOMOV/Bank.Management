//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

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
    Console.Clear();
    Console.WriteLine("=============== LogIn ================\n");
    User userLogIn = InputRegistrUserInfo();
    bool isThere = processingService.LogInUser(userLogIn);
    if(isThere is false)
    {
        Console.Clear();
        Console.WriteLine("================== Welcome to BANK =================\n");
        Console.WriteLine("============= Sign Up =============\n");
        User userSignUp = InputRegistrUserInfo();
        processingService.PostUser(userSignUp);
    }
}
else if (command == 2)
{
    BankProcessingService bankProcessingService = BankManagmentProject();
    Console.Clear();
    Console.WriteLine("============= Sign Up =============\n");
    User userSinnUp = InputRegistrUserInfo();
    User userInfo = bankProcessingService.PostUser(userSinnUp);
    if(userInfo.Name != userSinnUp.Name
        && userInfo.Password != userSinnUp.Password)
    {
        Console.WriteLine("=============== LogIn ================\n");
        User userLogIn = InputRegistrUserInfo();
        bankProcessingService.LogInUser(userLogIn);
    }
}

bool isContinue = true;
BankProcessingService bankProcessingService1 = BankManagmentProject();
do
{
    Console.WriteLine("================== Welcome to BANK =================\n");
    Console.WriteLine("1. Bank\n");
    Console.WriteLine("2. Client\n");
    Console.Write("Enter command: ");
    int command1 = Convert.ToInt32(Console.ReadLine());
    if (command1 == 1)
    {
        SelectBankFunction();
        Console.Write("Enter command: ");
        int commandBank = Convert.ToInt32(Console.ReadLine());
        if(commandBank == 1)
        {
            Console.Write("Enter the AccountNumber: ");
            decimal accountNumber = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            bankProcessingService1.PostDeposit(accountNumber, balance);
        }
        if(commandBank == 2)
        {
            Console.Write("Enter the accountNumberForBank: ");
            decimal accountNumberForBank = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            decimal balanceForBank = bankProcessingService1.GetMoney(accountNumberForBank, balance);
            Console.WriteLine(balanceForBank);
        }
        if(commandBank == 3)
        {
            Console.Write("Enter the accountNumberForBank: ");
            decimal accountNumberForBank = Convert.ToDecimal(Console.ReadLine());
            decimal balanceForBank = bankProcessingService1.GetBalance(accountNumberForBank);
            Console.WriteLine(balanceForBank);
        }
    }
    else if(command1 == 2)
    {
        SelectClientFunction();
        Console.Write("Enter command: ");
        int commandClient = Convert.ToInt32(Console.ReadLine());
        if(commandClient == 1)
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
        if(commandClient == 2)
        {
            Console.Write("Enter the firstAccountNumber: ");
            decimal firstAccountNumber = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the secondAccountNumber: ");
            decimal secondAccountNumber = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter the money: ");
            decimal money = Convert.ToDecimal(Console.ReadLine());
            bankProcessingService1.TransferMoneyBetweenAccountsForClient(firstAccountNumber, secondAccountNumber, money);
        }
        if(commandClient == 3)
        {
            Console.Write("Enter the AccountNumber: ");
            decimal accountNumber = Convert.ToDecimal(Console.ReadLine());
            bankProcessingService1.DeleteForClient(accountNumber);
        }
    }
   
    Console.Write("Is Continue ");
    string isCommand = Console.ReadLine();
    if (isCommand.ToLower().Contains("no") is true)
    {
        isContinue = false;
    }
    else if (isCommand.ToLower().Contains("yes") is true)
    {
        isContinue = true;
        Console.Clear();
    }
    else
    {
        isContinue = false;
        Console.WriteLine("The command was issued incorrectly.");
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
    Console.Clear();
    Console.WriteLine("================== Welcome to BANK =================\n");
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
    Console.Clear();
    Console.WriteLine("================ BANK =================\n");
    Console.WriteLine("1. Post Deposit For Bank");
    Console.WriteLine("2. Get Money For Bank");
    Console.WriteLine("3. Get balance For Bank");
}

static void SelectClientFunction()
{
    Console.Clear();
    Console.WriteLine("================== CLIENT =================\n");
    Console.WriteLine("1. Post For Client");
    Console.WriteLine("2. Transfer Money Between Accounts For Client");
    Console.WriteLine("3. Delete For Client");
}