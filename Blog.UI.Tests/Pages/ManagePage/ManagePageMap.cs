using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Blog.UI.Tests.Pages.ManagePage
{
    public partial class ManagePage : BasePage
    {
        public ManagePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ChangePasswordLink
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/dl/dd/a"));
            }
        }        

            public IWebElement SubmitButton
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[5]/div/input"));
            }
        }

        public IWebElement ChangePassword
        {
            get
            {
                return this.Driver.FindElement(By.XPath("/html/body/div[2]/div/dl/dd/a"));
            }
        }

    }
}
