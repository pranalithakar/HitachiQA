using HitachiQA.Data.Entity;
using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace HitachiQA.StepDefinition.F_OSteps
{
    [Binding]
    public class VendorCreationSteps
    {
        public Address Address;

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

        [Given(@"user continues to enter new Vendor section")]
        public void GivenUserContinuesToEnterNewVendorSection()
        {
            SharedObjects.ModulesButton.Click();
            SharedObjects.GetModulesListItem("Procurement and sourcing").Click();
            var expanded = SharedObjects.GetButton("Vendors").GetAttribute("aria-expanded");
            if(expanded != "true")
            {
                SharedObjects.GetButton("Vendors").Click();
            }
            SharedObjects.GetButton("All vendors").Click();
        }

        [Given(@"user continues to enter General Info")]
        public void GivenUserContinuesToEnterGeneralInfo()
        {
            Thread.Sleep(800);
            SharedObjects.NewButton.Click();
            string uniqueID = Functions.GetRandomInteger().ToString();
            SharedObjects.GetDropdown("Type").SelectDropdownOptionByText("Person");
            SharedObjects.GetDropdown("Group").setText("one");
            SharedObjects.GetUniqueElement("One-time vendors").Click();
            SharedObjects.GetTextField("Identification_AccountNum").setText("autoVendor" + uniqueID);
            SharedObjects.GetTextField("Vendor account").setText("autoVendor" + uniqueID);
            SharedObjects.GetTextField("Name_FirstName").setText("TheFirst");
            SharedObjects.GetTextField("Name_MiddleName").setText("TheSecond");
            SharedObjects.GetTextField("Name_LastName").setText("TheLast");
        }

        [Given(@"user enters vender Address Info")]
        public void GivenUserEntersVenderAddressInfo()
        {
            Dictionary<string, string> inputs = Address.AddressInputs;
            SharedObjects.GetTabSection("Addresses").Click();
            SharedObjects.GetButton("NewAddress").Click();
            foreach(var entry in inputs)
            {
                try
                {
                    SharedObjects.GetTextField(entry.Key).setText(entry.Value);
                }
                catch
                {
                    SharedObjects.GetDropdown(entry.Key).SelectDropdownOptionByText(entry.Value);
                }
            }
        }


        [Then(@"user successfully saves new Vendor")]
        public void ThenUserSuccessfullySavesNewVendor()
        {
            
        }
    }
}
