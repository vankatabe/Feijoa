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
    }
}
