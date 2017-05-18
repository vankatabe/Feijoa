using Blog.UI.Tests.Attributes;
using Blog.UI.Tests.Models;
using Blog.UI.Tests.Pages.HomePage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    public class HomePageTests
    {
        private IWebDriver driver = BrowserHost.Instance.Application.Browser;

        [SetUp]
        public void Init()
        {
            // this.driver = BrowserHost.Instance.Application.Browser; // void
        }

        [TearDown]
        public void CleanUp()
        {
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
            //driver.Quit(); // causes Firefox to crash
        }

        [Test]
        [Property("Priority", 1), Property("Test scenario number:", 9), Property("Homepage test number:", 1)]
        [Description("Navigate to Blog webb address, expected: Blog homepage open and Logo present")]
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

       
    }
}