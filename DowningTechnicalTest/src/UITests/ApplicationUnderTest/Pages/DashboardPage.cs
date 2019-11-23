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

        private By WelcomeMessageModal => By.Id("welcome-msg");

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
            DismissWelcomeMessage();

            HoverOver(AccountDropDownElement);

            var profileItem = AccountDropDownItems.Single(e => e.Text == "Profile settings");

            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(drv => profileItem.Displayed);

            profileItem.Click();

            return new ProfilePage(Driver, BaseUrl);
        }

        private void DismissWelcomeMessage()
        {
            var welcomeMessageModal = Driver.FindElements(WelcomeMessageModal).FirstOrDefault();

            if (welcomeMessageModal == null) return;

            if (!welcomeMessageModal.Displayed) return;

            var closeButton = welcomeMessageModal.FindElement(By.CssSelector(@"button[aria-label=""Close""]"));

            closeButton.Click();

            WaitUntilHidden(WelcomeMessageModal);
        }

        private bool WaitUntilHidden(By element)
            => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
                .Until(drv => !drv.FindElement(element).Displayed);
    }
}
