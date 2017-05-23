using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.RegisterPage
{
    public partial class RegisterPage
    {
        public IWebElement Email
        {
            get
            {
                return this.Driver.FindElement(By.Id("Email"));
            }
        }

        public IWebElement Fullname
        {
            get
            {
                return this.Driver.FindElement(By.Id("FullName"));
            }
        }

        public IWebElement Password
        {
            get
            {
                return this.Driver.FindElement(By.Id("Password"));
            }
        }

        public IWebElement ConfirmPassword
        {
            get
            {
                return this.Driver.FindElement(By.Id("ConfirmPassword"));
            }
        }

        public IWebElement SubmitButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//div[6]/div/input"));
            }
        }


        public IWebElement GreetingMessage
        {
            get
            {
                this.Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='logoutForm']/ul/li[2]/a")));
                return this.Driver.FindElement(By.XPath("//*[@id='logoutForm']/ul/li[2]/a"));
            }
        }
        
    }
}
