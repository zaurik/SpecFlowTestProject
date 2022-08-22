using OpenQA.Selenium;

namespace SpecFlowTestProject.Pages
{
    public partial class Google
    {
        private IWebElement RegionOnSearchPage => _webDriver.FindElement(By.XPath("/html/body/div[1]/div[5]/div[1]"));

        private IWebElement SettingsOnSearchPage => 
            _webDriver.FindElement(By.XPath("/html/body/div[1]/div[5]/div[2]/div[2]/span/span/g-popup/div[1]/div"));

        private IWebElement SearchSettingsItem =>
            _webDriver.FindElement(By.XPath("//*[@id=\"lb\"]/div/g-menu/g-menu-item[1]/div/a"));

        private IWebElement RegionSettingsShowMoreLink => _webDriver.FindElement(By.Id("regionanchormore"));

        private IWebElement NewZealandRegionOption => _webDriver.FindElement(By.Id("regionoNZ"));

        private IWebElement SearchSettingSaveButton => _webDriver.FindElement(By.XPath("//*[@id='form-buttons']/div[1]"));

        private IWebElement SearchBox => _webDriver.FindElement(By.Name("q"));

        private IList<IWebElement> SearchResults => (_webDriver.FindElement(By.Id("rso"))).FindElements(By.XPath(".//div[@class='yuRUbf']"));
    
        IWebElement NextPageLink => _webDriver.FindElement(By.Id("pnnext"));
    }
}
