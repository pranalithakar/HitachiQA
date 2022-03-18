using HitachiQA.Data.Entity;
using HitachiQA.Driver;
using HitachiQA.Helpers;
using HitachiQA.Pages;
using System;
using System.Collections.Generic;
using System.Text;
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
            var button = SharedObjects.GetButton("Advanced");
            if(button.assertElementIsPresent(2, true) == true)
            {
                button.Click();
                SharedObjects.ConnectionContinueButton.Click();
            }
            SharedObjects.DashboardTitle.assertElementIsPresent();
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
            SharedObjects.NewButton.Click();
            string uniqueID = Functions.GetRandomInteger().ToString();
            SharedObjects.GetTextField("Vendor account").setText("autoVendor" + uniqueID);
            SharedObjects.GetTextField("Name").setText("autoVendor" + uniqueID);
            SharedObjects.GetDropdown("Type").SelectDropdownOptionByText("Person");
            SharedObjects.GetDropdown("Group").SelectDropdownOptionByText("One-time vendors");

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
