using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employers
{
    public class User
    {
        public User(string login, string password, string email, string access)
        {
            this.Login = login;
            this.Password = password;
            this.Email = email;
            this.Access = access;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Telephone { get; set; }
        public string Post { get; set; }
        public string Access { get; set; }
    }
}
