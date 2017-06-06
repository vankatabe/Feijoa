using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.CreatePage;
using Blog.UI.Tests.Pages.EditPage;
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
    public class EditPageTests
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
        [Property("Priority", 1), Property("Category", "Regression"), Property("Test scenario number:", 7), Property("Edit test number:", 1)]
        [Description("User Register and Login and Create Article, then navigate to Edit page and enter 'Edited' as Article content, expected: Article content changed to 'Edited'")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void Edit_ChangeContent_ContentChanged()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            EditPage editPage = new EditPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);
            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            Thread.Sleep(1000);
            homePage.GetArticleNumber(uniqId);
            editPage.NavigateTo(editPage.URL + homePage.ArticleNumber);

            editPage.EditArticle(page.ArticleBodyText);
            Thread.Sleep(1000);
            homePage.OpenArticle(uniqId);
            Thread.Sleep(1000);

            editPage.AssertArticleIsEdited(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(EditPageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { editPage, page.Effect });
        }

        [Test]
        [Property("Priority", 2), Property("Category", "Navigation"), Property("Test scenario number:", 10), Property("Navigation test number:", 8)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid data to create article, then navigates to his article and clicks Edit, expected: Navigates to Edit page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Edit_ClickEdit_EditPageLoaded()
        {
            HomePage homePage = new HomePage(this.driver);
            EditPage editPage = new EditPage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name);
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            homePage.OpenArticle(uniqId);
            Thread.Sleep(1000);

            editPage.EditButton.Click();
            Thread.Sleep(1000);
            //Cannot for the life of me make the data driven asserter work here
            Assert.IsTrue(createPage.SubmitButton.Displayed);
        }

        [Test]
        [Property("Priority", 3), Property("Category", "Negative"), Property("Test scenario number:", 7), Property("Edit test number:", 2)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid article title and body, expected: Comment button exists")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Edit_EditArticleFromOtherAuthor_EditUnsuccessful()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            EditPage editPage = new EditPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            homePage.OpenArticle(uniqId);            
            homePage.LogoffLink.Click();
            Thread.Sleep(1000);
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId + "2");
            Thread.Sleep(1000);
            homePage.OpenArticle(uniqId);
            editPage.EditButton.Click();
            Thread.Sleep(1000);

            editPage.AssertUnableToEditMessageExists(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, page.Effect });
        }
    }
}
