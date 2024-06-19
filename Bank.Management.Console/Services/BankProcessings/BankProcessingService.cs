//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;
using Bank.Management.Console.Services.Foundations.Banks;
using Bank.Management.Console.Services.Foundations.Customers;
using Bank.Management.Console.Services.Foundations.Registrs;

namespace Bank.Management.Console.Services.BankProcessings
{
    internal class BankProcessingService : IBankProcessingService
    {
        private readonly IRegistrService registrService;
        private readonly IBankService bankService;
        private readonly ICustomerService customerService;

        public BankProcessingService(
            IRegistrService registrService,
            IBankService bankService,
            ICustomerService customerService)
        {
            this.registrService = registrService;
            this.bankService = bankService;
            this.customerService = customerService;
        }

        public bool DeleteForClient(decimal accountNumber) =>
            this.customerService.DeleteClient(accountNumber);

        public decimal GetBalance(decimal accountNumberForBank) =>
            this.bankService.GetBalanceInBank(accountNumberForBank);

        public decimal GetMoney(decimal accountNumberForBank, decimal balance) =>
            this.bankService.GetMoney(accountNumberForBank, balance);

        public bool LogInUser(User user) =>
            this.registrService.LogIn(user);

        public bool PostDeposit(decimal accountNumberForBank, decimal balance) =>
            this.bankService.AddDeposit(accountNumberForBank, balance);

        public bool PostForClient(Customer customer) =>
            this.customerService.CreateClient(customer);

        public User PostUser(User user) =>
            this.registrService.SignUp(user);

        public bool TransferMoneyBetweenAccountsForClient(
            decimal firstAccountNumber,
            decimal secondAccountNumber,
            decimal money) =>
            this.customerService.TransferMoneyBetweenClients(
                            firstAccountNumber,
                            secondAccountNumber,
                            money);
    }
}
