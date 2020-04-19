using System.Text;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using Rentals._02_Utility_Tier;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rentals._03_Application_Tier.PageObjects
{
    class DeleteCustomer
    {
        public IWebDriver oWebDriver;
        //public Hashtable oHashTable;

        public DeleteCustomer(IWebDriver webDriver) { oWebDriver = webDriver; }

        public DeleteCustomer deleteCustomer()
        {
            try
            {
                var oPropDict = ExcelUtil.populateMultiDictionaryFromExcel("Rentals_Scenario_One", "DeleteCustomer" + ExcelUtil.GetData("ApplnVersion"));

                // oPropDict= ExcelUtil.populatDictionaryFromExcel("Rentals_Scenario_One", "CreateCustomer");
                var customerid = WebPageUtility.getTextFromTableByXpath(oPropDict["CustomerId"]);
                WebPageUtility.clickElementByXpath(oPropDict["DeleteLink"]);
                WebPageUtility.inputTextByXpath(oPropDict["CustomerValue"], customerid);
                WebPageUtility.clickElementByXpath(oPropDict["SubmitBtn"]);
                IAlert alert = oWebDriver.SwitchTo().Alert();
                alert.Accept();
                alert.Accept();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return new DeleteCustomer(oWebDriver);
        }

    }

}
