using HitachiQA.Driver;
using HitachiQA.Helpers;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class LoginSteps
    {
        [Given(@"user is landed on Apps page")]
        public void GivenUserIsLandedOnAppsPage()
        {
            UserActions.Navigate()
        }

    }
}
