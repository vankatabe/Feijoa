using Blog.UI.Tests.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.LoginPage
{
    public partial class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public string URL
        {
            get
            {
                return base.url + "Account/Login/";
            }
        }

        public void FillLoginForm(BlogPages user, string uniqId)
        {
            Type(this.Email, user.UniqEmail(uniqId));
            Type(this.Password, user.Password);
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
    }
}
