﻿using System;
using System.Collections.Generic;
using System.Text;
using HitachiQA.Helpers;
using HitachiQA.Driver;
using OpenQA.Selenium;

namespace HitachiQA.Pages
{
    class HsalHome 
    {
        public static string URL_PATH = "/";
        public static void navigate() => UserActions.Navigate(URL_PATH);
        public static Element OpenSearch => new Element("//*[@id='Search']");
        public static Element SearchInput => new Element("//*[@id='MF_form_phrase']");
        public static Element SearchButton => new Element("//*[@class='SearchTextBox']");
        public static Element ResultSearchInput => new Element("//input[@class='mf_finder_searchBox_query_input']");
        
    }
}
