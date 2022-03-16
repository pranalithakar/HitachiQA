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
            string password = Cryptography.Encrypt(Environment.GetEnvironmentVariable("PASSWORD"));
            //string password = Cryptography.Decrypt(Environment.GetEnvironmentVariable("PASSWORD"));
        }

    }
}
