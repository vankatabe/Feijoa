using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.ManagePage
{
    public static class ManagePageAsserter
    {
        public static void AssertChangePasswordDisplayed(this ManagePage page, string text)
        {
            Assert.AreEqual(text, page.ChangePassword.Text);
        }

        public static void AssertManagePageUrl(this ManagePage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }
    }
}
