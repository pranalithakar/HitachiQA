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

        [Given(@"The user is on Work order page")]
        public void GivenTheUserIsOnWorkOrderPage()
        {
            HomePage.WorkOrderNavigationMenu.Click();

            // TODO : If action has been complete in line 59, is it necessary that we're still waiting in 63?
            // If not, we can remove it. If it is needed, no problem. This is the correct way if it's needed.
            WorkOrdersPage.ActiveWorkOrdersMenuButton.WaitUntilClickable();
        }

        [Given(@"user click on New ribbon button")]
        public void GivenUserClickOnNewRibbonButton()
        {
            WorkOrdersPage.NewButton.Click();
        }

        [Then(@"New work order form is opened")]
        public void ThenNewWorkOrderFormIsOpened()
        {
            WorkOrdersPage.NewWorkOrderPageIdentifier.assertElementIsVisible(90, true);
        }

        [Then(@"User enters Service Account '(.*)'")]
        public void ThenUserEntersServiceAccount(string serviceAccount)
        {
            WorkOrdersPage.ServiceAccount.Click();
            WorkOrdersPage.ServiceAccount.setText(serviceAccount);
            WorkOrdersPage.ServiceAccountSearchBtn.Click();
            WorkOrdersPage.ServiceAccountListElementXpath.WaitUntilClickable();
            WorkOrdersPage.ServiceAccountListElementXpath.TryClick();
        }

        [Then(@"User Enters Functional Location as '(.*)'")]
        public void ThenUserEntersFunctionalLocationAs(string functionalLocation)
        {
            WorkOrdersPage.FunctionalLocation.Click();
            WorkOrdersPage.FunctionalLocation.setText(functionalLocation);
            WorkOrdersPage.FunctionalLocationItemsList(functionalLocation).TryClick();
        }

        [Then(@"User enters Work order type as '(.*)'")]
        public void ThenUserEntersWorkOrderTypeAs(string workOrderType)
        {
            WorkOrdersPage.WorkOrderType.Click();
            WorkOrdersPage.WorkOrderType.setText(workOrderType);
            var options = WorkOrdersPage.WorkOrderType.ListofElements(WorkOrdersPage.WorkOrdeTypeListElementXpath.ToString());
            options.ForEach(x => x.Click());

            //TODO - I really like this approach using options.ForEach. 
            // I'd like to watch this in the browser if you don't mind showing me when we meet again.
        }

        [Then(@"User click on Save and close button")]
        public void ThenUserClickOnSaveAndCloseButton()
        {
            WorkOrdersPage.SaveButton.Click();
        }

        [Then(@"Work order is created and saved")]
        public void ThenWorkOrderIsCreatedAndSaved()
        {
            Assert.IsTrue(WorkOrdersPage.WorkOrderHeader.GetInnerText().Contains(" - Saved"));
            var OrderText = WorkOrdersPage.WorkOrderHeader.GetInnerText().Replace(" - Saved", "");
            /*int orderNumber =*/
            Convert.ToInt32(OrderText);
            //TODO
            // Int orderNumber (line 126) is declared but never used. We can just call Convert.ToInt32 directly without the instantiation.
        }

    }
}
