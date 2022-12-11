using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using CoreFramework.APICore;
using NUnit.Framework;
using CoreFramework.Reporter.ExtentMarkup;

namespace CoreFramework.Reporter
{
    internal class HtmlReport 
    {
        private static int testCaseIndex;
        private static ExtentReports _report;
        private static ExtentTest extentTestSuite;
        private static ExtentTest extentTestCase;

        public static ExtentReports createReport()
        {
            if (_report == null)
            {
                _report = createInstance();
            }
            return _report;
        }

        private static ExtentReports createInstance()
        {
            HtmlReportDirectory.InitReportDirection();
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(HtmlReportDirectory.REPORT_FILE_PATH);
            htmlReporter.Config.DocumentTitle = "Automation Test Report";
            htmlReporter.Config.ReportName = "Automation Test Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.Encoding = "UTF-8";

            ExtentReports report = new ExtentReports();
            report.AttachReporter(htmlReporter);
            return report;
        }

        public static void flush()
        {
            if (_report != null)
            {
                _report.Flush();
            }
        }

        public static ExtentTest createTest(string className, string classDescreption = "")
        {
            if (_report == null)
            {
                _report = createInstance();
            }
            extentTestSuite = _report.CreateTest(className, classDescreption);
            return extentTestSuite;
        }

        public static ExtentTest createNode(string className, string testcase, string classDescreption = "")
        {
            if (extentTestSuite == null)
            {
                extentTestSuite = createTest(className);
            }
            extentTestCase = extentTestSuite.CreateNode(testcase, classDescreption);
            return extentTestCase;
        }



        public static ExtentTest GetParent()
        {
            return extentTestSuite;
        }

        public static ExtentTest GetNode()
        {
            return extentTestCase;
        }

        public static ExtentTest GetTest()
        {
            if (GetNode() == null)
            {
                return GetParent();
            }
            return GetNode();
        }

        public static void Pass(string des)
        {
            GetTest().Pass(des);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des)
        {
            GetTest().Fail(des);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des, string path)
        {
            GetTest().Fail(des).AddScreenCaptureFromPath(path);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des, string ex, string path)
        {
            GetTest().Fail(des).AddScreenCaptureFromPath(path);
            TestContext.WriteLine(des);
        }

        public static void Warning(string des)
        {
            GetTest().Warning(des);
            TestContext.WriteLine(des);
        }

        public static void Skip(string des)
        {
            GetTest().Skip(des);
            TestContext.WriteLine(des);
        }

        public static void CreateAPIRequestLog(APIRequest request, APIResponse response)
        {
            GetTest().Info(MarkupHelperPlus.CreateAPIRequestLog(request, response));
        }

        public static void MarkUpPassJson(string responseBody)
        {
            var json = "{'foo' : 'bar', 'foos' : ['b','a','r'], 'bar' : {'foo' : 'bar' , 'bar' : false, 'foobar':1234}}";
            GetTest().Info(MarkupHelper.CreateCodeBlock(json, CodeLanguage.Json));
        }

        public static void MarkUpTable()
        {
            string[][] someInts = new string[][] { new string[] { "<label> HAHAHA <label>" }, new string[] { } };
            var m = MarkupHelper.CreateTable(someInts);
            GetTest().Info(m);
        }

        public static void MarkUpLabel()
        {
            var text = "extent";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Blue);
            GetTest().Info(m);
        }

    }
}
