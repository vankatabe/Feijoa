using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.HomePage
{
    public partial class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public string URL
        {
            get
            {
                return base.url;
            }
        }

        public void OpenArticle(string Id)
        {
            ArticleTitle(Id).Click();
        }

        public void OpenLoginPage()
        {
            LoginLink.Click();
        }

        public void OpenRegistrationPage()
        {
            RegisterLink.Click();
        }

        public void ClickLogo()
        {
            Logo.Click();
        }

    }
}
