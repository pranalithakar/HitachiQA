using System;
using System.Collections.Generic;
using System.Text;
using ApolloQA.Source.Helpers;
using ApolloQA.Source.Driver;
using OpenQA.Selenium;

namespace ApolloQA.Pages
{
    class Home 
    {
        public static string URL_PATH = "/home";
        public static void navigate() => UserActions.Navigate(URL_PATH);

        public static Element ApolloIcon => new Element("//fa-icon[@routerlink='home']");





    }
}

