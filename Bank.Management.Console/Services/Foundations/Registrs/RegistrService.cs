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
            if(String.IsNullOrWhiteSpace(user.Name)
                || String.IsNullOrWhiteSpace(user.Password))
            {
                this.loggingBroker.LogInformation("User data is not required.");
                return false;
            }
            else
            {
               bool IsLogIn =  this.registrBroker.CheckoutUser(user);

                if(user.Password.Length >= 8)
                {
                    if(IsLogIn is true)
                    {
                        this.loggingBroker.LogInformation("Successfully logged in.");
                        return true;
                    }
                    else
                    {
                        this.loggingBroker.LogError("User does not exist in the database.");
                        return false;
                    }
                }
                else
                {
                    this.loggingBroker.LogError("Password does not contain 8 characters.");
                    return false;
                }
            }
        }

        private bool InvalidLogInUser()
        {
            this.loggingBroker.LogError("User data is null.");
            return false;
        }
    }
}
