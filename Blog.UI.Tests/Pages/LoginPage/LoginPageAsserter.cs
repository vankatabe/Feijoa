﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.LoginPage
{
    public static class LoginPageAsserter
    {
        public static void AssertGreetingDisplayed(this LoginPage page, string text)
        {
            Assert.AreEqual(text, page.GreetingMessage.Text);
        }

        public static void AssertPasswordErrorMessageExists(this LoginPage page, string text)
        {
            Assert.AreEqual(text, page.PasswordErrorMessage.Text);
        }

        public static void AssertEmailErrorMessageExists(this LoginPage page, string text)
        {
            Assert.AreEqual(text, page.EmailErrorMessage.Text);
        }
    }
}
