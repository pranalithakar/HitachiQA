using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using OpenQA.Selenium;
using System;
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
            SharedObjects.PublishedAppsTitle.assertElementIsVisible(15, true);
            var fieldService = Environment.GetEnvironmentVariable("FIELDSERVICE");
            UserActions.Navigate(fieldService);
            Wait();
        }

        [Given(@"user creates a new work order")]
        public void GivenUserCreatesANewWorkOrder()
        {
            SharedObjects.GetLeftNavItem("Work Orders").Click();
            Wait();
            SharedObjects.GetButton("New").Click();
        }


        [Then(@"User enters Service Account '(.*)'")]
        public void ThenUserEntersServiceAccount(string serviceAccount)
        {
            //SharedObjects.ServiceAccount.Click();
            //SharedObjects.ServiceAccount.setText(serviceAccount);
            //SharedObjects.ServiceAccountSearchBtn.Click();
            //SharedObjects.ServiceAccountListElementXpath.WaitUntilClickable();
            //SharedObjects.ServiceAccountListElementXpath.TryClick();
        }

        [Then(@"User Enters Functional Location as '(.*)'")]
        public void ThenUserEntersFunctionalLocationAs(string functionalLocation)
        {
            //SharedObjects.FunctionalLocation.Click();
            //SharedObjects.FunctionalLocation.setText(functionalLocation);
            //SharedObjects.FunctionalLocationItemsList(functionalLocation).TryClick();
        }

        [Then(@"User enters Work order type as '(.*)'")]
        public void ThenUserEntersWorkOrderTypeAs(string workOrderType)
        {
            //SharedObjects.WorkOrderType.Click();
            //SharedObjects.WorkOrderType.setText(workOrderType);
            //var options = WorkOrdersPage.WorkOrderType.ListofElements(WorkOrdersPage.WorkOrdeTypeListElementXpath.ToString());
            //options.ForEach(x => x.Click());
        }

        [Then(@"User click on Save and close button")]
        public void ThenUserClickOnSaveAndCloseButton()
        {
            //SharedObjects.SaveButton.Click();
        }

        [Then(@"Work order is created and saved")]
        public void ThenWorkOrderIsCreatedAndSaved()
        {
            //Assert.IsTrue(SharedObjects.WorkOrderHeader.GetInnerText().Contains(" - Saved"));
            //var OrderText = SharedObjects.WorkOrderHeader.GetInnerText().Replace(" - Saved", "");
            //Convert.ToInt32(OrderText);
        }

    }
}
