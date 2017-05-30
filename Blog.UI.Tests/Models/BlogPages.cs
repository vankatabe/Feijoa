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

        public string Key { get; set; }

        // Next two variables are used only for limited number of reading Email and Fullname from the DataDriven. In most cases the Email and Fullname are uniquely generated
        public string Email { get; set; }
        public string Fullname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Effect { get; set; }

        public string Effect2 { get; set; }

        public string Asserter { get; set; }

        public string Asserter2 { get; set; }

        public string Status { get; set; }

        public string UniqEmail(string uniqId)
        {
            return uniqId + "@uniqueemail.com";
        }

        public string UniqFullname(string uniqId)
        {
            return uniqId;
        }

        public string ArticleBodyText { get; set; }
    }
}
