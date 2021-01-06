using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using HitachiQA.Driver;
using HitachiQA.Helpers;

namespace HitachiQA.Steps
{
    [Binding]
    public sealed class GoogleSearchSteps
    {
        public IWebDriver driver;
        public string searchCriteria;

        GoogleSearchSteps(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        [Given(@"user landed in Google.com")]
        public void GivenUserLandedBiBerkPageWithValidURL()
        {
            Pages.Google.navigate();
        }

        [When(@"user searches for (.*)")]
        public void WhenUserEntersAnd(string searchCriteria)
        {

            Pages.Google.SearchInput.setText(searchCriteria);
            this.searchCriteria = searchCriteria;



        }

        [When(@"user attempts to search")]
        public void WhenUserAttemptsToLogin()
        {
            Pages.Google.SearchButton.Click();
        }
       
        [Then(@"user presented with search results")]
        public void ThenUserLoginSuccessfullyToBiBerkPage()
        {
            Pages.Google.SearchInput.assertTextFieldTextEquals(this.searchCriteria);

            Log.Info("This is an infomrmaitonal Message");
            ScreenShot.Info();

        }
    }
}
