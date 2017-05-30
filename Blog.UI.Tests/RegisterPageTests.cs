using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
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
    public class RegisterPageTests
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
        [Property("Setup negative tests account", 0)]
        [Author("Mario Georgiev")]
        [Description("Sets up the test account for our negative tests in case it is not present in DB")]
        public void Setup_Negative_Test_Account()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name);

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 2), Property("Registration test number:", 1)]
        [Description("Navigate to Registration page web address and populate fields with valid input, expected: Account registered and User Logged automatially")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Register_UniqueCredentials_RegisterSuccessful()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);

            registerPage.AssertGreetingDisplayed(page.Effect + ' ' + page.UniqEmail(uniqId) + '!');
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect + ' ' + page.UniqEmail(uniqId) + '!' });
        }        
    }
}
