using AventStack.ExtentReports.MarkupUtils;
using CoreFramework.DriverCore;
using CoreFramework.Utilities;
using Emgu.CV;
using Emgu.CV.Features2D;
using SixLabors.ImageSharp;

namespace CoreFramework.Reporter.ExtentMarkup;

public class ImageLog : IMarkup
{
    private ImageProcessor _imageProcessor;
    private string _imageName;

    public ImageLog(ImageProcessor imageProcessor, string imageName)
    {
        _imageProcessor = imageProcessor;
        _imageName = imageName;
    }

    public string GetMarkup()
    {
        string log = $@"
                    <table>
                        <tr>
                            <p>[{WebDriverAction.GetDateTimeStamp()}] Actual {_imageName} image matches with expected image with similarity rate of [{WebDriverAction.SimilarRate.ToString("##")}%]</p>
                        </tr>
                        <tr>
                            <td>Baseline</td>
                            <td>Actual</td>
                            <td>Difference</td>
                        </tr>
                        <tr>
                            
                            <td>
                                <img class=""r-img"" title="""" onerror=""this.style.display=&quot;none&quot;""    
                                    data-featherlight=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.BaselinePath)}"" 
                                    src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.BaselinePath)}"" 
                                    data-src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.BaselinePath)}"">
                                </td>

                            
                            <td>
                                <img class=""r-img"" title="""" onerror=""this.style.display=&quot;none&quot;"" 
                                    data-featherlight=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.ActualPath)}"" 
                                    src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.ActualPath)}"" 
                                    data-src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.ActualPath)}"">
                            </td>
                            
                            <td>
                                <img class=""r-img"" title="""" onerror=""this.style.display=&quot;none&quot;"" 
                                    data-featherlight=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.MergedPath)}"" 
                                    src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.MergedPath)}"" 
                                    data-src=""{Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, WebDriverAction.MergedPath)}"">
                            </td>
                        </tr>
                    </table>
                    ";                 
        return log;
    }
}



