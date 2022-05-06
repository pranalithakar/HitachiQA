using HitachiQA.Driver;

namespace HitachiQA.Pages
{
    public class WorkOrderPage
    {
        public static Element FabInput => new Element("//*[@data-id='msdyn_serviceaccount-FieldSectionItemContainer'] //Input");
        public static Element WOType => new Element("//*[contains(@id, '_workordertype_4_InputSearch')] //input");
        public static Element WOSaving => new Element("//*[contains(text(), 'Saving...')]");
        public static Element WOStatus => new Element("//h1[contains(text(), 'New Work Order')] //*");


        public static Element SelectOptionItem(string optionName)
        {
            return new Element($"//*[@aria-label='{optionName}']");
        }
    }
}
