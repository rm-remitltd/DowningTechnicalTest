using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UITests.ApplicationUnderTest.Models;
using UITests.Selenium;

namespace UITests.ApplicationUnderTest.Pages.Profile
{
    internal class PersonalDetailsSection
    {
        private readonly IWebDriver _driver;
        private readonly AddressSection _addressSection;

        public PersonalDetailsSection(IWebDriver driver)
        {
            _driver = driver;

            _addressSection = new AddressSection(_driver);
        }

        #region Elements

        private SelectElement TitleDropDown
            => new SelectElement(_driver.FindElement(By.XPath(@"//label[text()=""Title*""]/following-sibling::select")));

        private IWebElement FirstNameInput
            => _driver.FindElement(By.XPath(@"//label[text()=""First name*""]/following-sibling::input"));

        private IWebElement LastNameInput
            => _driver.FindElement(By.XPath(@"//label[text()=""Last name*""]/following-sibling::input"));

        private IWebElement DateOfBirthRow
            => _driver.FindElement(By.XPath(@"//label[text()=""Date of birth*""]/following-sibling::div[1]"));

        private IList<SelectElement> DateOfBirthSelectElements
            => DateOfBirthRow.FindElements(By.TagName("select"))
                .Select(e => new SelectElement(e))
                .ToList();

        private SelectElement DaySelect => DateOfBirthSelectElements[0];

        private SelectElement MonthSelect => DateOfBirthSelectElements[1];

        private SelectElement YearSelect => DateOfBirthSelectElements[2];

        private IWebElement PhoneNumberInput => _driver.FindElement(By.Id("about-me-tel"));

        private IWebElement IntermediaryInput
            => _driver.FindElement(By.XPath(@"//label[text()=""Intermediary""]/following-sibling::input"));

        private IWebElement NationalInsuranceNumberInput
            => _driver.FindElement(By.XPath(@"//label[text()=""National insurance number""]/following-sibling::input"));

        private IWebElement SaveButton => _driver.FindElement(By.CssSelector("personal-details .primary-btn"));

        private IEnumerable<IWebElement> AlertListItems => _driver.FindElements(By.ClassName("alert__list-item__text"));

        #endregion

        #region Properties

        public string Title { set => TitleDropDown.SelectByText(value); }

        public string FirstName { set => FirstNameInput.SetText(value); }

        public string LastName { set => LastNameInput.SetText(value); }

        public DateTime DateOfBirth
        {
            set
            {
                DaySelect.SelectByText(value.Day.ToString());
                MonthSelect.SelectByText(value.ToString("MMMM"));
                YearSelect.SelectByText(value.Year.ToString());
            }
        }

        public string PhoneNumber { set => PhoneNumberInput.SetText(value); }

        public string Intermediary { set => IntermediaryInput.SetText(value); }

        public string NationalInsuranceNumber { set => NationalInsuranceNumberInput.SetText(value); }

        public IList<string> Errors { get => AlertListItems.Select(ali => ali.Text).ToList(); }

        #endregion

        #region Public Methods

        public void Enter(PersonalDetails details)
        {
            Title = details.Title;
            FirstName = details.FirstName;
            LastName = details.LastName;
            DateOfBirth = details.DateOfBirth;
            PhoneNumber = details.PhoneNumber;
            Intermediary = details.Intermediary;
            NationalInsuranceNumber = details.NationalInsuranceNumber;

            Enter(details.Address);
        }

        public void Enter(Address address) => _addressSection.Enter(address);

        public void Save() => SaveButton.Click();
 

        #endregion
    }
}
