using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Blog.UI.Tests.SpecFlowTests
{
    [Binding]
    public class OpenHomePageSteps
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;
        private BlogPages page;
        private HomePage homePage;
        private string testStatus = "failed";

        [Given(@"that the Visitor opens a web browser")]
        public void GivenThatTheVisitorOpensAWebBrowser()
        {
            homePage = new HomePage(this.driver);
            page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus); // First, write in xlsx 'failed' against the test
        }

        [When(@"the Visitor navigates to the Online Blog Website address")]
        public void WhenTheVisitorNavigatesToTheOnlineBlogWebsiteAddress()
        {
            homePage.NavigateTo(homePage.URL);
        }

        [Then(@"the Blog Home Page is open")]
        public void ThenTheBlogHomePageIsOpen()
        {
            homePage.AssertHomePageLogoDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            // OR could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { homePage, page.Effect });
            testStatus = "passed";
            AccessExcelData.WriteTestResult(TestContext.CurrentContext.Test.Name, testStatus);
        }
    }
}
