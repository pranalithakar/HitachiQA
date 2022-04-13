using HitachiQA.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiQA.Pages
{
    public class SharedObjects
    {
        public static Element AdvancedButton => new Element("//*[@id='details-button']");
        public static Element ConnectionContinueButton => new Element("//*[@id='proceed-link']");
        //public static Element DashboardTitle => new Element("//*[contains(text(), 'Constose Entertainmenet System')]");

        // Top Navigation

        public static Element NewButton => new Element("//button[@name='SystemDefinedNewButton']");

        // left Navigation

        public static Element ModulesButton => new Element("//*[@id='navPaneModuleID']");
        public static Element WorkSpaceGroupButton => new Element("//*[@id='navPaneWorkSpaceGroupID']");
        public static Element RecentsButton => new Element("//*[@id='navPaneRecentsID']");
        public static Element FavoritesButton => new Element("//*[@id='navPaneFavoritesID']");



        public static Element GetButton(string DisplayName)
        {
            return new Element($"//button[contains(text(), '{DisplayName}')] |" +
                                $"//input[@value='{DisplayName}'] |" +
                               $"//button[@data-dyn-controlname='{DisplayName}'][descendant::*[text() = 'Add']] |" +
                              $"//*[@aria-label='{DisplayName}']");
        }

        public static Element GetWorkSpace(string SpaceName)
        {
            return new Element($"//*[@role='button'][descendant::*[contains(text(), '{SpaceName}')]]");
        }

        public static Element GetModulesListItem(string ModuleName)
        {
            return new Element($"//*[@role='treeitem' and @data-dyn-title='{ModuleName}']");
        }

        public static Element GetTextField(string LabelName)
        {
            return new Element($"//input[preceding-sibling::label[contains(text(), '{LabelName}')]] |" +
                               $"//input[@type='{LabelName}'] |" + 
                               $"//input[@name='{LabelName}']");
        }

        public static Element GetDropdown(string DropdownName)
        {
            return new Element($"//input[@name='{DropdownName}']");
        }

        public static Element GetTabSection(string SectionName)
        {
            return new Element($"//*[@role='tab'][descendant::*[text() = '{SectionName}']]");
        }
    }
}
