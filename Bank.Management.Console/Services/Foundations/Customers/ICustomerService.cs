//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Services.Foundations.Customers
{
    internal interface ICustomerService
    {
        bool CreateClient(Customer customer);
        bool DeleteClient(decimal accountNumber);

        bool TransferMoneyBetweenClients(
            decimal firstAccountNumber,
            decimal secondAccountNumber,
            decimal money);
    }
}
