using NUnit.Framework;
using System.Linq;
using TechTalk.SpecFlow;
using UITests.ApplicationUnderTest.Models;
using UITests.ApplicationUnderTest.Pages;
using UITests.ApplicationUnderTest.Pages.Profile;
using UITests.Specflow;

namespace UITests.Features
{
    [Scope(Feature = "Maintain Personal Details")]
    [Binding]
    public sealed class MaintainPersonalDetailsSteps : SeleniumSteps
    {
        public MaintainPersonalDetailsSteps(ScenarioContext scenarioContext)
            : base(scenarioContext) { }

        [Given(@"the investor has logged in")]
        public void GivenTheInvestorHasLoggedIn()
        {
            var login = new LoginPage(Driver, Configuration.DowningWebsiteUrl);

            var dashBoard = login.Submit(Configuration.EmailAddress, Configuration.Password);

            Save(dashBoard);
        }

        [Given(@"the Dashboard is displayed")]
        public void GivenTheDashboardIsDisplayed()
        {
            var dashBoard = Retrieve<DashboardPage>();
        }

        [Given(@"the investor has elected to maintain their profile")]
        public void GivenTheInvestorHasElectedToMaintainTheirProfile()
        {
            var dashBoard = Retrieve<DashboardPage>();

            var personalDetails = dashBoard.Profile().InvestingForSelf();

            Save(personalDetails);
        }

        [When(@"the following mandatory personal details are provided")]
        public void WhenTheFollowingMandatoryPersonalDetailsAreProvided(PersonalDetails details)
        {
            var personalDetails = Retrieve<PersonalDetailsSection>();

            personalDetails.Enter(details);

            personalDetails.Save();
        }

        [When(@"their personal details are updated without providing any mandatory data")]
        public void WhenTheirPersonalDetailsAreUpdatedWithoutProvidingAnyMandatoryData()
        {
            var personalDetails = Retrieve<PersonalDetailsSection>();

            personalDetails.Save();
        }


        [Then(@"investors profile will be updated with no errors")]
        public void ThenInvestorsProfileWillBeUpdatedWithNoErrors()
        {
            var personalDetails = Retrieve<PersonalDetailsSection>();

            Assert.That(personalDetails.Errors, Is.Empty, "Unexpected errors were displayed.");
        }

        [Then(@"the investor will be prompted about the following issues")]
        public void ThenTheInvestorWillBePromptedAboutTheFollowingIssues(Table errorsTable)
        {
            var expectedErrors = errorsTable.Rows.Select(tr => tr["Expected Error"]).ToList();

            var personalDetails = Retrieve<PersonalDetailsSection>();

            Assert.That(personalDetails.Errors, Is.EquivalentTo(expectedErrors));
        }
    }
}
