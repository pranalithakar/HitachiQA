using System;
using System.Collections.Generic;
using System.Text;
using HitachiQA.Helpers;
using HitachiQA.Driver;
using OpenQA.Selenium;

namespace HitachiQA.Pages
{
    class Home 
    {
        public static string URL_PATH = "/";
        public static void navigate() => UserActions.Navigate(URL_PATH);
        public static Element SearchInput => new Element("//input[@title='Search']");
        public static Element SearchButton => new Element("//input[@type='submit' and @aria-label='Google Search']");


    }
}

