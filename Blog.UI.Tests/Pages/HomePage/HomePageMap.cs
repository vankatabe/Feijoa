using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.HomePage
{
    public partial class HomePage
    {
        public IWebElement Logo
        {
            get
            {
                return this.Wait.Until(w => w.FindElement(By.CssSelector("a.navbar-brand")));
                // by xpath /html/body/div[1]/div/div[1]/a
                // by xpath contains text: //a[contains(text(),'SOFTUNI BLOG')]
                // by css selector: a.navbar-brand
                // var logo = homePage.Wait.Until(w => w.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a")));
                // this.Wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[1]/a")));
                // return this.Driver.FindElement(By.XPath("//*[@id='pie_register']/li[1]/div/div/span"));

            }
        }

        public IWebElement LogoffLink
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//a[contains(text(),'Log off')]"));
            }
        }
        
            public IWebElement LoginLink
        {
            get
            {
                return this.Driver.FindElement(By.Id("loginLink"));
            }
        }

    }
}
