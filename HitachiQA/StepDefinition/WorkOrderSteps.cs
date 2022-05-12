using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class WorkOrderSteps
    {
        public string searchCriteria;
        private static void Wait() => UserActions.waitForPageLoad();


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
            Wait();
        }

        [Given(@"The user nagivates to work order page")]
        public void GivenTheUserNagivatesToWorkOrderPage()
        {
            SharedObjects.PublishedAppsTitle.assertElementIsVisible(5, true);
            var fieldService = Environment.GetEnvironmentVariable("FIELDSERVICE");
            UserActions.Navigate(fieldService);
            Wait();
        }

        [Given(@"user creates a new work order")]
        public void GivenUserCreatesANewWorkOrder()
        {
            SharedObjects.GetLeftNavItem("Work Orders").Click();
            Wait();
            SharedObjects.GetButton("New").WaitUntilClickable();
            SharedObjects.GetButton("New").Click();
        }

        [Given(@"user enters required work order Info '([^']*)'")]
        public void GivenUserEntersRequiredWorkOrderInfo(string fabName)
        {
            WorkOrderPage.FabInput.setText(fabName);
            WorkOrderPage.SelectOptionItem(fabName).WaitUntilClickable();
            WorkOrderPage.SelectOptionItem(fabName).Click();

            string woType = "Test";
            WorkOrderPage.WOType.setText(woType);
            WorkOrderPage.SelectOptionItem("Test WO Type").WaitUntilClickable();
            WorkOrderPage.SelectOptionItem("Test WO Type").Click();
        }

        [Then(@"Work order is created and saved")]
        public void ThenWorkOrderIsCreatedAndSaved()
        {
            SharedObjects.AdminSaveButton.Click();
            WorkOrderPage.WOSaving.assertElementNotPresent(15, true);
            try { WorkOrderPage.WOStatus.assertElementContainsText("Saved"); }
            catch {
                Thread.Sleep(300);
                WorkOrderPage.WOStatus.assertElementContainsText("Saved");
            }
        }
    }
}
