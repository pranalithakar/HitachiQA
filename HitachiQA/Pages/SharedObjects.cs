using HitachiQA.Driver;

namespace HitachiQA.Pages
{
    public class SharedObjects
    {    
        // Top Navigation

        public static Element NewButton => new Element("//button[@name='SystemDefinedNewButton']");
        public static Element FormSaveButton => new Element("(//button[descendant::*[contains(text(), 'Save')]])[1]");
        public static Element SystemSaveButton => new Element("//button[@name='SystemDefinedSaveButton']");

        // left Navigation

        public static Element ModulesButton => new Element("//*[@id='navPaneModuleID']");
        public static Element WorkSpaceGroupButton => new Element("//*[@id='navPaneWorkSpaceGroupID']");
        public static Element RecentsButton => new Element("//*[@id='navPaneRecentsID']");
        public static Element FavoritesButton => new Element("//*[@id='navPaneFavoritesID']");

        // Important Object - titles, headers, etc

        public static Element SavedVendor => new Element("//span[@id='vendtablelistpage_3_HeaderTitle']");
        public static Element AttachmentsButton => new Element("(//button[@name='SystemDefinedAttachButton'])[2]");




        public static Element LastListedSalesOrder => new Element("(//*[@id='SalesTable_SalesIdAdvanced_151_0'] //input[@aria-label='Sales order'])[last()]");

        public static Element GetButton(string DisplayName)
        {
            return new Element($"//button[contains(text(), '{DisplayName}')] |" +
                                $"//input[@value='{DisplayName}'] |" +
                               $"//button[@data-dyn-controlname='{DisplayName}'][descendant::*[text() = 'Add']] |" +
                              $"//*[@aria-label='{DisplayName}'] |" +
                              $"//button[descendant::*[contains(text(), '{DisplayName}')]] |" +
                              $"//button[@name='{DisplayName}']");
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
            return new Element($"//input[@name='{DropdownName}'] |" +
                               $"//input[@role='textbox' and @id='vendtablelistpage_3_Posting_VendGroup_input'][preceding-sibling::label[contains(text(), '{DropdownName}')]]");
        }

        public static Element GetTabSection(string SectionName)
        {
            return new Element($"//*[@role='tab'][descendant::*[text() = '{SectionName}']]");
        }

        public static Element GetUniqueElement(string element)
        {
            return new Element($"//input[@value='{element}']");
        }
    }
}
