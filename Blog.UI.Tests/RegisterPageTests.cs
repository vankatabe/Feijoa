using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
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
        [Property("Priority", 1), Property("Category", "Regression"), Property("Test scenario number:", 2), Property("Registration test number:", 1)]
        [Description("Navigate to Registration page web address and populate fields with valid input, expected: Account registered and User Logged automatially")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Register_UniqueCredentials_RegisterSuccessful()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            registerPage.AssertGreetingDisplayed(page.Effect + ' ' + page.UniqEmail(uniqId) + '!');
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect + ' ' + page.UniqEmail(uniqId) + '!' });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 2), Property("Registration test number:", 2)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Email field empty, exected: Account not registered and Email field required message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_Email()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed

            registerPage.AssertEmailErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 2), Property("Registration test number:", 3)]
        [Description("Navigate to Registration page web address and populate fields with valid data while leaving Full Name field empty, exected: Account not registered and Full Name field required message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_FullName()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed

            registerPage.AssertFullNameErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 2), Property("Registration test number:", 4)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Passwordd field empty, exected: Account not registered and Password field required message displayed, and Password missmatch message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_Password()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed

            registerPage.AssertPasswordErrorMessageExists(page.Effect);
            registerPage.AssertPasswordMissmatchErrorMessageExists(page.Effect2);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            MethodInfo asserter2 = typeof(RegisterPageAsserter).GetMethod(page.Asserter2);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
            asserter2.Invoke(null, new object[] { registerPage, page.Effect2 });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 2), Property("Registration test number:", 5)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Confirm Passwordd field empty, exected: Account not registered and Password missmatch message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_ConfirmPassword()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed

            registerPage.AssertPasswordMissmatchErrorMessageExists2(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
        }
    }
}
