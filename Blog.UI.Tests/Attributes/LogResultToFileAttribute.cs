using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace Blog.UI.Tests.Attributes
{
    public class LogResultToFileAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
        }

        public void AfterTest(ITest test)
        {
            var message = GetLogMessage(test.Name, TestContext.CurrentContext.Result);
            /*   to clear the file's contents after each test:
              if (File.Exists(LogFilePath))
              {
                  File.Delete(LogFilePath);
              } 
            */
            File.AppendAllText(LogFilePath, message);
        }

        public ActionTargets Targets { get; }

        protected virtual string LogFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Logs\\Log.txt");

        private string GetLogMessage(string testName, TestContext.ResultAdapter result)
        {
            switch (result.Outcome.Status)
            {
                case TestStatus.Passed:
                case TestStatus.Inconclusive:
                case TestStatus.Skipped:
                    return CreateLogMessage(testName, result.Outcome.Status);
                case TestStatus.Failed:
                    return CreateLogMessageForFailedTest(testName, result.Message);
                default:
                    return "";
            }
        }

        private string CreateLogMessage(string testName, TestStatus status)
        {
            return string.Format("[{0:s}] [{1}] {2}{3}", DateTime.Now, status, testName, Environment.NewLine);
        }

        private string CreateLogMessageForFailedTest(string testName, string message)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("[{0:s}] [{1}] {2}", DateTime.Now, TestStatus.Failed, testName);
            builder.AppendLine();
            builder.AppendLine(message);

            return builder.ToString();
        }
    }
}
