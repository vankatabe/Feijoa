using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.CreatePage;
using Blog.UI.Tests.Pages.HomePage;
using Blog.UI.Tests.Pages.LoginPage;
using Blog.UI.Tests.Pages.ManagePage;
using Blog.UI.Tests.Pages.RegisterPage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
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
    public class HomePageTests
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
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog web address, expected: Blog homepage open and Logo present")]
        [Author("vankatabe")]
        [LogResultToFileAttribute]
        public void CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage()
        {
            HomePage homePage = new HomePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file

            homePage.NavigateTo(homePage.URL);

            homePage.AssertHomePageLogoDisplayed(page.Effect);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { homePage, page.Effect });
        }
        //TODO: change property values
        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog web address, and click Log in, expected: navigates to Log in page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Home_ClickLogin_LoginLoaded()
        {
            HomePage homePage = new HomePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file
            // homePage.LogoffLink.Click(); // Make sure User is Logged-off

            homePage.NavigateTo(homePage.URL);
            Thread.Sleep(1000);
            homePage.LoginLink.Click();
            Thread.Sleep(1000);
                        
            homePage.AssertLoginPageUrl(this.driver, page.Effect);
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            asserter.Invoke(null, new object[] { homePage, this.driver, page.Effect  });

        }
        //TODO: change property values
        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog web address, and click Register, expected: navigates to Register page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Home_ClickRegister_RegisterLoaded()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage regPage = new RegisterPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file
            // homePage.LogoffLink.Click(); // Make sure User is Logged-off

            homePage.NavigateTo(homePage.URL);
            Thread.Sleep(1000);
            homePage.OpenRegistrationPage();
            Thread.Sleep(1000);

            homePage.AssertRegisterPageUrl(this.driver, page.Effect);
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            asserter.Invoke(null, new object[] { homePage, this.driver, page.Effect });
        }               

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 4), Property("Create test number:", 1)]
        [Description("User Register and Login, then navigate to Create page web address and enter valid data to create article, then navigates to his article, expected: Navigates to own article")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Home_CreateArticle_ReadArticle()
        {
            HomePage homePage = new HomePage(this.driver);
            RegisterPage registerPage = new RegisterPage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            CreatePage createPage = new CreatePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name);
            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);

            createPage.NavigateTo(createPage.URL);
            createPage.CreateArticle(page, uniqId);
            Thread.Sleep(1000);
            homePage.OpenArticle(uniqId);
            Thread.Sleep(1000);

            createPage.AssertArticleIsDisplayed(homePage.ArticleTitle(uniqId), page.Effect + uniqId);
            // for the DataDriven Asserter:
            MethodInfo asserter = typeof(CreatePageAsserter).GetMethod(page.Asserter);
            // could be also like next row - Effect - from the Effect column in the Excel file - what message or effect are we expecting
            asserter.Invoke(null, new object[] { createPage, homePage.ArticleTitle(uniqId), page.Effect + uniqId });
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 6), Property("Logoff test number:", 1)]
        [Description("Register with valid User credentials where User Logs-in automatically and press his Account Name link, expected: User navigates to Manage page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Manage_ClickAccount_NavigateToManagePage()
        {
            RegisterPage registerPage = new RegisterPage(this.driver);
            ManagePage managePage = new ManagePage(this.driver);
            LoginPage loginPage = new LoginPage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = Login_UniqueCredentials_LoginSuccessful) and use it as a Key in the xlsx file

            registerPage.NavigateTo(registerPage.URL);
            registerPage.FillRegistrationForm(page, uniqId);
            Thread.Sleep(1000);
            loginPage.NavigateTo(loginPage.URL);
            loginPage.FillLoginForm(page, uniqId);
            Thread.Sleep(1000);
            loginPage.ManageLink.Click();
            Thread.Sleep(1000);
            //cannot for the life of me make the data driven asserter work here.
            Assert.AreEqual(page.Effect, driver.Url);
            Assert.AreEqual(page.Effect2, managePage.ChangePasswordLink.Text);
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog web address, and click the Logo, expected: navigates to Home page")]
        [Author("Mario Georgiev")]
        [LogResultToFileAttribute]
        public void Home_LogoClick_HomePageLoaded()
        {
            HomePage homePage = new HomePage(this.driver);
            BlogPages page = AccessExcelData.GetTestData(TestContext.CurrentContext.Test.Name); // Get the current test method name (TestContext.CurrentContext.Test.Name = CheckWebSiteLoad_EnterBlogURL_OpenBlogHomePage) and use it as a Key in the xlsx file
            // homePage.LogoffLink.Click(); // Make sure User is Logged-off
            homePage.NavigateTo(homePage.URL);
            Thread.Sleep(1000);
            homePage.ClickLogo();
            Thread.Sleep(1000);

            homePage.AssertHomePageUrl(this.driver, page.Effect);
            MethodInfo asserter = typeof(HomePageAsserter).GetMethod(page.Asserter);
            asserter.Invoke(null, new object[] { homePage, this.driver, page.Effect });
        }
    }
}