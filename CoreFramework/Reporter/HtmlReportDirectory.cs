using CoreFramework.Utilities;

namespace CoreFramework.Reporter;
public class HtmlReportDirectory
{
    public static string REPORT_ROOT { get; set; }
    public static string REPORT_FOLDER_PATH { get; set; }
    public static string REPORT_FILE_PATH { get; set; }
    public static string SCREENSHOT_PATH { get; set; }    
    public static string ACTUAL_SCREENSHOT_PATH { get; set; }
    public static string DIFFERENCE_SCREENSHOT_PATH { get; set; }
    public static string BASELINE_SCREENSHOT_PATH { get; set; }
    public static string MERGED_IMAGE_PATH { get; set; }
    public static string DIFFERENCE_POINTS_PATH { get; set; }
    public static string BASELINE_IMAGE_PATH { get; set; }
    public static string ACTUAL_IMAGE_PATH { get; set; }


    public static void InitReportDirection()
    {
        string projectPath = FilePaths.GetCurrentDirectoryPath();
        REPORT_ROOT = projectPath + "\\reports";
        REPORT_FOLDER_PATH = REPORT_ROOT + "\\latest reports";
        REPORT_FILE_PATH = REPORT_FOLDER_PATH + "\\report.html";
        SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\screenshot";

        FilePaths.CreateIfNotExists(REPORT_ROOT);
        CheckExistReportAndRename(REPORT_FOLDER_PATH);
        FilePaths.CreateIfNotExists(REPORT_FOLDER_PATH);
        FilePaths.CreateIfNotExists(SCREENSHOT_PATH);
       
    }
    private static void CheckExistReportAndRename(string reportFolder)
    {
        if (Directory.Exists(reportFolder))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(reportFolder);
            var newPath = REPORT_ROOT + "\\report_" + dirInfo.CreationTime.
                ToString().Replace(":", ".").Replace("/", "-");
            Directory.Move(reportFolder, newPath);
        }
    }

    public static void InitImageReport(string browsername, int browserwidth, int browserheight, string imagename)
    {
        string browserSize = browserwidth.ToString() + "x" + browserheight.ToString();

        ACTUAL_SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\actual";
        DIFFERENCE_SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\difference";
        BASELINE_SCREENSHOT_PATH = FilePaths.GetCurrentDirectoryPath() + "\\resource\\baseline";

        FilePaths.CreateIfNotExists(ACTUAL_SCREENSHOT_PATH);
        FilePaths.CreateIfNotExists(DIFFERENCE_SCREENSHOT_PATH);
        FilePaths.CreateIfNotExists(BASELINE_SCREENSHOT_PATH);

        MERGED_IMAGE_PATH = DIFFERENCE_SCREENSHOT_PATH + "\\" + browsername + "\\" + browserSize
           + "\\" + imagename + "_merged.png";
        BASELINE_IMAGE_PATH = BASELINE_SCREENSHOT_PATH + "\\" + browsername + "\\" + browserSize
            + "\\" + imagename + "_baseline.png";
        ACTUAL_IMAGE_PATH = ACTUAL_SCREENSHOT_PATH + "\\" + browsername + "\\" + browserSize
            + "\\" + imagename + "_actual.png";



        FilePaths.CreateIfNotExists(MERGED_IMAGE_PATH + "\\..");
        FilePaths.CreateIfNotExists(ACTUAL_IMAGE_PATH + "\\..");
        FilePaths.CreateIfNotExists(BASELINE_IMAGE_PATH + "\\..");
    }
}

