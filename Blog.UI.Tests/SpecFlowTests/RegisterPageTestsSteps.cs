using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using Blog.UI.Tests.Pages.RegisterPage;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace Blog.UI.Tests
{
    [Binding]
    [LogResultToFileAttribute]
    public class RegisterPageTestsSteps
    {
        private static IWebDriver driver = BrowserHost.Instance.Application.Browser;
        RegisterPage registerPage = new RegisterPage(driver);
        BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Register_UniqueCredentials_RegisterSuccessful) and use it as a Key in the xlsx file

        [Given(@"I am on the Registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            registerPage.NavigateTo(registerPage.URL);
        }

        [When(@"I fill-in the registration form is ""(.*)""")]
        public void WhenIFill_InTheRegistrationFormWithoutIs(string testName)
        {
            registerPage.FillRegistrationFormNegative(page);
            Thread.Sleep(1000); // Necessary due to test execution speed
        }

        [Then(@"Message should be displayed is ""(.*)""")]
        public void ThenErrorMessageForNamesShouldBeDisplayedIs(string errorMessage)
        {
            registerPage.AssertEmailErrorMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(RegisterPageAsserter).GetMethod(page.Asserter);
            //could be also like next row -Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { registerPage, page.Effect });
        }
    }
}
