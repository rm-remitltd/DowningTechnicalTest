using OpenQA.Selenium;

namespace UITests.Selenium
{
    internal static class WebDriverExtensions
    {
        internal static void SetText(this IWebElement element, string text)
            => element.SendKeys(text ?? string.Empty);
    }
}
