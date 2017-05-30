using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.EditPage
{
    public partial class EditPage
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

        public IWebElement SubmitButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[4]/div/input"));
            }
        }

        public IWebElement CancelButton
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
