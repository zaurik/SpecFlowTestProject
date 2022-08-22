using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SpecFlowTestProject.Support;
using System.Security.Policy;

namespace SpecFlowTestProject.Pages
{
    public partial class Google
    {
        private IWebDriver _webDriver;

        public Google(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void VerifyAndSetRegion(string country)
        {
            if (RegionOnSearchPage.Text != country)
            {
                SettingsOnSearchPage.Click();
                SearchSettingsItem.Click();
                RegionSettingsShowMoreLink.Click();
                NewZealandRegionOption.Click();
                SearchSettingSaveButton.Click();
                _webDriver.SwitchTo().Alert().Accept();
            }
        }

        public void EnterSearchText(string query)
        {
            SearchBox.SendKeys(query);
        }

        public void SearchByEnterKey()
        {
            Actions actionBuilder = new Actions(_webDriver);
            actionBuilder.SendKeys(Keys.Enter);
            actionBuilder.Perform();
        }

        public void ClickOnResultWithLink(string expectedUrl)
        {
            IWebElement webElement = GetResultWithLink(expectedUrl);
           
            if (webElement != null)
            {
                webElement.Click();
            }
            else
            {
                throw new NullReferenceException("There isn't a result with the expected link.");
            }
        }

        private IWebElement GetResultWithLink(string expectedUrl)
        {
            List<IWebElement> searchResults = SearchResults.ToList();
            List<string> pageUrls = new List<string>();
            IWebElement anchor = null;

            foreach (IWebElement result in searchResults)
            {
                anchor = result.FindElement(By.XPath(".//a"));
                string url = anchor.GetAttribute("href");

                //Remove the protocol from URL
                if (url.StartsWith("https"))
                {
                    url = url.Substring("https://".Length);
                }
                else
                {
                    url = url.Substring("http://".Length);
                }

                // Remove the "www."
                if (url.StartsWith("www."))
                {
                    url = url.Substring("www.".Length);
                }

                // Removes the ending '/'
                if (url.EndsWith("/"))
                {
                    url = url.Remove(url.Length - 1);
                }

                if (url.Trim() == expectedUrl.Trim())
                {
                    break;
                }
                else
                {
                    anchor = null;
                }
            }

            return anchor;
        }
    }
}
