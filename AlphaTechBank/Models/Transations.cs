using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTechBank.Models
{
    public class Transations
    {
        public string AccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string ReceiverName { get; set; }
        public string TransactionType { get; set; }
        public DateTime DateCompleted { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
