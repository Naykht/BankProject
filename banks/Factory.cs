using banks.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace banks
{
    public class Factory
    {
        static Factory instance; 
        private Factory() { }
        public static Factory Instance => instance ?? (instance = new Factory());
        public IDeposit GDeposit()
        {
            return new BankDataJson();
        }
        public IClient GClient()
        {
            return new BankDataJson();
        }
        public IAccount GAccount()
        {
            return new BankDataJson();
        }
        public ILoan GLoan()
        {
            return new BankDataJson();
        }
        public ITransaction GTransaction()
        {
            return new BankDataJson();
        }
    }
}
