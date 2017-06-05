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

        public static void AssertTitleErrorMessageDisplayed(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.TitleErrorMessage.Text);
        }

        public static void AssertContentErrorMessageDisplayed(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.ContentErrorMessage.Text);
        }

        public static void AssertTitleOutsideRangeErrorMessageDisplayed(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.TitleOutsideRangeErrorMessage.Text);
        }

        public static void AssertCommentButtonExists(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.CommentButton.Text);
        }

        public static void AssertArticleAuthorExists(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.ArticleAuthor.Text);
        }

        public static void AssertCommentExists(this CreatePage page, string text)
        {
            Assert.AreEqual(text, page.Comment.Text);
        }
    }
}