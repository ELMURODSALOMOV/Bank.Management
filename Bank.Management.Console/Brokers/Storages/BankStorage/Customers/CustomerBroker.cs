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

        public decimal GetBalance(decimal accountNumber)
        {
            if(accountNumber.ToString().Length >= 16)
            {
                string[] accountLines = File.ReadAllLines(filePath);

                for(int itarator = 0; itarator < accountLines.Length; itarator++)
                {
                    string accountLine = accountLines[itarator];
                    string[] clientInfo = accountLine.Split('*');

                    if (clientInfo[1].Contains(accountNumber.ToString()))
                    {
                        return Convert.ToDecimal(clientInfo[2]);
                    }
                }

                return 0;
            }

            return 0;
        }
        
        public string ReadAllCustormer()
        {
            string clientInfo = File.ReadAllText(filePath);
            return clientInfo;
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

            string newClient = $"{customer.Name}*{customer.AccountNumber}*{customer.Balance}\n";
            File.AppendAllText(filePath, newClient);
            return true;
        }

        public bool TransferMoneyBetweenAccounts(decimal firstAccountNumber, decimal secondAccountNumber, decimal money)
        {
            string[] clientInfo = File.ReadAllLines(filePath);

            if(IsAccountNumberCheck(firstAccountNumber) 
                && IsAccountNumberCheck(secondAccountNumber))
            { 
                int firstIndex = this.GetIndex(firstAccountNumber);
                int secondIndex = this.GetIndex(secondAccountNumber);

                if (Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) >= money)
                {
                    File.WriteAllText(filePath, string.Empty);
                    decimal firstAccount = Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]);
                    firstAccount = Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) - money;
                    clientInfo[firstIndex].Split('*')[2] = String.Empty;
                    clientInfo[firstIndex].Split('*')[2] = firstAccount.ToString();

                    decimal secondAccount = Convert.ToDecimal(clientInfo[secondIndex].Split('*')[2]);
                    firstAccount = Convert.ToDecimal(clientInfo[secondIndex].Split('*')[2]) + money;
                    clientInfo[secondIndex].Split('*')[2] = String.Empty;
                    clientInfo[secondIndex].Split('*')[2] = secondAccount.ToString();

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

            if (isFileThere is false)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
