using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SpecFlowTestProject.POCO;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace SpecFlowTestProject.Pages
{
    public partial class Maui
    {
        private IWebDriver webDriver;

        private readonly ScenarioContext scenarioContext;
        
        public Maui()
        {

        }

        public Maui(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        public Maui(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void SelectTheDestinationOnMainPage(string destination)
        {
            DestinationDropDown.Click();

            switch (destination)
            {
                case "Australia":
                    DestinationOptionAustralia.Click();
                    break;
                case "New Zealand":
                    DestinationOptionNewZealand.Click();
                    break;
                default:
                    throw new ArgumentException("The specified destination is invalid.");
            }
        }

        public void SetPickUpDate(DateTime date)
        {
            PickUpDatePicker.Click();
            SelectDateOnCalendar(date);
        }

        public void SetDropOffDate(DateTime date)
        {
            DropOffDatePicker.Click();
            SelectDateOnCalendar(date);
        }

        public void SetPickUpLocation(string location)
        {
            PickUpLocationDropdown.Click();
            List<IWebElement> locationOptions = PickUpLocationDropdown.FindElements(By.TagName("li")).ToList();
            bool matchFound = false;

            foreach (IWebElement option in locationOptions)
            {
                if (option.Text.ToLower() == location.ToLower().Trim())
                {
                    option.Click();
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
                throw new Exception("Pick Up location not found!");
        }
        
        public void SetDropOffLocation(string location)
        {
            DropOffLocationField.Click();
            List<IWebElement> listOptions = DropOffLocationField.FindElements(By.TagName("li")).ToList();
            bool matchFound = false;

            foreach (IWebElement option in listOptions)
            {
                if (option.Text.ToLower() == location.ToLower().Trim())
                {
                    option.Click();
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
                throw new Exception("Drop Off location not found!");
        }

        public void SetDriversLicense(string country)
        {
            DriversLicenseDropdown.Click();
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(d => DriversLicenseSearchBox);
            DriversLicenseSearchBox.SendKeys(country);

            List<IWebElement> listOptions = DriversLicenseDropdown.FindElements(By.TagName("li")).ToList();

            foreach (IWebElement option in listOptions)
            {
                if (option.Text.ToLower() == country.ToLower().Trim())
                {
                    option.Click();
                    break;
                }
            }
        }

        public void PerformSearch()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => SearchButton);
            SearchButton.Click();
        }

        #region API Testing

        public void SetRequestParameterValue(string parameter, string value)
        {
            RestRequest request = (RestRequest) scenarioContext["RestRequest"];
            request.AddParameter(parameter, value);
        }

        public void SendPricingRequest()
        {
            RestClient client = (RestClient) scenarioContext["RestClient"];
            RestRequest request = (RestRequest) scenarioContext["RestRequest"];
            PricingRequest priceRequest = (PricingRequest) scenarioContext["PricingRequest"];

            request.AddParameter("countryCode", priceRequest.CountryCode);
            request.AddParameter("checkoutLocationCode", priceRequest.CheckoutLocationCode);
            request.AddParameter("checkoutDateTime", priceRequest.CheckoutDateTime);
            request.AddParameter("checkinLocationCode", priceRequest.CheckinLocationCode);
            request.AddParameter("checkinDateTime", priceRequest.CheckinDateTime);
            request.AddParameter("countryOfResidence", priceRequest.CountryOfResidence);
            request.AddParameter("numberOfAdults", priceRequest.NumberOfAdults);
            request.AddParameter("numberOfChildren", priceRequest.NumberOfChildren);

            scenarioContext["PricingResponse"] = client.Execute(request);
        }

        #endregion

        private void SelectDateOnCalendar(DateTime date)
        {
            // Select the year
            string displayedYear = DatePickerControl.FindElement(By.XPath("//span[@class='ui-datepicker-year']")).Text.Trim();
            int diffInYears = date.Year - Convert.ToInt32(displayedYear);

            if (diffInYears > 0)
            {
                while (DatePickerControl.FindElement(By.XPath("//span[@class='ui-datepicker-year']")).Text.Trim() != date.Year.ToString())
                {
                    CalendarNextButton.Click();
                }
            }
            else if (diffInYears < 0)
            {
                throw new Exception("The required year is in the past and a booking cannot be made.");
            }

            // Select the month
            string displayedMonth = DatePickerControl.FindElement(By.XPath("//span[@class='ui-datepicker-month']")).Text.Trim();
            int displayedMonthNo =
                    DateTime.ParseExact(displayedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            int diffInMonths = date.Month - displayedMonthNo;

            if (diffInMonths > 0)
            {
                for (int i = 0; i < diffInMonths; i++)
                {
                    CalendarNextButton.Click();
                }
            }
            else if (diffInMonths < 0)
            {
                for (int i = 0; i < (diffInMonths * -1); i++)
                {
                    CalendarPreviousButton.Click();
                }
            }

            // Select the day
            IWebElement dayNumber = CalendarDays.FindElement(By.XPath($"//*[text()='{date.Day}']"));

            if (!dayNumber.GetAttribute("class").Contains("ui-datepicker-unselectable ui-state-disabled"))
            {
                dayNumber.Click();
            }
            else
            {
                throw new Exception("Date is not selectable.");
            }
        }
    }
}
