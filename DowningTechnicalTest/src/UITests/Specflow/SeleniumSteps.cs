using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UITests.Specflow
{
    public abstract class SeleniumSteps : Steps
    {
        public SeleniumSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        protected new ScenarioContext ScenarioContext { get; }

        protected IWebDriver Driver => Retrieve<IWebDriver>();

        protected T Retrieve<T>() => ScenarioContext.Get<T>(typeof(T).Name);

        protected void Save<T>(T obj) => ScenarioContext.Add(typeof(T).Name, obj);
    }
}
