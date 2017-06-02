using Blog.UI.Tests.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.CreatePage
{
    public partial class CreatePage : BasePage
    {
        public CreatePage(IWebDriver driver) : base(driver)
        {
        }

        public string URL
        {
            get
            {
                return base.url + "Article/Create/";
            }
        }

        public void CreateArticle(BlogPages user, string uniqId)
        {
            Type(this.Title, "Article" + uniqId);
            Type(this.Content, uniqId + ' ' + user.ArticleBodyText);
            this.SubmitButton.Click();
        }

        private void Type(IWebElement element, string text)
        {
            element.Clear();
            if (text == null)
            {
                text = String.Empty;
            }
            element.SendKeys(text);
        }

        public void CancelButtonClick()
        {
            CancelButton.Click();
        }
    }
}
