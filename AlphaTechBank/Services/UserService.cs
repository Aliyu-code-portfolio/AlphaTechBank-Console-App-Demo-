using AlphaTechBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTechBank.Services
{
    public class UserService
    {
        public List<User> Users { get; set; }
        public UserService(List<User> users) 
        {
            Users = users;
            Users.Add(new User()
            {
                FirstName = "Aliyu",
                LastName = "Abdullahi",
                Id = "DD44-445KIG",
                Email = "aliyu@gmail.com",
                Password = "inovation",
                PhoneNumber = "1234567890"
            });
            Users.Add(new User()
            {
                FirstName = "Kalid",
                LastName = "Miracle",
                Id = "DD44223-45KIG",
                Email = "kalid@gmail.com",
                Password = "inovation",
                PhoneNumber = "1234567890",
            });
        }

        public void CreateNewUser()
        {
            //regular expression to force user not to add number to the beginning of their names 
            Console.WriteLine("Enter your first and last name");
            string[] names = Console.ReadLine().Trim().Split(' ');
            if (names.Length < 2)
            {
                Console.WriteLine("Invalid. Enter first name and last name");
                return;
            }
            bool isValid = false;
            EmailAddressAttribute emailAddressAttribute = new();
            string email;
            do
            {
                Console.WriteLine("Enter a valid email address");
                email = Console.ReadLine();
                isValid = emailAddressAttribute.IsValid(email);
            }
            while (!isValid);
            if (Users.Where(u => u.Email == email).Any())
            {
                Console.WriteLine("\nEmail already exists\n");
                return;
            }
            Console.WriteLine("Enter your phone number");
            //regular expression to force user to add country code 
            long.TryParse(Console.ReadLine(), out long phoneNumber);
            string id = Guid.NewGuid().ToString();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            User user = new User();
            user.Id = id;
            user.FirstName = names[0];
            user.LastName = names[1];
            user.PhoneNumber = phoneNumber.ToString();
            user.Email = email;
            user.Password = password;
            Users.Add(user);

        }
        public string LoggedInUser()
        {
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine().Trim();
            User user = Users.Where(x => x.Email == email).FirstOrDefault();
            if(user is null)
            {
                Console.WriteLine("Failed to log in\n");
                return "";
            }
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine().Trim();
            if(user.Password != password)
            {
                Console.WriteLine("Failed to log in\n");
                return "";
            }
            Console.WriteLine("Successfully logged in\n");
            return user.Id;    
        }
    }
}
