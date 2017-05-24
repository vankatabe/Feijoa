﻿using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using Blog.UI.Tests.Pages.LoginPage;
using Blog.UI.Tests.Pages.RegisterPage;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    public class LogoffTests
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;
        private string testStatus = "failed";
        private string uniqId;

        [SetUp]
        public void Init()
        {
            // this.driver = BrowserHost.Instance.Application.Browser; // void
            uniqId = Guid.NewGuid().ToString();
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus); // First, write in xlsx 'failed' against the test
        }

        [TearDown]
        public void CleanUp()
        {
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus);
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
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, "failed"); // First, write in xlsx 'failed' against the test

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            // Assert that User is Logged-in
            // loginPage.AssertGreetingDisplayed("Hello " + page.UniqEmail(uniqId) + '!');
            // Press Logoff link
            homePage.LogoffLink.Click();

            homePage.AssertLoginDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { homePage, page.Effect });
            testStatus = "passed";
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }
    }
}