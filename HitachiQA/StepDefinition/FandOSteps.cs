using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using HitachiQA.Source.Helpers;
using OpenQA.Selenium;
using System;
using AutoItX3Lib;
using TechTalk.SpecFlow;
using System.Threading;
using System.IO;

namespace HitachiQA.StepDefinition
{
    [Binding]
    public class FandOSteps
    {
        private static void Wait() => UserActions.waitForPageLoad();
        private string VendorName;
        private string VendorFirst;
        private string VendorMiddle;
        private string VendorLast;


        [Given(@"user navigates to dashboard")]
        public void GivenUserNavigatesToDashboard()
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
                Wait();
            }
            SharedObjects.GetButton("All vendors").Click();
            Wait();
        }

        [When(@"user continues to enter General Info")]
        public void WhenUserContinuesToEnterGeneralInfo()
        {
            SharedObjects.NewButton.Click();
            Wait();
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
            Wait();
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

        [Given(@"user continues to sales order list")]
        public void GivenUserContinuesToSalesOrderList()
        {
            SharedObjects.ModulesButton.Click();
            SharedObjects.GetModulesListItem("Sales and marketing").WaitUntilClickable(60);
            SharedObjects.GetModulesListItem("Sales and marketing").Click();
            SharedObjects.GetButton("All sales orders").Click();
        }

        [Given(@"user selects Sales Order to upload attachment")]
        public void GivenUserSelectsSalesOrderToUploadAttachment()
        {
            Thread.Sleep(500);
            SharedObjects.LastListedSalesOrder.RightClick();
            SharedObjects.GetButton("View details").Click();
            Wait();
            SharedObjects.AttachmentsButton.Click();
            Wait();
            SharedObjects.GetButton("ctrlAdd").Click();
            SharedObjects.GetButton("DocumentType_File").Click();
            Thread.Sleep(700);
        }

        [When(@"user saves attachment")]
        public void WhenUserSavesAttachment()
        {
            do
            {
                SharedObjects.UploadBrowseButton.WaitUntilClickable();
                Thread.Sleep(700);
                SharedObjects.UploadBrowseButton.Click();
                Thread.Sleep(3000);
                UploadFileToAttachment();
                Thread.Sleep(500);
            } while (SharedObjects.UpLoadPopup.assertElementNotPresent(3, true) == false);

            SharedObjects.SystemSaveButton.WaitUntilClickable();
            SharedObjects.SystemSaveButton.Click();
        }

        [Then(@"user launches '([^']*)' to verify upload successful")]
        public void ThenUserLaunchesToVerifyUploadSuccessful(string batchFileName)
        {
            GivenUserLaunchesSalesOrderBatchScript(batchFileName);
        }


        //
        // Batch
        //

        [Given(@"user launches '([^']*)' sales order batch script")]
        public void GivenUserLaunchesSalesOrderBatchScript(string batchFileName)
        {
            switch (batchFileName.ToLower())
            {
                case "create":
                    batchFileName = "CreateSO";
                    BatchRunner.ExecuteBatchFile(batchFileName);
                    break;
                case "confirm":
                    batchFileName = "ConfirmSO";
                    BatchRunner.ExecuteBatchFile(batchFileName);
                    break;

                default: { throw new Exception($"Batch file {batchFileName} is not a recognized sales order batch script"); }
            }
        }


        //
        // File Upload
        //

        // for Auto ITX3 to work - need to register dll
        // by running command as admin "regsvr32 : C:\Program Files (x86)\AutoIt3\AutoItX\AutoItX3.dll
        private static string path = GetUploadFile("UploadTest");
        private static void UploadFileToAttachment()
        
        {
            AutoItX3 autoIT = new AutoItX3();
            autoIT.Sleep(3000);
            autoIT.ControlFocus("Open", "", "ComboBox1");
            autoIT.ControlSetText("Open", "", "Edit1", $"{path}");
            autoIT.ControlClick("Open", "", "Button1");
        }

        private static string GetUploadFile(string FileName)
        {
            string startPath = Directory.GetCurrentDirectory();
            string ProjectPath = startPath.Substring(0, 63);
            string fileLocation = ProjectPath + $"\\Data\\Uploads\\{FileName}.txt";
            return fileLocation;
        }
    }
}
