using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.RegisterPage
{
    public static class RegisterPageAsserter
    {
        public static void AssertGreetingDisplayed(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.GreetingMessage.Text);
        }

        public static void AssertEmailErrorMessageExists(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.EmailErrorMessage.Text);
        }

        public static void AssertFullNameErrorMessageExists(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.FullNameErrorMessage.Text);
        }

        public static void AssertPasswordErrorMessageExists(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.PasswordErrorMessage.Text);
        }

        public static void AssertPasswordMissmatchErrorMessageExists(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.PasswordMissmatchErrorMessage.Text);
        }

        public static void AssertPasswordMissmatchErrorMessageExists2(this RegisterPage page, string text)
        {
            Assert.AreEqual(text, page.PasswordMissmatchErrorMessage2.Text);
        }
    }
}
