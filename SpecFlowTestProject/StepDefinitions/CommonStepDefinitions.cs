using RestSharp;
using SpecFlowTestProject.Support;

namespace SpecFlowTestProject.StepDefinitions
{
    [Binding]
    public sealed class CommonStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        public CommonStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"that I'm on the ""([^""]*)"" page")]
        public void GivenThatImOnThePage(string url)
        {
            Driver.WebDriver.Navigate().GoToUrl(url);
        }

        [When(@"I wait for ""([^""]*)"" seconds")]
        public void WhenIWaitForSeconds(int timeInSeconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(timeInSeconds));
        }

        [Given(@"that I want to send a request for pricing to ""([^""]*)""")]
        public void GivenThatIWantToSendARequestForPricingTo(string company)
        {
            RestClient client = null;
            RestRequest request = null;

            switch (company)
            {
                case "Maui":
                    client = new RestClient("https://api.aws.thlonline.com");
                    client.Options.MaxTimeout = -1;
                    scenarioContext["RestClient"] = client;
                    
                    request = new RestRequest("/api/2/availability/pricing", Method.Get);
                    request.AddHeader("Accept", "application/json, text/plain, */*");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Authority", "api.aws.thlonline.com");
                    request.AddHeader("origin", "https://booking.maui-rentals.com");
                    request.AddHeader("referer", "https://booking.maui-rentals.com/");
                    request.RequestFormat = DataFormat.Json;
                    scenarioContext["RestRequest"] = request; 
                    break;
                case "Britz":
                    client = new RestClient("https://api.aws.thlonline.com");
                    client.Options.MaxTimeout = -1;
                    scenarioContext["RestClient"] = client;

                    request = new RestRequest("/api/2/availability/pricing", Method.Get);
                    request.AddHeader("Accept", "application/json, text/plain, */*");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Authority", "api.aws.thlonline.com");
                    request.AddHeader("origin", "https://booking.maui-rentals.com");
                    request.AddHeader("referer", "https://booking.maui-rentals.com/");
                    request.RequestFormat = DataFormat.Json;
                    scenarioContext["RestRequest"] = request;
                    break;
                default:
                    break;
            }
        }

    }
}
