using HitachiQA.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiQA.Pages
{
    public class SharedObjects
    {
        //
        // Heahers, Titles, etc
        //

        public static Element PublishedAppsTitle => new Element("//h2[contains(text(), 'Published Apps')]");


        //
        // Left Navigation
        //

        public static Element GetLeftNavItem(string LeftNavItem)
        {
            return new Element($"//*[@role='treeitem' and @title='{LeftNavItem}']");
        }


        public static Element GetPublishedApp(string AppName)
        {
            return new Element($"//*[@role='listitem'][descendant::*[@title='{AppName}']]");
        }

        public static Element GetInputField(string FieldName)
        {
            return new Element($"//input[@type='{FieldName}']");
        }

        public static Element GetButton(string ButtonName)
        {
            return new Element($"//*[@value='{ButtonName}'] |" +
                               $"//button[@aria-label='{ButtonName}']");
        }
    }
}
