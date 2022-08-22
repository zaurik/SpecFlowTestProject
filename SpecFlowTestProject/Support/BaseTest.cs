using OpenQA.Selenium;
using SpecFlowTestProject.Support.Enums;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SpecFlowTestProject.Support
{
    public class BaseTest
    {
        private const string screenShotSuffix = ".png";

        private readonly ScenarioContext _scenarioContext;

        public BaseTest(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public static void InitialiseBrowser()
        {
            Driver.StartBrowser(BrowserTypes.Chrome);
        }

        public static void TakeScreenshot()
        {
            try
            {
                string name = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "-");
                string cleanName = Regex.Replace(name, "[^A-Za-z]", "-");

                if (!Directory.Exists(ConfigurationManager.AppSettings["ScreenshotFolderPath"]))
                {
                    DirectoryInfo dirInfo =
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["ScreenshotFolderPath"]);
                }
                string fileName = 
                    ConfigurationManager.AppSettings["ScreenshotFolderPath"] + cleanName 
                    + "_" + DateTime.Now.ToString("MM-dd-yyyy-hh-mmtt") + screenShotSuffix;
                //If there is an alert present we have to close it in order to take a screen shot
                if (Driver.WebDriver.IsAlertPresent())
                {
                    //Switch to the Alert and click Ok button
                    Driver.WebDriver.SwitchTo().Alert().Accept();
                }

                Screenshot screenshot = ((ITakesScreenshot)Driver.WebDriver).GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);

            }
            catch (Exception)
            {
                throw new Exception("Failed to create screenshot");
            }
        }

        public static void CloseBrowser()
        {
            Driver.StopBrowser();
            Debug.WriteLine("Browser has been closed.");
        }
    }
}
