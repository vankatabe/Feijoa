using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.EditPage
{
    public static class EditPageAsserter
    {
        public static void AssertArticleIsEdited(this EditPage page, string text)
        {
            Assert.AreEqual(text, page.ArticleContent.Text);
        }

        public static void AssertUnableToEditMessageExists(this EditPage page, string text)
        {
            Assert.AreEqual(text, page.UnableToEdiErrorMessage.Text);
        }
    }
}
