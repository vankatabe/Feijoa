using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.HomePage
{
    public static class HomePageAsserter
    {
        public static void AssertHomePageLogoDisplayed(this HomePage page, string text)
        {
            Assert.AreEqual(text, page.Logo.Text);
        }

        public static void AssertLoginDisplayed(this HomePage page, string text)
        {
            Assert.AreEqual(text, page.LoginLink.Text);
        }
    }
}
