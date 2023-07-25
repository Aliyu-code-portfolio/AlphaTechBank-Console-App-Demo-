using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTechBank.Models
{
    public class Account
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Input a valid email address")]
        public string Email { get; set; }
        public int TransactionPin { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(13, ErrorMessage = "Maximum lenght is 13 character")]
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal minBalance => AccountType == "savings" ? 1000 : 0;
        public bool IsActive { get; set; } = true;
        public List<Transations> Transations { get; set; } = new List<Transations>();

      
    }
}
