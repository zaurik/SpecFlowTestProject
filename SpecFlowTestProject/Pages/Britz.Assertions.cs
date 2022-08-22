using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using SpecFlowTestProject.POCO;
using SpecFlowTestProject.Support;

namespace SpecFlowTestProject.Pages
{
    public partial class Britz
    {
        public void AssertTheResultsCount(int expectedCount)
        {
            Driver.WebDriverWait.WaitUntilElementNotVisible(By.XPath("//*[@class='fa fa-fw fa-refresh fa-spin text-info']"), 30);
            SearchResultsCount.Text.Should().Be($"{expectedCount} Results");
        }

        #region API Tests

        public void AssertThatTheResponseStatusCodeIs(string statusDescription)
        {
            RestResponse response = (RestResponse)scenarioContext["PricingResponse"];
            response.StatusDescription.Trim().ToLower().Should().Be(statusDescription.Trim().ToLower());
        }

        public void AssertTheNumberOfValidSearchResults(int numberOfResults)
        {
            RestResponse response = (RestResponse)scenarioContext["PricingResponse"];

            int resultCount = 0;
            var serialize = JsonConvert.DeserializeObject<List<PricingResponse>>(response.Content);

            foreach (var item in serialize)
            {
                if (item.isAvailable == "true")
                    resultCount++;
            }

            resultCount.Should().Be(numberOfResults);
        }

        #endregion
    }
}
