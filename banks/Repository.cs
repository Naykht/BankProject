using banks.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace banks
{
   
    public interface IClient
    {
        List<Client> Clients { get; }
        void AddClient(string name, DateTime date, string email, string phone, string address);
        void EditClient(string name, DateTime date, string email, string phone, string address, int id);
    }
    public interface IAccount
    {
        List<Account> Accounts { get; }
        List<Account> ClAcc(int id);
        void AddAccount(int clientId, decimal balance);
        void ChangeAccountStatus(Account acc);
        void Take(int id, decimal amount);
    }
    public interface ILoan
    {
        List<Loan> Loans { get; }
        List<Loan> DateLoan(DateTime start, DateTime end, string c);
        List<Loan> AccLoan(int id);
        void AddLoan(int id, decimal am, DateTime end, decimal per);
        void StatusLoan();
        void CloseLoan(int id);

    }
    public interface IDeposit
    {
        List<Deposit> Deposits { get; }
        List<Deposit> DateDep(DateTime start, DateTime end);
        List<Deposit> AccDep(int id);
        void AddDeposit(int depId, int accId, decimal amount, DateTime startDate, DateTime endDate, decimal Percent);
    }
    public interface ITransaction
    {
        List<Transaction> Transactions { get; }
        List<Transaction> DateTran(DateTime start, DateTime end);
        List<Transaction> AccTran(int id);
    }
    
    /*public interface IData
    {
        List<Client> Clients { get; set; }
        List<Account> Accounts { get; set; }
        List<Transaction> Transactions { get; set; }
        List<Loan> Loans { get; set; }
        List<Deposit> Deposits { get; set; }

        void AddClient(string name, DateTime date, string email, string phone, string address);
        void EditClient(string name, DateTime date, string email, string phone, string address, int id);
    }*/
    public interface IGetData
    {
        static string Path;
        void LoadData();
        void SaveData();
    }
    public class BankData: IAccount, IClient, ILoan, IDeposit, ITransaction
    {
        
        public List<Client> Clients { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Loan> Loans { get; set; }
        public List<Deposit> Deposits { get; set; }
        public virtual void SaveData()
        {
        }
        public void AddClient(string name, DateTime date, string email, string phone, string address)
        {
            var cl = new Client
            {
                Name = name,
                BirthDate = date,
                Phone = phone,
                Address = address,
                Email = email,
                Id = Clients.Max(u => u.Id) + 1
            };
            Clients.Add(cl);
            SaveData();
        }
        public void EditClient(string name, DateTime date, string email, string phone, string address, int id)
        {
            var cl = new Client
            {
                Name = name,
                BirthDate = date,
                Phone = phone,
                Address = address,
                Email = email,
                Id = id
            };
            Clients.RemoveAll(u => u.Id == id);
            Clients.Add(cl);
            Clients = Clients.OrderBy(u => u.Id).ToList();
            SaveData();
        }
        public void AddLoan(int id, decimal am, DateTime end, decimal percent)
        {
            var lo = new Loan
            {
                LoanId = Loans.Max(u => u.LoanId) + 1,
                Amount = am,
                StartDate = DateTime.Now.Date,
                EndDate = end,
                AccId = id,
                Status = "Active",
                Percent = percent
            };
            Loans.Add(lo);
            SaveData();
        }
        public void AddDeposit(int depId, int accId, decimal amount, DateTime startDate, DateTime endDate, decimal Percent)
        {
            var dep = new Deposit
            {
                AccId = accId,
                Amount = amount,
                StartDate = startDate,
                EndDate = endDate,
                Percent = Percent,
                DepId = Deposits.Max(u => u.DepId) + 1
            };
            Deposits.Add(dep);
            SaveData();
        }
        public void AddAccount(int clientId, decimal balance)
        {
            var ac = new Account
            {
                ClientId = clientId,
                AccId = Accounts.Max(u => u.AccId) + 1,
                Balance = balance,
                Status = true
            };
            Accounts.Add(ac);
            SaveData();
        }

        public void ChangeAccountStatus(Account acc)
        {
            if (acc.Status == false)
                acc.Status = true;
            else
                acc.Status = false;
            Accounts.RemoveAll(u => u.AccId == acc.AccId);
            Accounts.Add(acc);
            SaveData();
        }
        public List<Loan> DateLoan(DateTime start, DateTime end, string c)
        {
            if (c == "All")
                return Loans.FindAll(u => u.StartDate >= start && u.EndDate <= end);
            return Loans.FindAll(u => u.StartDate >= start && u.EndDate <= end && u.Status == c);
        }
        public List<Deposit> DateDep(DateTime start, DateTime end)
        {
            return Deposits.FindAll(u => u.StartDate <= end || u.EndDate <= start);
        }
        public List<Transaction> DateTran(DateTime start, DateTime end)
        {
            return Transactions.FindAll(u => u.Date >= start && u.Date <= end);
        }
        public List<Loan> AccLoan(int id)
        {
            return Loans.FindAll(u => u.AccId == id);
        }
        public List<Deposit> AccDep(int id)
        {
            return Deposits.FindAll(u => u.AccId == id);
        }
        public List<Transaction> AccTran(int id)
        {
            return Transactions.FindAll(u => u.From == id || u.To == id);
        }
        public List<Account> ClAcc(int id)
        {
            return Accounts.FindAll(u => u.ClientId == id);
        }
        public void StatusLoan()
        {
            var los = Loans.FindAll(u => u.EndDate < DateTime.Now && u.Status == "Active");
            foreach (Loan el in los)
            {
                Loans.RemoveAll(u => u.LoanId == el.LoanId);
                el.Status = "Expired";
                Loans.Add(el);
            }
            SaveData();
        }
        public void CloseLoan(int id)
        {
            Loan lo = Loans.First(u => u.LoanId == id);
            lo.Status = "Closed";
            Loans.RemoveAll(u => u.LoanId == id);
            Loans.Add(lo);
            SaveData();
        }
        public void Take(int id, decimal amount)
        {
            Account acc = Accounts.First(u => u.AccId == id);
            Accounts.RemoveAll(u => u.AccId == id);
            acc.Balance = acc.Balance - amount;
            Accounts.Add(acc);
            SaveData();
        }
    }
    public class BankDataJson: BankData, IGetData
    {
        
        static string Path = "../../../../banks/Data/BankData.json";
        
        public BankDataJson()
        {
            LoadData();
        }
        public void Serialize<T>(T data)
        {
            using (var sw = new StreamWriter(Path))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }
        public T Deserialize<T>()
        {
            using (var sr = new StreamReader(Path))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }
        public void LoadData()
        {
            var data = Deserialize<BankData>();
            Clients = data.Clients ?? new List<Client>();
            Deposits = data.Deposits ?? new List<Deposit>();
            Transactions = data.Transactions ?? new List<Transaction>();
            Accounts = data.Accounts ?? new List<Account>();
            Loans = data.Loans ?? new List<Loan>();

        }
        public override void SaveData()
        {
            var data = new BankData
            {
                Clients = Clients,
                Deposits = Deposits,
                Accounts = Accounts,
                Loans = Loans,
                Transactions = Transactions
            };
            Serialize(data);
        }
    }
}
