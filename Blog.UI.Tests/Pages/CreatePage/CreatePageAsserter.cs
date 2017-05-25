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
            //page.ArticleTitle = 
            Assert.AreEqual(text, title.Text);
        }
    }
}
