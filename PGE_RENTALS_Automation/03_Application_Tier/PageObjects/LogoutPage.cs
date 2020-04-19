using System;
using System.IO;
using System.Text;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
//using WebDriver_Demo.Libraries;
//using WebDriver_Demo.Libraries.Utils;
using Rentals._02_Utility_Tier;
using System.Resources;

using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Rentals._03_Application_Tier.PageObjects
{
    class LogoutPage
    {
        public static IWebDriver webDriver;

        public LogoutPage LogOut()
        {
            try
            {
                if (WebPageUtility.clickElementByXpath("//ul[@class='menusubnav']//li/a[@href='Logout.php']"))
                {
                    Reporter.ReportEvent("Pass", "Log out successful", "Exception");
                }
                else
                {
                    Reporter.ReportEvent("Fail", "Did not click", "Exception");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return new LogoutPage();
        }
    }
}
