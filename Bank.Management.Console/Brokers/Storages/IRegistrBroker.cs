//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Brokers.Storages
{
    internal interface IRegistrBroker
    {
        User AddUser(User user);
        bool CheckoutUser(User user);
    }
}
