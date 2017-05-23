using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Models
{
    public class BlogPages
    {
        private static string uniqueUser = Guid.NewGuid().ToString();
        private static string email = uniqueUser + "@email.com";
        private static string fullname = uniqueUser;

        public string Key { get; set; }

        // Remains from the old way of reading Email and Fullname from the DataDriven. Now the Email and Fullname are uniquely generated
        public string Email { get; set; }
        public string Fullname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Effect { get; set; }

        public string Asserter { get; set; }

        public string Status { get; set; }

        public string UniqEmail
        {
            get { return email; }
            set { email = uniqueUser + "@uniqueemail.com"; }
        }

        public string UniqFullname
        {
            get { return fullname; }
            set { fullname = uniqueUser; }
        }
    }
}
