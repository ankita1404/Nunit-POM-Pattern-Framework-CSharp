using System.Text;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using Rentals._02_Utility_Tier;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace Rentals._03_Application_Tier.PageObjects
{
    class CustomerPage
    {
        public IWebDriver oWebDriver;
        public WebDriverWait wait;
        public IAlert alert;
        //public Hashtable oHashTable;
       
        public CustomerPage(IWebDriver webDriver) 
        { 
            oWebDriver = webDriver; 
        }

        public CustomerPage createCustomer()
        {
            try
            {
                var oPropDict = ExcelUtil.populateMultiDictionaryFromExcel("Rentals_Scenario_One", "CreateCustomer" + ExcelUtil.GetData("ApplnVersion"));

                // oPropDict= ExcelUtil.populatDictionaryFromExcel("Rentals_Scenario_One", "CreateCustomer");

                oWebDriver.Manage().Window.Maximize();

                WebPageUtility.clickElementByXpath(oPropDict["New_Customer"]);
                WebPageUtility.inputTextByXpath(oPropDict["Customer_Name"], ExcelUtil.GetData("CustName"));
                WebPageUtility.inputTextByXpath(oPropDict["DOB"], ExcelUtil.GetData("DOB"));
                WebPageUtility.inputTextByXpath(oPropDict["Address"], ExcelUtil.GetData("Address"));
                WebPageUtility.inputTextByXpath(oPropDict["City"], ExcelUtil.GetData("City"));
                WebPageUtility.inputTextByXpath(oPropDict["State"], ExcelUtil.GetData("State"));
                WebPageUtility.inputTextByXpath(oPropDict["Pin"], ExcelUtil.GetData("PIN"));
                WebPageUtility.inputTextByXpath(oPropDict["MobileNum"], ExcelUtil.GetData("MobileNumber"));
                WebPageUtility.inputTextByXpath(oPropDict["Email"], ExcelUtil.GetData("E-Mail"));
                WebPageUtility.inputTextByXpath(oPropDict["Password"], ExcelUtil.GetData("CustPassword"));
                Thread.Sleep(1000);
                WebPageUtility.clickElementByXpath(oPropDict["Submit"]);
                wait = new WebDriverWait(oWebDriver, TimeSpan.FromSeconds(Env.timeoutInSeconds));
                if (wait.Until(ExpectedConditions.AlertIsPresent()) == null)
                {
                    Console.WriteLine("Alert was not present");
                    Reporter.ReportEvent("Pass", "Create Customer", "Test case passed");
                }
                else
                {
                    alert = oWebDriver.SwitchTo().Alert();
                    alert.Accept();
                    Reporter.ReportEvent("Fail", "Create Customer", "Test case got failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return new CustomerPage(oWebDriver);
        }
    }

}

