using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.CreatePage
{
    public static class CreatePageAsserter
    {
        public static void AssertArticleIsDisplayed(this CreatePage page, IWebElement title, string text)
        {
            Assert.AreEqual(text, title.Text);
        }

        public static void AssertHomePageUrl(this CreatePage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }
    }
}
