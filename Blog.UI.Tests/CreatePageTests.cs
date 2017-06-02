using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.CreatePage;
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
    public class CreatePageTests
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
        [Property("Priority", 1), Property("Test scenario number:", 4), Property("Create test number:", 1)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid data to create article, expected: Article created")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Create_ValidFieldEntries_ArticleCreated()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            homePage.LogoffLink.Click(); // To assure that the new Article is visible even for non-logged user
            homePage.OpenArticle(uniqId); // To assure it is possible the article to be opened

            createPage.AssertArticleIsDisplayed(homePage.ArticleTitle(uniqId), page.Effect + uniqId);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, homePage.ArticleTitle(uniqId), page.Effect + uniqId });
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 4), Property("Create test number:", 1)]
        [Description("User Register and Login, then navigate to Create page web address and clicks cancel button, expected: Redirected to Home page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Create_CreateArticleCancelButton_NavigateToHomePage()
        {
            LoginPage loginPage = new LoginPage(this.driver);
            RegisterPage regPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name);

            regPage.NavigateTo(regPage.URL);
            regPage.FillRegistrationForm(page, uniqId);

            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginForm(page, uniqId);
            Thread.Sleep(1000);

            CreatePage createPage = new CreatePage(this.driver);
            createPage.NavigateTo(createPage.URL);
            createPage.CancelButtonClick();
            Thread.Sleep(2000);

            Assert.AreEqual(page.Effect, driver.Url);
        }        
    }
}
