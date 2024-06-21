//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Brokers.Loggings;
using Bank.Management.Console.Brokers.Storages.BankStorage.Customers;
using Bank.Management.Console.Models;
using Bank.Management.Console.Services.Foundations.Customers;

namespace Bank_management.Services.Foundation.Banks.Customers
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerBroker customerBroker;
        private readonly ILoggingBroker loggingBroker;

        public CustomerService()
        {
            this.loggingBroker = new LoggingBroker();
            this.customerBroker = new CustomerBroker();
        }

        public bool CreateClient(Customer customer)
        {
            return customer is null
                ? InvalidCreateClient()
                : ValidationAndCreateClient(customer);
        }

        public bool DeleteClient(decimal accountNumber)
        {
            return accountNumber < 0 && accountNumber is 0
                ? InvalidDeleteClient()
                : ValidationAndDeleteClient(accountNumber);
        }

        public bool TransferMoneyBetweenClients(
            decimal firstAccountNumber,
            decimal secondAccountNumber,
            decimal money)
        {
            return (firstAccountNumber < 0 || firstAccountNumber is 0)
                  && (secondAccountNumber < 0 || secondAccountNumber is 0)
                  && (money < 0 || money is 0)
                  ? InvalidTransferMoneyBetweenClients()
                  : ValidationAndTransferMoneyBetweenClients(
                                    firstAccountNumber,
                                    secondAccountNumber,
                                    money);

        }

        private bool ValidationAndTransferMoneyBetweenClients(
            decimal firstAccountNumber,
            decimal secondAccountNumber,
            decimal money)
        {
            bool isTrnasferMoney = this.customerBroker.TransferMoneyBetweenAccounts(
                firstAccountNumber,
                secondAccountNumber,
                money);
            if (isTrnasferMoney is true)
            {
                this.loggingBroker
                    .LogInformation(
                    "Money has been successfully transferred between two accounts.");
                return isTrnasferMoney;
            }

            this.loggingBroker
                .LogError(
                "An error occurred when transferring money between two accounts.");

            return isTrnasferMoney;
        }

        private bool InvalidTransferMoneyBetweenClients()
        {
            this.loggingBroker
                .LogError("There is an error when entering account number or currency information.");

            return false;
        }

        private bool ValidationAndDeleteClient(decimal accountNumber)
        {
            bool isClosesForClient =
                this.customerBroker.CloseAccountNumberForClient(accountNumber);

            if (isClosesForClient is true)
            {
                this.loggingBroker.LogInformation("Client account closed successfully.");
                return isClosesForClient;
            }

            this.loggingBroker.LogError("The client account does not exist in the database.");
            return isClosesForClient;
        }

        private bool InvalidDeleteClient()
        {
            this.loggingBroker.LogError("Account number information is incomplete.");
            return false;
        }

        private bool ValidationAndCreateClient(Customer customer)
        {
            if (!(String.IsNullOrWhiteSpace(customer.Name))
                && (customer.AccountNumber.ToString().Length >= 16
                && customer.AccountNumber > 0))
            {
                bool isCreateClient =
                    this.customerBroker.CreateAccountNumberForClient(customer);

                if (isCreateClient is true)
                {

                    this.loggingBroker
                        .LogInformation("The account for the client has been successfully created.");

                    return isCreateClient;
                }
                else
                {
                    this.loggingBroker.LogError("This account has been created.");
                    return isCreateClient;
                }
            }

            this.loggingBroker.LogError("The client information is incomplete.");
            return false;
        }

        private bool InvalidCreateClient()
        {
            this.loggingBroker.LogError("Client has no information.");
            return false;
        }
    }
}
