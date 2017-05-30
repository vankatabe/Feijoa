using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    public class HomePageTests
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void CleanUp()
        {
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.Status.ToString());// Write actual test result status in xlsx
            HomePage homePage = new HomePage(this.driver);
            homePage.YeOldeFailedTestsLogger(); // The old-style logger for failed tests
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog web address, expected: Blog homepage open and Logo present")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage()
        {
            HomePage homePage = new HomePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file

            homePage.NavigateTo(homePage.URL);

            homePage.AssertHomePageLogoDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { homePage, page.Effect });
        }
    }
}