using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UITests.ApplicationUnderTest.Pages.Profile;
using UITests.Selenium;

namespace UITests.ApplicationUnderTest.Pages
{
    internal class DashboardPage : Page
    {
        public DashboardPage(IWebDriver driver, string baseUrl) : base(driver, baseUrl) {}

        #region Elements

        private IWebElement AccountDropDownElement 
            => Driver.FindElement(By.XPath(@"//a[contains(text(),""Account"")]"));

        private IEnumerable<IWebElement> AccountDropDownItems 
            => Driver.FindElement(By.ClassName("navbar-right"))
                .FindElement(By.ClassName("dropdown-menu"))
                .FindElements(By.TagName("li"));

        #endregion

        private bool Expanded(IWebElement element) => element.GetAttribute("aria-expanded") == "true";

        private IWebElement HoverOver(IWebElement element)
        {
            new Actions(Driver)
                .MoveToElement(element)
                .Build()
                .Perform();

            return element;
        }

        public ProfilePage Profile()
        {
            HoverOver(AccountDropDownElement);

            var profileItem = AccountDropDownItems.Single(e => e.Text == "Profile settings");

            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(drv => profileItem.Displayed);

            profileItem.Click();

            return new ProfilePage(Driver, BaseUrl);
        }
    }
}
