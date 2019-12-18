using System;
using System.Collections.Generic;
using System.Text;

namespace banks.Model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int AccId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Percent { get; set; }
    }
}
