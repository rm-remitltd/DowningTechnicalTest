using OpenQA.Selenium;

namespace UITests.Selenium
{
    internal abstract class Page
    {
        protected IWebDriver Driver { get; }
        protected string BaseUrl { get; }

        public Page(IWebDriver driver, string baseUrl)
        {
            Driver = driver;

            BaseUrl = baseUrl;
        }
    }
}
