//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Services.BankProcessings
{
    internal interface IBankProcessingService
    {
        string GetAllClient();
        decimal GetBalanceClient(decimal accountNumber);
        User PostUser(User user);
        bool LogInUser(User user);
        bool PostDeposit(decimal accountNumberForBank, decimal balance);
        decimal GetMoney(decimal accountNumberForBank, decimal balance);
        decimal GetBalance(decimal accountNumberForBank);
        bool PostForClient(Customer customer);
        bool DeleteForClient(decimal accountNumber);
        bool TransferMoneyBetweenAccountsForClient(decimal firstAccountNumber, decimal secondAccountNumber, decimal money);
    }
}
