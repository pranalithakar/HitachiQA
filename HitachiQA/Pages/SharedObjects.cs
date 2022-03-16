using HitachiQA.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiQA.Pages
{
    public class SharedObjects
    {
        public static Element GetPublishedApp(string AppName)
        {
            return new Element($"//*[@role='listitem'][descendant::*[@title='{AppName}']]");
        }
    }
}
