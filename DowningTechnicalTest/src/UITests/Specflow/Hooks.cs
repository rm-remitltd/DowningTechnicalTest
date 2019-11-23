using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace UITests.Specflow
{
    [Binding]
    public class Hooks : SeleniumSteps
    {
        public Hooks(ScenarioContext scenarioContext) : base(scenarioContext) {}

        [BeforeScenario]
        public void BeforeScenario()
        {
            var driver = new ChromeDriver(new ChromeOptions());

            driver.Manage().Window.Maximize();

            Save<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = Retrieve<IWebDriver>();

            try
            {
                driver.Close();

                driver.Quit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
