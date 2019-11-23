using OpenQA.Selenium;
using UITests.Selenium;

namespace UITests.ApplicationUnderTest.Pages.Profile
{
    internal class ProfilePage : Page
    {
        public ProfilePage(IWebDriver driver, string baseUrl) 
            : base(driver, baseUrl) {}

        #region Elements

        private IWebElement InvestingForSelfRadio 
            => Driver.FindElement(By.CssSelector(@"label[for=""myself_inv0""]"));

        #endregion

        internal PersonalDetailsSection InvestingForSelf()
        {
            InvestingForSelfRadio.Click();

            return new PersonalDetailsSection(Driver);
        }
    }
}
