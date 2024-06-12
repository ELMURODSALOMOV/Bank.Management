//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Brokers.Storages.Bank.Customers
{
    internal class CustomerBroker : ICustomerBroker
    {
        public bool CloseAccountNumberForClient(decimal accountNumber)
        {
            throw new NotImplementedException();
        }

        public bool CreateAccountNumberForClient(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool TransferMoneyBetweenAccounts(decimal firstAccountNumber, decimal secondAccountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
