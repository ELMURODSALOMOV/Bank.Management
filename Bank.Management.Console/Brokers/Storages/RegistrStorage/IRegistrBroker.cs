//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Brokers.Storages.RegistrStorage
{
    internal interface IRegistrBroker
    {
        User AddUser(User user);
        bool CheckoutUser(User user);
    }
}
