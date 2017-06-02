using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.CreatePage;
using Blog.UI.Tests.Pages.DeletePage;
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
    public class DeletePageTests
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
        [Property("Priority", 1), Property("Test scenario number:", 8), Property("Delete test number:", 1)]
        [Description("User Register and Login and Create Article, then navigate to Delete page and click 'Delete' button, expected: Article Deleted from Article List")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Delete_DeleteArticle_ArticleDeteted()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            DeletePage deletePage = new DeletePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);
            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            Thread.Sleep(1000);
            homePage.GetArticleNumber(uniqId);
            deletePage.NavigateTo(deletePage.URL + homePage.ArticleNumber);

            deletePage.DeleteArticle();
            Thread.Sleep(1000);

            homePage.AssertArticleIsDeleted(uniqId);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 8), Property("Delete test number:", 1)]
        [Description("User Register and Login and Create Article, then navigate to Delete page and click 'Delete' button, expected: Article Deleted from Article List")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Delete_ClickDeleteButton_ConfirmationPageLoaded()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            DeletePage deletePage = new DeletePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            Thread.Sleep(1000);

            homePage.GetArticleNumber(uniqId);
            deletePage.NavigateTo(deletePage.URL + homePage.ArticleNumber);            
            Thread.Sleep(1000);

            Assert.AreEqual(page.Asserter, deletePage.DeleteButton.Displayed.ToString());
        }
    }
}
