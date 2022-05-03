using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employers
{
    public class Employer
    {
        public Employer(string name, string address = null, string phone = null)
        {
            this.Name = name;
            this.Addres = address;
            this.Phone = phone;
        }
        public string Name { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }

        public override string ToString() => (Name + '\t' + Addres + '\t' + Phone);

        public static int CompareByName(Employer first, Employer second) => String.Compare(first.Name, second.Name);
    }
}
