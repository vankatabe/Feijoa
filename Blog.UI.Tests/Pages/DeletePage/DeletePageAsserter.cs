using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.DeletePage
{
    public static class DeletePageAsserter
    {
        public static void AssertConfirmDeleteButtonDisplayed(this DeletePage page, string text)
        {
            Assert.AreEqual(text, page.Delete.Text);
        }
    }
}
