using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.Pages
{
    public partial class Google
    {
        public void AssertThatUrlIsInResults(string expectedUrl, int numberOfPagesToSearch)
        {
            IWebElement webElement = null;

            for (int i = 0; i < numberOfPagesToSearch; i++)
            {
                webElement = GetResultWithLink(expectedUrl);

                if (webElement == null)
                {
                    NextPageLink.Click();
                }
                else
                {
                    break;
                }
            }

            webElement
                .Should()
                .NotBeNull($"there should be a result with the link to {expectedUrl} within the first {numberOfPagesToSearch} pages of search results.");
        }
    }
}
