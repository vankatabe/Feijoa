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
    public class LoginPageTests
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
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.Status.ToString()); // Write actual test result status in xlsx
            // The old-style logger for failed tests
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string filenameTxt = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", string.Empty) + ConfigurationManager.AppSettings["Logs"] + TestContext.CurrentContext.Test.Name + ".txt";
                if (File.Exists(filenameTxt))
                {
                    File.Delete(filenameTxt);
                }

                File.WriteAllText(filenameTxt,
                    "Test full name:\t" + TestContext.CurrentContext.Test.FullName + "\r\n\r\n"
                    + "Work directory:\t" + TestContext.CurrentContext.WorkDirectory + "\r\n\r\n"
                    + "Pass count:\t" + TestContext.CurrentContext.Result.PassCount + "\r\n\r\n"
                    + "Result:\t" + TestContext.CurrentContext.Result.Outcome.ToString() + "\r\n\r\n"
                    + "Message:\t" + TestContext.CurrentContext.Result.Message);

                var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot();
                var filenameJpg = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", string.Empty) + ConfigurationManager.AppSettings["Logs"] + TestContext.CurrentContext.Test.Name + ".jpg";
                screenshot.SaveAsFile(filenameJpg, ScreenshotImageFormat.Jpeg);
            }
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 3), Property("Login test number:", 1)]
        [Description("Navigate to Login page web address and enter valid registered User credentials, expected: User logged and greeting displayed")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Login_ValidCredentials_LoginSuccessful()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            homePage.LogoffLink.Click();
            Thread.Sleep(1000);

            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginForm(page, uniqId);

            loginPage.AssertGreetingDisplayed(page.Effect + ' ' + page.UniqEmail(uniqId) + '!');
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(LoginPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { loginPage, page.Effect + ' ' + page.UniqEmail(uniqId) + '!' });
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 3), Property("Login test number:", 2)]
        [Description("Navigate to Login page web address and enter a valid Email but invalid Password, expected: Login unsuccessful and Password field required message")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Login_InvalidPassword_LoginUnsuccessful()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginFormNegative(page);
            Thread.Sleep(1000);

            loginPage.AssertPasswordErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(LoginPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { loginPage, page.Effect });
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 3), Property("Login test number:", 2)]
        [Description("Navigate to Login page web address and enter an ibvalid Email but valid Password, expected: Login unsuccessful and Password field required message")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Login_Blank_Email_Login_Unsuccessful()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginFormNegative(page);
            Thread.Sleep(1000);

            loginPage.AssertEmailErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(LoginPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { loginPage, page.Effect });
        }
    }
}
