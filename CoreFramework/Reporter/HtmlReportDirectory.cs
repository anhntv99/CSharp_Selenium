
using CoreFramework.Utilities;

namespace CoreFramework.Reporter
{
    internal class HtmlReportDirectory
    {
        public static string REPORT_ROOT { get; set; }
        public static string REPORT_FOLDER_PATH { get; set; }
        public static string REPORT_FILE_PATH { get; set; }
        public static string SCREENSHOT_PATH { get; set; }
   
        public static void InitReportDirection()
        {
            string projectPath = FilePath.GetCurrentDirectoryPath();
            REPORT_ROOT = projectPath + "\\Reports";
            REPORT_FOLDER_PATH = REPORT_ROOT + "\\Latest Reports";
            REPORT_FILE_PATH = REPORT_FOLDER_PATH + "\\report.html";
            SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\Screenshot";

            FilePath.CreateIfNotExists(REPORT_ROOT);
            checkExistReportAndRename(REPORT_FOLDER_PATH);
            FilePath.CreateIfNotExists(REPORT_FOLDER_PATH);
            FilePath.CreateIfNotExists(SCREENSHOT_PATH);
         }

        private static void checkExistReportAndRename(String reportFolder)
        {
            if (Directory.Exists(reportFolder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(reportFolder);
                var newPath = REPORT_ROOT + "\\Report_" + dirInfo.CreationTime.ToString().Replace(":", ".").Replace("/", "-");
                Directory.Move(reportFolder, newPath);
            }
        }
    }
}
