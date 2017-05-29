using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.DeletePage
{
    public partial class DeletePage
    {
        public IWebElement Title
        {
            get
            {
                return this.Driver.FindElement(By.Id("Title"));
            }
        }


        public IWebElement Content
        {
            get
            {
                return this.Driver.FindElement(By.Id("Content"));
            }
        }

        public IWebElement DeleteButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[3]/div/input"));
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[3]/div/a"));
            }
        }

        public IWebElement DeleteArticleURL
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[4]/div/a"));
            }
        }

        public IWebElement ArticleContent
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//p"));
            }
        }
    }
}
