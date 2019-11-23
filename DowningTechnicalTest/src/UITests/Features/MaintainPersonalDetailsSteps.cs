using NUnit.Framework;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using UITests.ApplicationUnderTest.Models;
using UITests.ApplicationUnderTest.Pages;
using UITests.ApplicationUnderTest.Pages.Profile;
using UITests.ApplicationUnderTest.TestData;
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

            Assert.That(dashBoard.Displayed, Is.True, "Dashboard page not displayed.");
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

        [When(@"they supply a date of birth that makes them (.*) years and (.*) months old")]
        public void WhenTheySupplyADateOfBirthThatMakesThemYearsOld(int years, int months)
        {
            var mrTest = Investors.MrTest;
            mrTest.DateOfBirth = CalculateDoB(years, months);

            var personalDetails = Retrieve<PersonalDetailsSection>();

            personalDetails.Enter(mrTest);

            personalDetails.Save();
        }

        private DateTime CalculateDoB(int years, int months) 
            => DateTime.Today.AddMonths(-months).AddYears(-years);

        [Then(@"investors profile will be updated with no errors")]
        public void ThenInvestorsProfileWillBeUpdatedWithNoErrors()
        {
            var personalDetails = Retrieve<PersonalDetailsSection>();

            Assert.That(personalDetails.Errors, Is.Empty, "Unexpected errors were displayed.");
        }

        [Then(@"the investor will be prompted about the following issues")]
        public void ThenTheInvestorWillBePromptedAboutTheFollowingIssues(IEnumerable<string> expectedErrors)
        {
            var personalDetails = Retrieve<PersonalDetailsSection>();

            Assert.That(personalDetails.Errors, Is.EquivalentTo(expectedErrors));
        }
    }
}
