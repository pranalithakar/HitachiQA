using System;
using System.Collections.Generic;
using System.Text;
using ApolloQA.Source.Helpers;
using ApolloQA.Source.Driver;
using OpenQA.Selenium;

namespace ApolloQA.Pages
{
    class Login 
    {
        public static string URL_PATH = "/";
        public static void navigate() => UserActions.Navigate(URL_PATH);
        public static Element usernameField => new Element("//input[@type='email' and @name='loginfmt']");
        public static Element passwordField => new Element("//input[@type='password' and @name='passwd']");
        public static Element nextButton => new Element(By.Id("idSIButton9"));
        public static Element noButton => new Element(By.Id("idBtn_Back"));

    }
}

