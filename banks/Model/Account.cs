using System;
using System.Collections.Generic;
using System.Text;

namespace banks.Model
{
    public class Account
    {
        public int ClientId { get; set; }
        public int AccId { get; set; } 
        public decimal Balance { get; set; }
        public string Status { get; set; } 
    }
}
