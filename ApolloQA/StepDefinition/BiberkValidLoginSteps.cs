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
using ApolloQA.Source.Driver;
using ApolloQA.Source.Helpers;

namespace ApolloQA.Steps
{
    [Binding]
    public sealed class BiberkValidLoginSteps
    {
        public IWebDriver driver;


        BiberkValidLoginSteps(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        [Given(@"user landed biBerk page with valid URL")]
        public void GivenUserLandedBiBerkPageWithValidURL()
        {
            Console.WriteLine("Current Page" + Environment.GetEnvironmentVariable("HOST"));

            Pages.Login.navigate();
        }

        [When(@"user enters username: (.*) and password: (.*)")]
        public void WhenUserEntersAnd(string username, string password)
        {
            Log.Info("User enters username");
            Log.Critical("critical username");

            Log.Critical("Secret" + KeyVault.GetSecret());

            Pages.Login.usernameField.setText(username);
            Pages.Login.nextButton.Click();
            Pages.Login.passwordField.setText(password);
            var sev1 = new Severity(2);
            var sev2 = new Severity(2);
            Console.WriteLine(Assert.AreEqual(sev1, sev2,true));
            Boolean? somebool = null;
            Console.WriteLine(Assert.IsNull(somebool));


               
        }

        [When(@"user attempts to login")]
        public void WhenUserAttemptsToLogin()
        {
            Pages.Login.nextButton.Click();
            Pages.Login.noButton.Click();
            
        }
       
        [Then(@"user login successfully to biBerk page")]
        public void ThenUserLoginSuccessfullyToBiBerkPage()
        {
            Pages.Home.ApolloIcon.assertElementIsVisible();
            Log.Warn("this is a warning message");
            ScreenShot.Info();
            ScreenShot.Debug();

        }
    }
}
