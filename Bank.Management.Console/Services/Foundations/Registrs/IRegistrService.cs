//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Services.Foundations.Registrs
{
    internal interface IRegistrService
    {
        User SignUp(User user);
        bool LogIn(User user);
    }
}
