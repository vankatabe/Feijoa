using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno;
using TestStack.Seleno.Configuration;

namespace Blog.UI.Tests
{
    public static class BrowserHost
    {
        public static readonly SelenoHost Instance = new SelenoHost();
        private static readonly string RootUrl = ConfigurationManager.AppSettings["URL"];

        static BrowserHost()
        {
            // For Firefox:
             Instance.Run("Blog", 60639);
            // For Chrome:
            // Instance.Run("Blog", 60634, w => w.WithRemoteWebDriver(BrowserFactory.Chrome));
            // or:
            // Instance.Run("Blog", 60639, w => w.WithRemoteWebDriver(() => new ChromeDriver()));
        }
    }
}
