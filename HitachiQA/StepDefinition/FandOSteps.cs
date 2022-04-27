using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class FandOSteps
    {
        private string VendorName;
        private string VendorFirst;
        private string VendorMiddle;
        private string VendorLast;

        [Given(@"user navigates to dashobard")]
        public void GivenUserNavigatesToDashobard()
        {
            UserActions.Navigate(Environment.GetEnvironmentVariable("HOST"));
            string username = Environment.GetEnvironmentVariable("USER_NAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");
            string decUser = Functions.DecryptString(username);
            string decPass = Functions.DecryptString(password);
            SharedObjects.GetTextField("email").setText(decUser);
            SharedObjects.GetButton("Next").Click();
            SharedObjects.GetTextField("password").setText(decPass);
            SharedObjects.GetButton("Sign in").Click();
            SharedObjects.GetButton("No").Click();
        }

        //
        // Vendor creation
        //

        [Given(@"user continues to enter new Vendor section")]
        public void GivenUserContinuesToEnterNewVendorSection()
        {
            SharedObjects.ModulesButton.Click();
            SharedObjects.GetModulesListItem("Procurement and sourcing").Click();
            var expanded = SharedObjects.GetButton("Vendors").GetAttribute("aria-expanded");
            if(expanded != "true")
            {
                SharedObjects.GetButton("Vendors").Click();
                Thread.Sleep(200);
            }
            SharedObjects.GetButton("All vendors").Click();
            Thread.Sleep(200);
            ScreenShot.Info();
        }

        [When(@"user continues to enter General Info")]
        public void WhenUserContinuesToEnterGeneralInfo()
        {
            Thread.Sleep(1100);
            SharedObjects.NewButton.Click();
            Thread.Sleep(1100);
            string uniqueID = Functions.GetRandomInteger().ToString();
            VendorName = "autoVendor" + uniqueID;
            VendorFirst = "TheFirst";
            VendorMiddle = "TheSecond";
            VendorLast = "TheLast";

            SharedObjects.GetDropdown("Type").SelectDropdownOptionByText("Person");
            SharedObjects.GetDropdown("Group").setText("ONE");
            SharedObjects.GetTextField("Vendor account").setText(VendorName +  Keys.Tab);
            SharedObjects.GetTextField("Name_FirstName").setText(VendorFirst + Keys.Tab);
            SharedObjects.GetTextField("Name_MiddleName").setText(VendorMiddle + Keys.Tab);
            SharedObjects.GetTextField("Name_LastName").setText(VendorLast);
            ScreenShot.Info();
        }


        [Then(@"user successfully saves new Vendor")]
        public void ThenUserSuccessfullySavesNewVendor()
        {
            string newVendorTitle = $"{VendorName} : {VendorFirst} {VendorMiddle} {VendorLast}";
            bool TitleExists = true;
            SharedObjects.FormSaveButton.Click();
            do
            {
              SharedObjects.SavedVendor.WaitUntilElementContainsText(newVendorTitle);

            } while (!TitleExists);
            SharedObjects.SavedVendor.assertElementInnerTextEquals(newVendorTitle);
            ScreenShot.Info();
        }

        //
        // Sales Order
        //

        [Given(@"user launches '([^']*)' sales order batch script")]
        public void GivenUserLaunchesSalesOrderBatchScript(string batchFileName)
        {
            if(batchFileName.ToLower() == "confirm" | batchFileName.ToLower() == "create")
            {
                LaunchSalesOrderBatch(batchFileName);
            }
            else { throw new Exception($"Batch file {batchFileName} is not recognized"); }
        }

        public void LaunchSalesOrderBatch(string filename)
        {
            Process.Start("c:\\batchfilename.bat");
        }

    }
}
