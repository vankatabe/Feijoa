using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.LoginPage
{
    public partial class LoginPage
    {
        public IWebElement Email
        {
            get
            {
                return this.Driver.FindElement(By.Id("Email"));
            }
        }

        public IWebElement Password
        {
            get
            {
                return this.Driver.FindElement(By.Id("Password"));
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[4]/div/input"));
            }
        }

        public IWebElement GreetingMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='logoutForm']/ul/li[2]/a"));
            }
        }

        public IWebElement PasswordErrorMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id=\"Password-error\"]"));
            }
        }

        public IWebElement EmailErrorMessage
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[2]/div/span"));
            }
        }

        public IWebElement ManageLink
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id=\"logoutForm\"]/ul/li[2]/a"));
            }
        }
    }
}
