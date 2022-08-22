using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.Pages
{
    public partial class Maui
    {
        IWebElement DestinationDropDown => webDriver.FindElement(By.XPath("/html/body/div[4]/div/div/div/div/div/div"));

        IWebElement DestinationOptionNewZealand => DestinationDropDown.FindElement(By.XPath("//option[2]"));

        IWebElement DestinationOptionAustralia => DestinationDropDown.FindElement(By.XPath("//option[3]"));

        IWebElement PickUpDatePicker => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[2]"));

        IWebElement DropOffDatePicker => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[3]"));

        IWebElement PickUpLocationDropdown => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[5]"));
        //IWebElement PickUpLocationDropdown => webDriver.FindElement(By.Id("form_location_pickUp__main"));

        IWebElement DropOffLocationField => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[6]"));

        IWebElement PassengersCountControl => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[7]"));

        IWebElement DriversLicenseDropdown => webDriver.FindElement(By.XPath("//*[@id='form_main']/div[10]"));

        IWebElement DriversLicenseSearchBox => DriversLicenseDropdown.FindElement(By.XPath(".//div[@class='chosen-search']/input"));

        IWebElement SearchButton => webDriver.FindElement(By.XPath("//button[@class='[ btnPrimary ][ submit ]']"));

        #region Calendar Control
        IWebElement DatePickerControl => webDriver.FindElement(By.Id("datepicker__main"));

        IWebElement CalendarNextButton => DatePickerControl.FindElement(By.XPath("//a[@title='Next']"));

        IWebElement CalendarPreviousButton => DatePickerControl.FindElement(By.XPath("//a[@title='Prev']"));

        IWebElement DatePickerCalendar => DatePickerControl.FindElement(By.XPath("//table[@class='ui-datepicker-calendar']"));

        IWebElement CalendarDays => DatePickerCalendar.FindElement(By.XPath("//tbody"));
        #endregion

        #region Search Results Page
        IWebElement SearchResultsSpinner => webDriver.FindElement(By.XPath("//*[@class='fa fa-fw fa-refresh fa-spin text-info']"));
        
        IWebElement SearchResultsCount => webDriver.FindElement(By.XPath("//*[@id='rootPrimary']/div[1]/div/div/div/div[2]/div[2]/div[1]/div[2]/div/div[1]"));
        #endregion
    }
}
