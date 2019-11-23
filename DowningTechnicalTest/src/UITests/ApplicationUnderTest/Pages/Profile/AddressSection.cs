using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UITests.ApplicationUnderTest.Models;
using UITests.Selenium;

namespace UITests.ApplicationUnderTest.Pages.Profile
{
    internal class AddressSection
    {
        private readonly IWebDriver _driver;

        public AddressSection(IWebDriver driver)
        {
            _driver = driver;
        }

        #region Elements

        private IWebElement PostcodeInput 
            => _driver.FindElement(By.Id("about-me-pcode"));

        private IWebElement LookupButton
            => _driver.FindElement(By.Id("address-lookup"));

        private IEnumerable<IWebElement> MoveInDateRow =>
            _driver.FindElements(By.CssSelector(".address-lookup-row select"));

        private SelectElement MoveInDateMonthSelect 
            => new SelectElement(MoveInDateRow.First());

        private SelectElement MoveInDateYearSelect
            => new SelectElement(MoveInDateRow.Last());


        #endregion

        #region Properties

        public string Postcode { set => PostcodeInput.SetText(value); }

        public DateTime MoveInDate
        {
            set
            {
                MoveInDateMonthSelect.SelectByText(value.ToString("MMMM"));
                MoveInDateYearSelect.SelectByText(value.Year.ToString());
            }
        }

        #endregion

        #region Public Methods

        public void Enter(Address address)
        {
            Postcode = address.Postcode;
            MoveInDate = address.MoveInDate;

            LookupButton.Click();
        }

        #endregion  
    }
}
