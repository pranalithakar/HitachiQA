using System;
using System.Collections.Generic;
using System.Text;

using BoDi;
using Microsoft.Azure.Cosmos;
using Dynamitey;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using HitachiQA.Helpers;
using System.IO;
using System.Reflection;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;

namespace HitachiQA.Driver
{
    [Binding]
    public class Setup
    {
        private static IObjectContainer _objectContainer;
        public static IWebDriver driver = null;
        public static IWebDriver driverTemp;
        public static String SourceDir = Environment.GetEnvironmentVariable("SourceDir") ?? "../" ;
        public static Dictionary<int, int> TestCaseOutcome = new Dictionary<int, int> ();
        public static bool isNoBrowserFeature = false;


        public Setup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            invokeEnvironmentVariables(Environment.GetEnvironmentVariable("ENVIRONMENT_FILE") ?? Environment.GetEnvironmentVariable("ENVIRONMENT_FILE", EnvironmentVariableTarget.User) ?? "default.env.json");

            
        }
        public static void invokeNewDriver()
        {
          
            string browser = Environment.GetEnvironmentVariable("BROWSER", EnvironmentVariableTarget.Process);
            switch (browser?.ToLower())
            {
                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArgument("--window-size=1920,1080");
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new NotImplementedException($"Environment variable BROWSER value={browser} is not supported");
            }
            driver.Manage().Window.Maximize();

        }


        [BeforeScenario("newWindow", Order =1)]
        public static void pre_NewWindow()
        {
            driverTemp = driver;
            invokeNewDriver();
        }
        [AfterScenario("newWindow", Order=1)]
        public static void post_NewWindow()
        {
            driver.Quit();
            driver = driverTemp;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (driver != null)
            {
                _objectContainer.RegisterInstanceAs(driver);
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext _scenarioContext)
        {
            double totalTime = 0;
            foreach (var timespan in RestAPI.timeSpans)
            {
                Log.Debug($"[{timespan.seconds.ToString("0.0000")}] [{timespan.key}]");
                totalTime += timespan.seconds;
            }

            Log.Debug("Total Time spent on API " + totalTime.ToString("0.0000"));

            IEnumerable<string> testCases = _scenarioContext.ScenarioInfo.Tags.Where(it => it.ToLower().StartsWith("tc:")).Select(it => it.Substring(3));
            List<int> testCaseids = new List<int>();

            foreach (var tc in testCases)
            {
                if (int.TryParse(tc, out int testCaseId))
                {
                    testCaseids.Add(testCaseId);
                }
                else
                {
                    Functions.handleFailure(new ArgumentException($"Scenario: {_scenarioContext.ScenarioInfo.Title} Test Case {tc} is invalid"));
                }

            }
            if (_scenarioContext.TestError != null && !isNoBrowserFeature)
            {
                if (isNoBrowserFeature == false)
                {
                    ScreenShot.Error();
                }

                foreach (var testCaseId in testCaseids)
                {
                    if(! TestCaseOutcome.ContainsKey(testCaseId))
                    {
                        TestCaseOutcome.Add(testCaseId, Devops.OUTCOME_FAIL);
                    }
                   
                }

            }
            else
            {
                foreach (var testCaseId in testCaseids)
                {
                    //if it already contains an outcome (pass or fail) we do not want to alter it
                    if ( !TestCaseOutcome.ContainsKey(testCaseId))
                    {
                        TestCaseOutcome.Add(testCaseId, Devops.OUTCOME_PASS);
                    }
                    else
                    {
                        TestCaseOutcome[testCaseId] = Devops.OUTCOME_PASS;
                    }
                }
            }


        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if(!featureContext.FeatureInfo.Tags.Contains("NoBrowser"))
            {
                isNoBrowserFeature = false;
                if (driver == null)
                {
                    invokeNewDriver();
                }
            }
            else
            {
                isNoBrowserFeature = true;
            }

        }
        [AfterFeature]
        public static void AfterFeature()
        {
            try
            {
                Devops.markTestCases(TestCaseOutcome);
            }
            catch(Exception ex)
            {
                Log.Critical(ex.Message);
                Log.Critical(ex.StackTrace);

            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (Severity.parseLevel(Environment.GetEnvironmentVariable("CURRENT_SEVERITY_LEVEL")).Level != Severity.INT_DEBUG)
            {
                driver.Quit();
            }
            Cosmos.client.Dispose();
        }

        private static void invokeEnvironmentVariables(string JsonEnvironmentFileName )
        {
            JObject environmentVariables = JsonConvert.DeserializeObject<JObject>(new StreamReader(Path.Combine(SourceDir, JsonEnvironmentFileName.ToLower())).ReadToEnd());
            //automation specific keyvault
            var KeyVault = new KeyVault(environmentVariables?["KEYVAULT_URI"]?.ToString());
            

            //application under test's keyvault
            var appKeyVault = new KeyVault(environmentVariables?["APP_KEYVAULT_URI"]?.ToString());
            
            //
            //Loop through each variable, 
            //checks if the variable has already been set 
            //Only sets variables if they have not already been set
            //
            //sets Secret and it's value as Environment Variables if provided SECRETNAME
            foreach (var variable in environmentVariables)
            {
                if (Environment.GetEnvironmentVariable(variable.Key) == null)
                {
                    //Log.Info($"Setting {variable.Key} = {variable.Value}");
                    Environment.SetEnvironmentVariable(variable.Key, variable.Value.ToString());

                }

                //if the variable is a secret name we will load the secret as an environment variable if it does not already exist
                if (variable.Key.Contains("SECRETNAME") && Environment.GetEnvironmentVariable(variable.Value.ToString()) == null)
                {
                    var secretName = variable.Value.ToString();

                    //Log.Debug(secretName);
                    if(KeyVault.GetSecret(secretName, true) is var secretValue && !string.IsNullOrWhiteSpace(secretValue))
                    {
                        Environment.SetEnvironmentVariable(secretName, secretValue);
                    }
                    else
                    {
                        Environment.SetEnvironmentVariable(secretName, appKeyVault.GetSecret(secretName));
                    }
                }
                
            }


        }
    }
}
