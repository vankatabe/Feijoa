using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.CreatePage
{
    public partial class CreatePage
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

        public IWebElement TitleErrorMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[1]/ul/li"));
            }
        }

        public IWebElement ContentErrorMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[1]/ul/li"));
            }
        }

        public IWebElement TitleOutsideRangeErrorMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[1]/ul/li"));
            }
        }

        public IWebElement CommentButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//a[contains(text(), 'Comment')]"));
            }
        }

        public IWebElement ArticleAuthor
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/article/small"));
            }
        }

        public IWebElement CommentField
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//a[contains(text(), 'Comment:')]"));
            }
        }

        public IWebElement SubmitCommentButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//a[contains(text(), 'SubmitComment')]"));
            }
        }

        public IWebElement Comment
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//a[contains(text(), 'Comment by:')]"));
            }
        }
    }
}
