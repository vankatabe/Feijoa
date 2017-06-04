using NUnit.Framework;
using OpenQA.Selenium;
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

        public static void AssertManagePageUrl(this LoginPage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }

        public static void AssertInvalidPasswordErrorMessageExists(this LoginPage page, string text)
        {
            Assert.AreEqual(text, page.InvalidPasswordErrorMessage.Text);
        }

        public static void AssertInvalidEmailErrorMessageExists(this LoginPage page, string text)
        {
            Assert.AreEqual(text, page.InvalidEmailErrorMessage.Text);
        }
    }
}
