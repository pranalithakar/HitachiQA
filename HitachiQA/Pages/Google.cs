using System;
using System.Collections.Generic;
using System.Text;
using HitachiQA.Helpers;
using HitachiQA.Driver;
using OpenQA.Selenium;

namespace HitachiQA.Pages
{
    class Google 
    {
        public static string GOOGLE_URL = "https://google.com";
        public static void navigate() => UserActions.Navigate(GOOGLE_URL);
        public static Element SearchInput => new Element("//input[@title='Search']");
        public static Element SearchButton => new Element("//input[@type='submit' and @aria-label='Google Search']");

    }
}

