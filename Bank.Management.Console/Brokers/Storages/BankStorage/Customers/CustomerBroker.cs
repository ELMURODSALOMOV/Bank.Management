//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Brokers.Storages.BankStorage.Customers
{
    internal class CustomerBroker : ICustomerBroker
    {
        private readonly string filePath = "../../../Assets/CustomerFileDB.txt";
        private bool isDelete;

        public CustomerBroker()
        {
            isDelete = false;
            EnsureFileExists();
        }

        public bool CloseAccountNumberForClient(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);
            File.WriteAllText(filePath,string.Empty);

            for (int itarator = 0; itarator < clientAllInfo.Length; itarator++)
            {
                string clientInfo = clientAllInfo[itarator];
                string[] client = clientInfo.Split('*');

                if (client[1].Contains(accountNumber.ToString()))
                {
                    isDelete = true;
                }
                else
                {
                    File.AppendAllText(filePath, clientInfo + "\n");
                }
            }

            return isDelete;    
        }

        public bool CreateAccountNumberForClient(Customer customer)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for(int itarator = 0; itarator < clientAllInfo.Length; itarator++)
            {
                string clientInfo = clientAllInfo[itarator];
                string[] client = clientInfo.Split('*');

                if (client[0].Contains(customer.Name)
                    && client[1].Contains(customer.AccountNumber.ToString()))
                {
                    return false;
                }
            }

            string newClient = $"{customer.Name}*{customer.AccountNumber}";
            File.AppendAllText(filePath, newClient);
            return true;
        }

        public bool TransferMoneyBetweenAccounts(decimal firstAccountNumber, decimal secondAccountNumber, decimal money)
        {
            string[] clientInfo = File.ReadAllLines(filePath);
            File.WriteAllText(filePath, string.Empty);

            if(IsAccountNumberCheck(firstAccountNumber) 
                && IsAccountNumberCheck(secondAccountNumber))
            {
                int firstIndex = this.GetIndex(firstAccountNumber);
                int secondIndex = this.GetIndex(secondAccountNumber);

                if (Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) >= money)
                {
                    clientInfo[firstIndex].Split('*')[2] =
                        (Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) - money).ToString();
                    clientInfo[secondIndex].Split('*')[2] =
                             (Convert.ToDecimal(clientInfo[secondIndex].Split('*')[2]) + money).ToString();

                    for (int itarator = 0; itarator < clientInfo.Length; itarator++)
                    {
                        string clientLineInfo = clientInfo[itarator];
                        File.AppendAllText(filePath, clientLineInfo + "\n");
                    }

                    return true;
                }
            }

            return false;
        }

        private bool IsAccountNumberCheck(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for(int itarator = 0; itarator < clientAllInfo.Length; itarator++)
            {
                string clientInfo = clientAllInfo[itarator];
                string[] client = clientInfo.Split('*');

                if (client[1].Contains(accountNumber.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetIndex(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for (int itarator = 0; itarator < clientAllInfo.Length; itarator++)
            {
                string clientInfo = clientAllInfo[itarator];
                string[] client = clientInfo.Split('*');

                if (client[1].Contains(accountNumber.ToString()))
                {
                    return itarator;
                }
            }

            return -1;
        }

        private void EnsureFileExists()
        {
            bool isFileThere = File.Exists(filePath);

            if (isFileThere is true)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
