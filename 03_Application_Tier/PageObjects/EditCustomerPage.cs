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
    public class EditCustomerPage
    {
        public IWebDriver oWebDriver;
        //public Hashtable oHashTable;

        public EditCustomerPage(IWebDriver webDriver) 
        { 
            oWebDriver = webDriver; 
        }
        public EditCustomerPage CustomerEdit()
        {
            try
            {
                var oPropDict = ExcelUtil.populateMultiDictionaryFromExcel("Rentals_Scenario_One", "EditCustomer" + ExcelUtil.GetData("ApplnVersion"));

                var customerid = WebPageUtility.getTextFromTableByXpath(oPropDict["CustomerId"]);
                WebPageUtility.clickElementByXpath(oPropDict["EditLink"]);

                WebPageUtility.inputTextByXpath(oPropDict["CustomerValue"], customerid);
                WebPageUtility.clickElementByXpath(oPropDict["SubmitBtn"].ToString());
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["Customer_Name"], ExcelUtil.GetData("CustName"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["DOB"], ExcelUtil.GetData("DOB"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["Address"], ExcelUtil.GetData("Address"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["City"], ExcelUtil.GetData("City"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["State"], ExcelUtil.GetData("State"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["Pin"], ExcelUtil.GetData("PIN"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["MobileNum"], ExcelUtil.GetData("MobileNumber"));
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["Email"], ExcelUtil.GetData("E-Mail"));
                WebPageUtility.inputTextByXpath(oPropDict["MobileNum"], ExcelUtil.GetData("ChangeMobileNumber"));
                //Thread.Sleep(1000);
                WebPageUtility.clickElementByXpath(oPropDict["Submit"]);
                WebPageUtility.checkTextExistInElementByXPath(oPropDict["Mobile"], ExcelUtil.GetData("ChangeMobileNumber"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return new EditCustomerPage(oWebDriver);
        }
    }

}
