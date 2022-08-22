using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlowTestProject.Support.Enums;

namespace SpecFlowTestProject.Support
{
    public static class Driver
    {
        static WebDriverWait driverWait;
        static IWebDriver webDriver;

        public static IWebDriver WebDriver
        {
            get
            {
                if (webDriver == null)
                {
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method Start.");
                }

                return webDriver;
            }
            set
            {
                webDriver = value;
            }
        }

        public static WebDriverWait WebDriverWait
        {
            get
            {
                if (driverWait == null || webDriver == null)
                {
                    throw new NullReferenceException("The WebDriver browser wait instance was not initialized. You should first call the method Start.");
                }

                return driverWait;
            }

            set
            {
                driverWait = value;
            }
        }

        public static void StartBrowser(BrowserTypes browser, int defaultTimeOutSeconds = 45)
        {
            switch (browser)
            {
                case BrowserTypes.Firefox:
                    throw new NotImplementedException("The driver for Firefox has not been set.");
                case BrowserTypes.Chrome:
                    WebDriver =
                        new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions());
                    break;
                case BrowserTypes.HeadlessChrome:
                    WebDriver =
                        new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions());
                    break;
                default:
                    throw new NotSupportedException("The browser has not been specified.");
            }

            WebDriver.Manage().Window.Maximize();
            WebDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(defaultTimeOutSeconds));
        }

        public static void StopBrowser()
        {
            WebDriver.Quit();
            WebDriver = null;
            WebDriverWait = null;
        }
    }

    public static class WebDriverExtensions
    {
        public static Boolean IsAlertPresent(this IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static class WebDriverWaitExtensions
    {
        //returns as soon as element is not visible, or throws WebDriverTimeoutException
        public static void WaitUntilElementNotVisible(this WebDriverWait wait, By searchElementBy, int timeoutInSeconds)
        {
            new WebDriverWait(Driver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds))
                            .Until(drv => !IsElementVisible(searchElementBy));
        }

        private static bool IsElementVisible(By searchElementBy)
        {
            try
            {
                return Driver.WebDriver.FindElement(searchElementBy).Displayed;

            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
