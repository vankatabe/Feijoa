using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using Blog.UI.Tests.Pages.LoginPage;
using Blog.UI.Tests.Pages.ManagePage;
using Blog.UI.Tests.Pages.RegisterPage;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    class ManagePageTests
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
        [Property("Priority", 1), Property("Test scenario number:", 3), Property("Login test number:", 2)]
        [Description("User creates an account, logs in and enters Manage panel, then selects Change password, expected: Navigate to Change password page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Account_ClickOnChangePassword_ChangePasswordPageLoaded()
        {
            RegisterPage regPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            ManagePage managePage = new ManagePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            regPage.NavigateTo(regPage.URL);
            regPage.FillRegistrationForm(page, uniqId);
            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginForm(page, uniqId);

            loginPage.ManageLinkClick();
            Thread.Sleep(3000);
            managePage.ChangePasswordLink.Click();

            Assert.AreEqual(page.Effect, driver.Url);
            Assert.AreEqual(page.Asserter, managePage.SubmitButton.Displayed.ToString());

        }
    }
}
