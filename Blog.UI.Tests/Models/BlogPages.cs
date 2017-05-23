﻿using System;
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

        // Remains from the old way of reading Email and Fullname from the DataDriven. Now the Email and Fullname are uniquely generated
        public string Email { get; set; }
        public string Fullname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Effect { get; set; }

        public string Asserter { get; set; }

        public string Status { get; set; }

        public string UniqEmail(string uniqId)
        {
             return uniqId + "@uniqueemail.com"; 
            // set { email = uniqueUser + "@uniqueemail.com"; }
        }

        public string UniqFullname(string uniqId)
        {
            return uniqId; 
            //set { fullname = uniqueUser; }
        }
    }
}
