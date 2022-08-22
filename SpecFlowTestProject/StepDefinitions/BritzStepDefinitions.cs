using SpecFlowTestProject.Pages;
using SpecFlowTestProject.POCO;
using SpecFlowTestProject.Support;
using SpecFlowTestProject.Support.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.StepDefinitions
{
    [Binding]
    public sealed class BritzStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public BritzStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I select the destination as ""([^""]*)"" on Britz")]
        public void WhenISelectTheDestinationAs(string destination)
        {
            Britz britz = new Britz(Driver.WebDriver);

            britz.SelectTheDestinationOnMainPage(destination);
        }

        [When(@"I set the (Pick Up Date|Drop Off Date|Pick Up Location|Drop Off Location|Drivers License|Promo Code) as ""([^""]*)"" on Britz")]
        public void WhenISetThePickUpDateAsOnBritz(string field, string value)
        {
            Britz britz = new Britz(Driver.WebDriver);

            switch (field)
            {
                case "Pick Up Date":
                    string[] pickUpDate = value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    britz.SetPickUpDate(
                        new DateTime(Convert.ToInt32(pickUpDate[2]),
                        Convert.ToInt32(pickUpDate[1]),
                        Convert.ToInt32(pickUpDate[0])));
                    break;
                case "Drop Off Date":
                    string[] dropOffDate = value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    britz.SetPickUpDate(
                        new DateTime(Convert.ToInt32(dropOffDate[2]),
                        Convert.ToInt32(dropOffDate[1]),
                        Convert.ToInt32(dropOffDate[0])));
                    break;
                case "Pick Up Location":
                    britz.SetPickUpLocation(value);
                    break;
                case "Drop Off Location":
                    britz.SetDropOffLocation(value);
                    break;
                case "Drivers License":
                    britz.SetDriversLicense(value);
                    break;
                default:
                    throw new PendingStepException();
            }
        }

        [When(@"I click on the Search button on Britz")]
        public void WhenIClickOnTheSearchButtonOnBritz()
        {
            Britz britz = new Britz(Driver.WebDriver);

            britz.PerformSearch();
        }

        #region API Tests

        [When(@"I set the Britz request parameter ""([^""]*)"" as ""([^""]*)""")]
        public void WhenISetTheBritzRequestParameterAs(string parameter, string value)
        {
            Britz britz = new Britz(scenarioContext);
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
                    LocationsAU puLocation = (LocationsAU)Enum.Parse(typeof(LocationsAU), value.RemoveWhiteSpacesAndPunctuations());
                    pricingRequest.CheckoutLocationCode = puLocation.GetDescription();
                    break;
                case "pick up date":
                    string[] puDateValues = value.Trim().Split('/');
                    pricingRequest.CheckoutDateTime = $"{puDateValues[2]}-{puDateValues[1]}-{puDateValues[0]}T11:00";
                    break;
                case "drop off date":
                    string[] doDateValues = value.Trim().Split('/');
                    pricingRequest.CheckinDateTime = $"{doDateValues[2]}-{doDateValues[1]}-{doDateValues[0]}T12:00";
                    break;
                case "drop off location":
                    LocationsAU doLocation = (LocationsAU)Enum.Parse(typeof(LocationsAU), value.RemoveWhiteSpacesAndPunctuations());
                    pricingRequest.CheckinLocationCode = doLocation.GetDescription();
                    break;
                case "driver's license":
                    DriversLicenses dl = (DriversLicenses)Enum.Parse(typeof(DriversLicenses), value.RemoveWhiteSpacesAndPunctuations());  // gets the related enum
                    pricingRequest.CountryOfResidence = dl.GetDescription();
                    break;
                case "number of adults":
                    pricingRequest.NumberOfAdults = value;
                    break;
                default:
                    throw new Exception($"The request parameter {parameter} is invalid!");
            }

            scenarioContext["PricingRequest"] = pricingRequest;
        }

        [When(@"I send the Britz pricing request")]
        public void WhenISendTheBritzPricingRequest()
        {
            Britz britz = new Britz(scenarioContext);

            britz.SendPricingRequest();
        }

        [Then(@"the Britz response status code should be (.*)")]
        public void ThenTheBritzHeaderResponseShouldBe(string statusDescription)
        {
            Britz britz = new Britz(scenarioContext);

            britz.AssertThatTheResponseStatusCodeIs(statusDescription);
        }

        [Then(@"the number of valid Britz search results should be (.*)")]
        public void ThenTheNumberOfValidSearchResultsShouldBe(int numberOfValidResults)
        {
            Britz britz = new Britz(scenarioContext);

            britz.AssertTheNumberOfValidSearchResults(numberOfValidResults);
        }

        #endregion
    }
}
