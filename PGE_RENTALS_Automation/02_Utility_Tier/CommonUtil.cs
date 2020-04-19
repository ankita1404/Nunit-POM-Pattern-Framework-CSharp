using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Text.RegularExpressions;
using Rentals._02_Utility_Tier;
using System.Reflection;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System.Diagnostics;

namespace Rentals
{
    // class name: ApprovalQueuePage (included to hold common global entities)
    // created: March 28, 2016 10:00 AM
    // Author: Ajay  
    // Revisions: Ajay (01/04/2016) - Added global helper methods

    public class CommonUtil
    {
        //************Global Entity variable*********************** 

        public static IWebDriver WebDriver { get; set; }
        public static IWebElement webElement { get; set; }
        public static Hashtable HashTable { get; set; }
        public static WebDriverWait wait { get; set; }
        public static IList<IWebElement> webElements { get; set; }

        //************Global Helper Methods*************************


        // method name: getTestDataDetails (included to get the test data from excel )
        // created: March 21, 2016 03:30 PM
        // Author: Sanketh
        // Revisions:

        public static void getTestDataDetails(string TestScriptID, out string xlsName, out string xlsSheetName, out string RunFlag)
        {
            try
            {

                System.Data.DataTable oTableEnv = ExcelUtil.ExcelToTable(Env.strRelativePath + Env.Configuration_Tier + Env.sysFileSeperator + Env.EnvironmentFiles + Env.sysFileSeperator + Env.Configuration_xlsx, Env.Scenarios);
                System.Data.DataRow[] oDataRowsEnv = oTableEnv.Select(Env.TestScriptId + TestScriptID);
                xlsName = null;
                RunFlag = null;
                xlsSheetName = null;

                if (oDataRowsEnv.Length != 0)
                {
                    ExcelUtil.oCurrentDataRow = oDataRowsEnv[0];
                    xlsName = ExcelUtil.GetData(Env.TestData);
                    xlsSheetName = ExcelUtil.GetData(Env.TestSheet);
                    RunFlag = ExcelUtil.GetData(Env.Run);
                }
                oTableEnv = null;
                oDataRowsEnv = null;
            }
            catch (Exception ex)
            {
                Reporter.ReportEvent("Fail", "getTestDataDetails", ex.Message.ToString());
                throw ex;
            }
        }

        // method name: setRelativePath (included to set the all file Relative Path )
        // created: March 21, 2016 11:00 AM
        // Author: Sanketh
        // Revisions

        public static void setRelativePath()
        {
            try
            {
                string strProjectName = Assembly.GetExecutingAssembly().GetName().Name;
                string startupPath = Environment.CurrentDirectory;
                string[] strRootPath = Regex.Split(startupPath, Env.bin);
                Env.strRelativePath = strRootPath[0];
                Console.WriteLine(Env.strRelativePath);
            }
            catch (Exception ex)
            {
                Reporter.ReportEvent("Fail", "setRelativePath", ex.Message.ToString());
                throw ex;
            }
        }


        // method name: setRelativePath (included to get the time stamp of file creation )
        // created: March 21, 2016 11:00 AM
        // Author: Sanketh
        // Revisions

        public static string getTimeStamp()
        {
            string strDateTime;
            try
            {
                strDateTime = DateTime.Now.ToString();
                strDateTime = strDateTime.Replace("-", "_");
                strDateTime = strDateTime.Replace(":", "_");
                strDateTime = strDateTime.Replace(" ", "_");
                strDateTime = strDateTime.Replace("/", "_");
            }
            catch (Exception ex)
            {
                Reporter.ReportEvent("Fail", "getTimeStamp", ex.Message.ToString());
                throw ex;
            }
            return strDateTime;
        }

        // method name: InitalizeWebDriver (included to Initalize All browser Web Driver )
        // created: March 21, 2016 11:00 AM
        // Author: Sanketh
        // Revisions

        public static void InitalizeWebDriver(ref  IWebDriver webDriver, ref WebDriverWait wait)
        {
            try
            {
                string browser = Env.strCurrentBrowser; //Get browser name from the config

                //get relative path
                if (Env.strRelativePath == null)
                {
                    setRelativePath();
                }

                switch (browser)
                {
                    case "Chrome":
                        webDriver = new ChromeDriver();
                        break;
                    case "Firefox":
                        webDriver = new FirefoxDriver();
                        break;
                    case "Internet Explorer":

                        string IE_DRIVER_PATH = Env.strRelativePath + "ExternalDlls\\";
                        var options = new InternetExplorerOptions()
                        {
                            IgnoreZoomLevel = true,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true
                        };
                        webDriver = new InternetExplorerDriver(IE_DRIVER_PATH, options);
                        ICapabilities browserCapabilities = ((RemoteWebDriver)webDriver).Capabilities;
                        Env.strcurrentBrowserVersion = browserCapabilities.Version;
                        break;


                    default:
                        string default_IE_DRIVER_PATH = Env.strRelativePath + "ExternalDlls\\";
                        var default_options = new InternetExplorerOptions()
                        {

                            IntroduceInstabilityByIgnoringProtectedModeSettings = true
                        };
                        webDriver = new InternetExplorerDriver(default_IE_DRIVER_PATH, default_options);
                        break;

                }
                wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
                webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
                webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                Reporter.ReportEvent("Fail", "InitalizeWebDriver", ex.Message.ToString());
                throw ex;
            }
        }

        // method name: GenerateRandomNumber (included to generate random number with number of digits passed as a parameter )
        // created: April 30, 2016 01:50 PM
        // Author: Revanth
        // Revisions
        public static string GenerateRandomNumber(int numberOfDigits)
        {
            string numberAsString = "";
            try
            {
                const string numbers = "0123456789";
                Random random = new Random();
                StringBuilder builder = new StringBuilder(6);
                for (var i = 1; i < numberOfDigits + 1; i++)
                {
                    builder.Append(numbers[random.Next(0, numbers.Length)]);
                }
                numberAsString = builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numberAsString;
        }

        public static void ChangeExplicitImplicitWaitTime(int seconds)
        {
            try
            {
                CommonUtil.wait = new WebDriverWait(CommonUtil.WebDriver, TimeSpan.FromSeconds(seconds));
                CommonUtil.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
                CommonUtil.WebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(seconds));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KILL_ALL(string strProcess)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(strProcess))
                {
                    proc.Kill();
                    Console.WriteLine("Killed " + strProcess);
                }
            }
            catch (Exception ex)
            {
                Reporter.ReportEvent("Fail", "Kill Process function", ex.Message);
                throw ex;
            }
        }
    }
}