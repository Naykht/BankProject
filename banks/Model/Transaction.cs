using System;
using System.Collections.Generic;
using System.Text;

namespace banks.Model
{
    public class Transaction
    {
        public int TranId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
