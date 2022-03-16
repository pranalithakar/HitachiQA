using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using System;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class LoginSteps
    {
        [Given(@"user is landed on Apps page")]
        public void GivenUserIsLandedOnAppsPage()
        {
            UserActions.Navigate(Environment.GetEnvironmentVariable("HOST"));
            SharedObjects.GetInputField("email").setText(Environment.GetEnvironmentVariable("USERNAME"));
            SharedObjects.GetButton("Next").Click();
            string password = Functions.DecryptString(Environment.GetEnvironmentVariable("PASSWORD"));
            SharedObjects.GetInputField("").setText(password);
        }

    }
}
