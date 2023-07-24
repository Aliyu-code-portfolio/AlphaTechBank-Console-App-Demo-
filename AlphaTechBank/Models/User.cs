using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTechBank.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Input a valid email address")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(13, ErrorMessage = "Maximum lenght is 13 character")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<Account> UserAccounts { get; set; }
    }
}
