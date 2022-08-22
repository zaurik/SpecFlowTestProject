using SpecFlowTestProject.Pages;
using SpecFlowTestProject.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.StepDefinitions
{
    [Binding]
    public sealed class GoogleStepDefinitions
    {
        [Given(@"the region is set to ""([^""]*)""")]
        public void GivenTheRegionIsSetTo(string country)
        {
            Google google = new Google(Driver.WebDriver);

            google.VerifyAndSetRegion(country);
        }

        [When(@"I search for ""([^""]*)""")]
        public void WhenISearchFor(string query)
        {
            Google google = new Google(Driver.WebDriver);

            google.EnterSearchText(query);
            google.SearchByEnterKey();
        }

        [Then(@"I should see a search result linking to the URL ""([^""]*)"" within (.*) pages of search results")]
        [Then(@"I should see a search result linking to the URL ""([^""]*)"" within (.*) page of search results")]
        public void ThenIShouldSeeASearchResultLinkingToTheURL(string url, int numberOfPagesToSearch)
        {
            Google google = new Google(Driver.WebDriver);

            google.AssertThatUrlIsInResults(url, numberOfPagesToSearch);
        }

        [When(@"I click on the search result linking to the URL ""([^""]*)""")]
        public void WhenIClickOnTheSearchResultLinkingToTheURL(string url)
        {
            Google google = new Google(Driver.WebDriver);

            google.ClickOnResultWithLink(url);
        }

    }
}
