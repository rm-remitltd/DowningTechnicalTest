using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UITests.Selenium;

namespace UITests.ApplicationUnderTest.Pages
{
    internal class LoginPage : Page
    {
        private const string Url = "account/login";

        public LoginPage(IWebDriver driver, string baseUrl) : base(driver, baseUrl)
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/" + Url);
        }

        #region Elements

        private IWebElement EmailAddressElement 
            => InputTags.Single(e => e.GetAttribute("type") == "email");

        private IWebElement PasswordElement => InputTags.Single(e => e.GetAttribute("type") == "password");

        private IEnumerable<IWebElement> InputTags => Driver.FindElements(By.TagName("input"));

        private IWebElement LoginButtonElement => Driver.FindElement(By.ClassName("btn-primary"));
        
        #endregion

        internal DashboardPage Submit(string email, string password)
        {
            EmailAddressElement.SendKeys(email);

            PasswordElement.SendKeys(password);

            LoginButtonElement.Click();

            return new DashboardPage(Driver, BaseUrl);
        }
    }
}
