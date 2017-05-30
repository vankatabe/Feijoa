using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
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
    public class RegisterPageTests
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;
        //private string testStatus = "failed";
        private string uniqId;
        


        [SetUp]
        public void Init()
        {
            // this.driver = BrowserHost.Instance.Application.Browser; // void
            uniqId = Guid.NewGuid().ToString();
            //AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus); // First, write in xlsx 'failed' against the test
        }

        [TearDown]
        public void CleanUp()
        {
            //This inserts the status of the latest Test build into the UserData.xlsx file
            AccessExcelData.WriteNegativeTestResult(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.Status.ToString());
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.Status.ToString());
            //AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus);
            // driver.Quit(); // causes Firefox to crash
            // The old-style logger for failed tests
            //    if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            //    {
            //        string filenameTxt = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", string.Empty) + ConfigurationManager.AppSettings["Logs"] + TestContext.CurrentContext.Test.Name + ".txt";
            //        if (File.Exists(filenameTxt))
            //        {
            //            File.Delete(filenameTxt);
            //        }
            //        File.WriteAllText(filenameTxt,
            //            "Test full name:\t" + TestContext.CurrentContext.Test.FullName + "\r\n\r\n"
            //            + "Work directory:\t" + TestContext.CurrentContext.WorkDirectory + "\r\n\r\n"
            //            + "Pass count:\t" + TestContext.CurrentContext.Result.PassCount + "\r\n\r\n"
            //            + "Result:\t" + TestContext.CurrentContext.Result.Outcome.ToString() + "\r\n\r\n"
            //            + "Message:\t" + TestContext.CurrentContext.Result.Message);
            //
            //        var screenshot = ((ITakesScreenshot)this.driver).GetScreenshot();
            //        var filenameJpg = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", string.Empty) + ConfigurationManager.AppSettings["Logs"] + TestContext.CurrentContext.Test.Name + ".jpg";
            //        screenshot.SaveAsFile(filenameJpg, ScreenshotImageFormat.Jpeg);
            //    }
            //
        }

        [Test]
        [Property("Setup negative tests account", 0)]
        [Author("Mario Georgiev")]
        [Description("Sets up the test account for our negative tests in case it is not present in DB")]
        public void Setup_Negative_Test_Account()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetNegativeTestData(TestContext.CurrentContext.Test.Name);

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
            //testStatus = "passed";
            //AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 2), Property("Registration test number:", 2)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Email field empty, exected: Account not registered and Email field required message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_Email()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetNegativeTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed.
            registerPage.AssertEmailErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect});
            //testStatus = "passed";
            //AccessExcelData.WriteNegativeTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 2), Property("Registration test number:", 3)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Full Name field empty, exected: Account not registered and Full Name field required message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_FullName()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetNegativeTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed.
            registerPage.AssertFullNameErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
            //testStatus = "passed";
            //AccessExcelData.WriteNegativeTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 2), Property("Registration test number:", 4)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Passwordd field empty, exected: Account not registered and Password field required message displayed, and Password missmatch message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_Password()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetNegativeTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed.
            registerPage.AssertPasswordErrorMessageExists(page.Effect);
            registerPage.AssertPasswordMissmatchErrorMessageExists(page.Effect2);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            MethodInfo asserter2 = typeof(RegisterPageAsserter).GetMethod(page.Asserter2);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
            asserter2.Invoke(null, new object[] { registerPage, page.Effect2 });
            //testStatus = "passed";
            //AccessExcelData.WriteNegativeTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 2), Property("Registration test number:", 5)]
        [Description("Navigate to Registration page web address and popualte fields with valid data while leaving Confirm Passwordd field empty, exected: Account not registered and Password missmatch message displayed")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Register_Without_ConfirmPassword()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetNegativeTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed.
            registerPage.AssertPasswordMissmatchErrorMessageExists2(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);           
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });            
            //testStatus = "passed";
            //AccessExcelData.WriteNegativeTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }
    }
}
