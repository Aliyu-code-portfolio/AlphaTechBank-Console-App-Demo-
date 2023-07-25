using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlphaTechBank.ServiceExtensions
{
    public static class ExtensionMethod
    {
        public static bool ValidatePhoneNumber(this string phoneNumber)
        {
            Regex regex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            return regex.IsMatch(phoneNumber);
        }
        public static bool ValidateEmailAddress(this string emailAddress)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(emailAddress);
        }
    }
}
