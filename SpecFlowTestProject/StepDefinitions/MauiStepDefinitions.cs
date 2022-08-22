using SpecFlowTestProject.Pages;
using SpecFlowTestProject.POCO;
using SpecFlowTestProject.Support;
using SpecFlowTestProject.Support.Enums;

namespace SpecFlowTestProject.StepDefinitions
{
    [Binding]
    public sealed class MauiStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public MauiStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I select the destination as ""([^""]*)""")]
        public void WhenISelectTheDestinationAs(string destination)
        {
            Maui maui = new Maui(Driver.WebDriver);

            maui.SelectTheDestinationOnMainPage(destination);
        }

        [When(@"I set the (Pick Up Date|Drop Off Date|Pick Up Location|Drop Off Location|Drivers License|Promo Code) as ""([^""]*)""")]
        public void WhenISetTheSearchValueAs(string field, string value)
        {
            Maui maui = new Maui(Driver.WebDriver);

            switch (field)
            {
                case "Pick Up Date":
                    string[] pickUpDate = value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    maui.SetPickUpDate(
                        new DateTime(Convert.ToInt32(pickUpDate[2]),
                        Convert.ToInt32(pickUpDate[1]),
                        Convert.ToInt32(pickUpDate[0])));
                    break;
                case "Drop Off Date":
                    string[] dropOffDate = value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    maui.SetPickUpDate(
                        new DateTime(Convert.ToInt32(dropOffDate[2]),
                        Convert.ToInt32(dropOffDate[1]),
                        Convert.ToInt32(dropOffDate[0])));
                    break;
                case "Pick Up Location":
                    maui.SetPickUpLocation(value);
                    break;
                case "Drop Off Location":
                    maui.SetDropOffLocation(value);
                    break;
                case "Drivers License":
                    maui.SetDriversLicense(value);
                    break;
                default:
                    throw new PendingStepException();
            }
        }

        [When(@"I click on the Search button")]
        public void WhenIClickOnTheSearchButton()
        {
            Maui maui = new Maui(Driver.WebDriver);

            maui.PerformSearch();
        }

        [Then(@"the search results count should be (.*)")]
        public void ThenTheSearchResultsCountShouldBe(int expectedCount)
        {
            Maui maui = new Maui(Driver.WebDriver);

            maui.AssertTheResultsCount(expectedCount);
        }

        #region API Testing

        [When(@"I set the request parameter ""([^""]*)"" as ""([^""]*)""")]
        public void WhenISetTheRequestParameterAs(string parameter, string value)
        {
            Maui maui = new Maui(scenarioContext);
            PricingRequest pricingRequest;

            try
            {
                pricingRequest = (PricingRequest)scenarioContext["PricingRequest"];
            }
            catch (KeyNotFoundException)
            {
                pricingRequest = new PricingRequest();
            }

            switch (parameter.ToLower())
            {
                case "session id":
                    if (value.Trim() != String.Empty)
                        pricingRequest.SessionId = value;
                    break;
                case "country":
                    CountryCodes countryCode = (CountryCodes)Enum.Parse(typeof(CountryCodes), value.RemoveWhiteSpacesAndPunctuations());  // gets the related enum
                    pricingRequest.CountryCode = countryCode.GetDescription();
                    break;
                case "pick up location":
                    PickUpLocationsNZ puLocation = 
                        (PickUpLocationsNZ)Enum.Parse(typeof(PickUpLocationsNZ), value.RemoveWhiteSpacesAndPunctuations());
                    pricingRequest.CheckoutLocationCode = puLocation.GetDescription();
                    break;
                case "pick up date":
                    string[] puDateValues = value.Trim().Split('/');
                    pricingRequest.CheckoutDateTime = $"{puDateValues[2]}-{puDateValues[1]}-{puDateValues[0]}T12:00";
                    break;
                case "drop off date":
                    string[] doDateValues = value.Trim().Split('/');
                    pricingRequest.CheckinDateTime = $"{doDateValues[2]}-{doDateValues[1]}-{doDateValues[0]}T11:00";
                    break;
                case "drop off location":
                    PickUpLocationsNZ doLocation = 
                        (PickUpLocationsNZ)Enum.Parse(typeof(PickUpLocationsNZ), value.RemoveWhiteSpacesAndPunctuations());
                    pricingRequest.CheckinLocationCode = doLocation.GetDescription();
                    break;
                case "driver's license":
                    string dlCountry = string.Concat(value.Where(c => !char.IsWhiteSpace(c)));  // removes all whitespaces
                    dlCountry = string.Concat(value.Where(c => !char.IsPunctuation(c)));    // removes punctuation marks
                    DriversLicenses dl = (DriversLicenses) Enum.Parse(typeof(DriversLicenses), dlCountry);  // gets the related enum
                    pricingRequest.CountryOfResidence = dl.GetDescription();
                    break;
                case "number of adults":
                    pricingRequest.NumberOfAdults = value;
                    break;
                default:
                    throw new Exception("An invalid request parameter has been specified!");           
            }

            scenarioContext["PricingRequest"] = pricingRequest;
        }

        [When(@"I send the pricing request")]
        public void WhenISendPricingRequest()
        {
            Maui maui = new Maui(scenarioContext);

            maui.SendPricingRequest();
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheHeaderResponseShouldBe(string statusDescription)
        {
            Maui maui = new Maui(scenarioContext);

            maui.AssertThatTheResponseStatusCodeIs(statusDescription);
        }

        [Then(@"the number of valid search results should be (.*)")]
        public void ThenTheNumberOfValidSearchResultsShouldBe(int numberOfValidResults)
        {
            Maui maui = new Maui(scenarioContext);
            maui.AssertTheNumberOfValidSearchResults(numberOfValidResults);
        }

        #endregion


    }
}
