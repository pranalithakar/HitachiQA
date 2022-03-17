using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class LoginSteps
    {
        public IWebDriver driver;
        public string searchCriteria;

        LoginSteps(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        private readonly ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"user is landed on Apps page")]
        public void GivenUserIsLandedOnAppsPage()
        {
            UserActions.Navigate("/");
            var username = Environment.GetEnvironmentVariable("USER_NAME");
            SharedObjects.GetInputField("email").setText(username);
            SharedObjects.GetButton("Next").Click();
            string password = Functions.DecryptString(Environment.GetEnvironmentVariable("PASSWORD"));
            SharedObjects.GetInputField("password").setText(password);
            SharedObjects.GetButton("Sign in").Click();
            if(SharedObjects.GetButton("Yes").assertElementIsVisible(1, true) == true)
            {
                SharedObjects.GetButton("No").Click();
            }
        }

    }
}
