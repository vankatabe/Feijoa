using NUnit.Framework;
using OpenQA.Selenium;
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

        public static void AssertArticleIsDeleted(this HomePage page, string uniqId)
        {
            Assert.Throws(typeof(NoSuchElementException), delegate { page.ArticleTitle(uniqId); });
            /* Another way - Assert that Exception Message is the relevant one:
              NoSuchElementException ex = Assert.Throws<NoSuchElementException>(
             delegate { throw new NoSuchElementException($"OpenQA.Selenium.NoSuchElementException : Unable to locate element: //*[contains(text(), '{page.ArticleTitle(uniqId)}')]"); });
             Assert.That(ex.Message, Is.StringContaining($"OpenQA.Selenium.NoSuchElementException : Unable to locate element: //*[contains(text(), '{page.ArticleTitle(uniqId)}')]"));
            */
        }

        public static void AssertLoginPageUrl(this HomePage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }

        public static void AssertRegisterPageUrl(this HomePage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }

        public static void AssertHomePageUrl(this HomePage page, IWebDriver driver, string text)
        {
            Assert.AreEqual(text, driver.Url);
        }
    }
}
