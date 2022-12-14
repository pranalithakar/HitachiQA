using OpenQA.Selenium;
using System;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using HitachiQA.Driver;
using System.Drawing;
using System.Drawing.Imaging;

namespace HitachiQA
{
    public class ScreenShot
    {
        public static void Info(String filename = null) => Take(Severity.INFO, filename!);
        public static void Debug(String filename = null) => Take(Severity.DEBUG, filename!);
        public static void Warn(String filename = null) => Take(Severity.WARN, filename!);
        public static void Error(String filename = null) => Take(Severity.ERROR, filename!);
        public static void Critical(String filename = null) => Take(Severity.CRITICAL, filename!);


        /// <summary>
        /// Take screenshot, by defualt the filename will be Severity_CurrentScenario_currentDateTime unless otherwise specified
        /// </summary>
        public static void Take(Severity severity, String filename =null)
        {
            var currentSev = Severity.parseLevel(Environment.GetEnvironmentVariable("CURRENT_SEVERITY_LEVEL")).Level;

            if (currentSev == 0)
            {
                return;
            }
            else if (severity.Level <= currentSev)
            { 

                String fileNameBase = filename ?? string.Format($"{FeatureContext.Current.FeatureInfo.Title}_{ScenarioContext.Current.ScenarioInfo.Title}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");

                fileNameBase = $"{severity.Name}_{fileNameBase}";
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

                if (!Directory.Exists(artifactDirectory)) { Directory.CreateDirectory(artifactDirectory); }

                string pageSource = Setup.driver.PageSource;
                string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                Console.WriteLine($"\nPage Source: {new Uri(sourceFilePath)}\n");


                ITakesScreenshot takesScreenshot = Setup.driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

                    screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
                    
                    var screenshotBitMap = (Bitmap)Image.FromFile(screenshotFilePath);

                    var resultBitMap = new Bitmap(screenshotBitMap.Width, screenshotBitMap.Height + 30);

                    using (Graphics g = Graphics.FromImage(resultBitMap))
                    {
                        g.DrawString(Setup.driver.Url, new Font("Arial", 20), Brushes.Red, new PointF(0, 0));
                        g.DrawImageUnscaled(screenshotBitMap, 0, 30);

                    }
                    screenshotBitMap.Dispose();
                    File.Delete(screenshotFilePath);

                    resultBitMap.Save(screenshotFilePath, ImageFormat.Png);
                    resultBitMap.Dispose();

                    Console.WriteLine($"SCREENSHOT[ {screenshotFilePath} ]SCREENSHOT");
                }
            }
        }
    }
}
