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
        [Property("Priority", 1), Property("Category", "Regression"), Property("Test scenario number:", 4), Property("Create test number:", 1)]
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
            Thread.Sleep(1000);

            createPage.AssertArticleIsDisplayed(homePage.ArticleTitle(uniqId), page.Effect + uniqId);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, homePage.ArticleTitle(uniqId), page.Effect + uniqId });
        }

        [Test]
        [Property("Priority", 2), Property("Category", "Navigation"), Property("Test scenario number:", 10), Property("Navigation test number:", 5)]
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

            createPage.AssertHomePageUrl(this.driver, page.Effect);
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            asserter.Invoke(null, new object[] { createPage, this.driver, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 4), Property("Create test number:", 2)]
        [Description("User Register and Login, then navigate to Create page web address and enter invalid article title but valid body, expected: Article not created")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Create_ArticleWIthoutTitle_ArticleNotCreated()
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
            createPage.Content.SendKeys(uniqId + ' ' + page.ArticleBodyText);
            createPage.SubmitButton.Click();
            Thread.Sleep(1000);

            createPage.AssertTitleErrorMessageDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 4), Property("Create test number:", 3)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid article title but invalid body, expected: Article not created")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Create_ArticleWithoutContent_ArticleNotCreated()
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
            createPage.Title.SendKeys("Article" + uniqId);
            createPage.SubmitButton.Click();
            Thread.Sleep(1000);

            createPage.AssertContentErrorMessageDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 4), Property("Create test number:", 4)]
        [Description("User Register and Login, then navigate to Create page web address and enter article title exceeding 50 characters but valid body, expected: Article not created")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Create_ArticleWithTitleOutsideRange_ArticleNotCreated()
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
            createPage.Title.SendKeys("Article" + uniqId + page.ArticleBodyText);
            createPage.Content.SendKeys(uniqId + ' ' + page.ArticleBodyText);
            createPage.SubmitButton.Click();
            Thread.Sleep(1000);

            createPage.AssertTitleOutsideRangeErrorMessageDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }

        [Test]
        [Property("Priority", 2), Property("Category", "Navigation"), Property("Test scenario number:", 10), Property("Navigation test number:", 10)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid article title and body, expected: Comment button exists")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Comment_CommentArticle_CommentButtonExists()
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
            homePage.NavigateTo(homePage.URL);
            homePage.OpenArticle(uniqId); // To assure it is possible the article to be opened
            Thread.Sleep(1000);

            createPage.AssertCommentButtonExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }

        [Test]
        [Property("Priority", 2), Property("Category", "Navigation"), Property("Test scenario number:", 10), Property("Navigation test number:", 11)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid article title and body, expected: Comment button exists")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Create_NavigateToArticle_ArticleAuthorExists()
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
            homePage.NavigateTo(homePage.URL);
            homePage.OpenArticle(uniqId); // To assure it is possible the article to be opened
            Thread.Sleep(1000);

            createPage.AssertArticleAuthorExists(page.Effect + "" + uniqId);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 5), Property("Comment test number:", 1)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid article title and body, expected: Comment button exists")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Comment_CommentAnArticle_CommentCreated()
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
            homePage.NavigateTo(homePage.URL);
            homePage.OpenArticle(uniqId); // To assure it is possible the article to be opened
            createPage.CommentButton.Click();
            createPage.CommentField.SendKeys("Test Comment");
            createPage.SubmitCommentButton.Click();
            Thread.Sleep(1000);

            createPage.AssertCommentButtonExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }
    }      
}
