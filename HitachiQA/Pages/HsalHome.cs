using HitachiQA.Driver;

namespace HitachiQA.Pages
{
    class HsalHome 
    {
        public static string URL_PATH = "/";
        public static void navigate() => UserActions.Navigate(URL_PATH);
        public static Element OpenSearch => new Element("//*[@id='SupportNaviSearch']");
        public static Element SearchInput => new Element("//*[@id='MF_form_phrase']");
        public static Element SearchButton => new Element("//*[@class='SearchBtn']");
        public static Element ResultSearchInput => new Element("//input[@title='search query']");
    }
}

