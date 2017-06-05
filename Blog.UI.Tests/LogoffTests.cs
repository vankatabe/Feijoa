using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using Blog.UI.Tests.Pages.LoginPage;
using Blog.UI.Tests.Pages.RegisterPage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    public class LogoffTests
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;
        private string uniqId;

        [SetUp]
        public void Init()
        {
            uniqId = Guid.NewGuid().ToString();
        }

        [TearDown]
        public void CleanUp()
        {
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.Status.ToString());// Write actual test result status in xlsx
            HomePage homePage = new HomePage(this.driver);
            homePage.YeOldeFailedTestsLogger(); // The old-style logger for failed tests
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 6), Property("Logoff test number:", 1)]
        [Description("Register with valid User credentials where User Logs-in automatically and press Logoff link, expected: User logged-off and Login link displayed")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Logoff_ClickLogoffLink_LogoffSuccessful()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            // Assert that User is Logged-in - good to have but breaks the test:
            // loginPage.AssertGreetingDisplayed("Hello " + page.UniqEmail(uniqId) + '!');

            Thread.Sleep(1000);
            homePage.LogoffLink.Click(); // Press Logoff link
            Thread.Sleep(1000);

            homePage.AssertLoginDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { homePage, page.Effect });
        }
    }
}
