using System;
using System.Data;
using System.IO;
using System.Diagnostics;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using Rentals._02_Utility_Tier;
using Rentals._03_Application_Tier.PageObjects;
using System.Collections.ObjectModel;

namespace Rentals._03_Application_Tier.ApplicationTests
{
  
        [TestClass] 
    public class TestDriver
    {

        public static IWebDriver webDriver;
      
        [TestInitialize]
        public void SetAppLaunch()
        {

            try
            {
                Console.WriteLine("##############Test Started");
                string userNameValue = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string[] stringSeparators = new string[] { "\\" };
                string[] stringResult = userNameValue.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                Env.UserName = stringResult[1];
                Env.boolDebugFlag = false;
                Helper.setRelativePath();

                if (true)
                {

                    Reporter.GetEnvConfigDetails();
                    Helper.InitalizeWebDriver(ref  webDriver);
                    WebPageUtility.webDriver = webDriver;
                    //if (Env.strReportFlag == "Y")
                    //Reporter.setTestResultFoders();
                    ExtentReports Report = Reporter.createReporterInstance();
                    Reporter.startTest("Application Launch", "Initial Application Launch");

                    if (WebPageUtility.naviGateToUrl(Env.strCurrentURL, Env.strBrowserTitle))
                    {
                        Reporter.ReportEvent("Pass", "[Navigation Done to URL] - " + Env.strCurrentURL, " [and has Guru99 ]");
                        //Reporter.startTest("Application Launch", "Initial Application Launch");
                    }

                    if (Env.strLoginFlag == "Y")
                    {
                        LoginPage oLogin = new LoginPage(TestDriver.webDriver);
                        oLogin.userLogin();
                        Reporter.ReportEvent("Pass", "Initial Page", "Launched and Logged into Application");

                    }
                }
            }

            catch (Exception Ex)
            {
                Console.WriteLine("Exception in launch :" + Ex.Message);
            }
        }


        [TestCleanup]
        public void TestClosure()
        {
            try
            {
                if (!Env.boolDebugFlag)
                {
                    Reporter.EndTest();
                    Reporter.Flush();
                    webDriver.Quit();
                    //Process.Start("firefox.exe", Reporter.strTestResHTMLFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

    }
}
