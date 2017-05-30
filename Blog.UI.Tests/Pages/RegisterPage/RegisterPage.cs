using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Blog.UI.Tests.Models;

namespace Blog.UI.Tests.Pages.RegisterPage
{
    public partial class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {
        }

        public string URL
        {
            get
            {
                return base.url + "Account/Register/";
            }
        }

        public void FillRegistrationForm(BlogPages user, string uniqId)
        {
            Type(this.Email, user.UniqEmail(uniqId));
            Type(this.Fullname, user.UniqFullname(uniqId));
            Type(this.Password, user.Password);
            Type(this.ConfirmPassword, user.ConfirmPassword);
            this.SubmitButton.Click();
        }


        public void FillRegistrationFormNegative(BlogPages user)
        {
            Type(this.Email, user.Email);
            Type(this.Fullname, user.Fullname);
            Type(this.Password, user.Password);
            Type(this.ConfirmPassword, user.ConfirmPassword);
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
