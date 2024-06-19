//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

namespace Bank.Management.Console.Brokers.Storages.BankStorage
{
    internal class BankStorageBroker : IBankStorageBroker
    {

        private readonly string filePath = "../../../Assets/BankFileDB.txt";

        public BankStorageBroker()
        {
            EnsurFileExists();
        }

        public decimal GetBalance(decimal accountNumberForBank)
        {
            if(accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositLines = File.ReadAllLines(filePath);

                for (int itarator = 0; itarator < depositLines.Length; itarator++)
                {
                    string depositeLine = depositLines[itarator];
                    string[] depositInfo = depositeLine.Split('*');

                    if (depositInfo[0].Contains(accountNumberForBank.ToString()))
                    {
                        return Convert.ToDecimal(depositInfo[1]);
                    }
                }

                return 0;
            }

            return 0;
        }

        public bool MakingDeposit(decimal accountNumberForBank, decimal balance)
        {
            if(accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositLines = File.ReadAllLines(filePath);

                for (int itarator = 0; itarator < depositLines.Length; itarator++)
                {
                    string depositeLine = depositLines[itarator];
                    string[] depositInfo = depositeLine.Split('*');

                    if (depositInfo[0].Contains(accountNumberForBank.ToString()))
                    {
                        depositInfo[1] = (Convert.ToDecimal(depositInfo[1]) + balance).ToString();
                        depositeLine = $"{depositInfo[0]}*{depositInfo[1]}";
                        depositLines[itarator] = depositeLine;

                        File.Delete(filePath);
                        File.WriteAllLines(filePath, depositLines);
                        return true;
                    }
                }

                string newDeposite = $"{accountNumberForBank}*{balance}\n";
                File.AppendAllText(filePath, newDeposite);
                return true;
            }

            return false;
        }

        public decimal WithdrawMoney(decimal accountNumberForBank, decimal balance)
        {
            if (accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositLines = File.ReadAllLines(filePath);

                for (int itarator = 0; itarator < depositLines.Length; itarator++)
                {
                    string depositeLine = depositLines[itarator];
                    string[] depositInfo = depositeLine.Split('*');

                    if (depositInfo[0].Contains(accountNumberForBank.ToString()))
                    {
                        depositInfo[1] = (Convert.ToDecimal(depositInfo[1]) - balance).ToString();
                        depositeLine = $"{depositInfo[0]}*{depositInfo[1]}";
                        depositLines[itarator] = depositeLine;

                        File.Delete(filePath);
                        File.WriteAllLines(filePath, depositLines);
                        return balance;
                    }
                }
            }

            return 0;
        }

        private void EnsurFileExists()
        {
            bool isFileThere = File.Exists(filePath);

            if (isFileThere is true)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
