//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Brokers.Loggings;
using Bank.Management.Console.Brokers.Storages;
using Bank.Management.Console.Models;
using System.ComponentModel.DataAnnotations;

namespace Bank.Management.Console.Services.Foundations.Registrs
{
    internal class RegistrService : IRegistrService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IRegistrBroker registrBroker;

        public RegistrService()
        {
            this.loggingBroker = new LoggingBroker();
            this.registrBroker = new RegistrBroker();
        }

        public bool LogIn(User user)
        {
            return user is null
                ? InvalidLogInUser()
                : ValidationAndLogInUser(user);
        }

        public User SignUp(User user)
        {
            return user is null
                ? InvalidSignUpUser()
                : ValidationAndSignUpUser(user);
        }

        private User ValidationAndSignUpUser(User user)
        {
            throw new NotImplementedException();
        }

        private User InvalidSignUpUser()
        {
            throw new NotImplementedException();
        }

        private bool ValidationAndLogInUser(User user)
        {
            throw new NotImplementedException();
        }

        private bool InvalidLogInUser()
        {
            throw new NotImplementedException();
        }
    }
}
