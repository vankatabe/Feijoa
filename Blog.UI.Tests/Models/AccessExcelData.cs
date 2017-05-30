using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Models
{
    class AccessExcelData
    {
        public static string TestDataFileConnection()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", string.Empty) + ConfigurationManager.AppSettings["TestDataSheetPath"];
            var filename = "UserData.xlsx";
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;
		                              Data Source = {0}; 
		                              Extended Properties=Excel 12.0;", path + filename);
            // Another way to write it, specifying that the first row of xslx is header and also that mixed-content cell content to be treated appropriately. Still do not put very long strings after row 10 of the xlsx table.
            // var con2 = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties='Excel 12.0; HDR = YES; IMEX = 1;';", path + filename);
            return con;
        }

        public static BlogPages GetTestData(string keyName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format("select * from [DataSet$] where key = '{0}'", keyName); // DataSet - name of the xlsx sheet where our data is
                var value = connection.Query<BlogPages>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }

        public static BlogPages GetNegativeTestData(string keyName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format("select * from [NegativeTests$] where key = '{0}'", keyName); // DataSet - name of the xlsx sheet where our data is
                var value = connection.Query<BlogPages>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }

        public static void WriteTestResult(string keyName, string testStatus)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                OleDbCommand writeResult = new OleDbCommand();
                connection.Open();
                writeResult.Connection = connection;
                string sqlWrite = string.Format("Update [DataSet$] set Status = '{1}' where Key = '{0}'", keyName,testStatus);
                writeResult.CommandText = sqlWrite;
                writeResult.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void WriteNegativeTestResult(string keyName, string testStatus)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                OleDbCommand writeResult = new OleDbCommand();
                connection.Open();
                writeResult.Connection = connection;
                string sqlWrite = string.Format("Update [NegativeTests$] set Status = '{1}' where Key = '{0}'", keyName, testStatus);
                writeResult.CommandText = sqlWrite;
                writeResult.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
