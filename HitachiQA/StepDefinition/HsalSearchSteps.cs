using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public sealed class HsalSearchSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public IWebDriver driver;
        public string searchCriteria;

        HsalSearchSteps(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        private readonly ScenarioContext _scenarioContext;

        public HsalSearchSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"user landed on HSAL homepage")]
        public void GivenUserLandedOnHSALHomepage()
        {

         Pages.HsalHome.navigate();
        }


        [When(@"user searches HSAL homepage for (.*)")]
        public void WhenUserSearchesHSALHomepage(string searchCriteria)
        {
            Pages.HsalHome.OpenSearch.Click();
            Pages.HsalHome.SearchInput.setText(searchCriteria);
            this.searchCriteria = searchCriteria;
        }


        [When(@"user attempts to search HSAL homepage")]
        public void WhenUserAttemptsToSearchHSALHomepage()
        {
            Pages.HsalHome.SearchButton.Click();
        }


        [Then(@"user presented with search results from HSAL")]
        public void ThenUserPresentedWithSearchResultsFromHSAL()
        {
            Pages.HsalHome.SearchInput.assertTextFieldTextEquals(this.searchCriteria);

            Log.Info("This is an infomrmaitonal Message");
            ScreenShot.Info();
        }

    }
}
