using System;
using System.Data;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using Rentals._02_Utility_Tier;
using Rentals._03_Application_Tier.PageObjects;

namespace Rentals._03_Application_Tier.ApplicationTests
{

    [TestClass]
    public class Rentals_Scenario_Two : TestDriver
    {        
        [TestMethod]
        public void CreateCustomer_01()
        {
            try
            {
                Console.WriteLine("Create Customer");
                Reporter.startTest("Create_Cutomer", "Creating new customer scenario");
                Reporter.AssignCategory("Create_Cutomer", "Customer");
                Reporter.strCurrentTestID = "TS001";
                //*******************************************************************************************************
                string dataFileToRefer = null; string dataSheetToRefer = null; string RunFlag = null;
                Helper.getTestDataDetails("'TS001'", out dataFileToRefer, out dataSheetToRefer, out  RunFlag);
                DataTable oDTable = ExcelUtil.ExcelToTable(Env.strRelativePath + Env.strApplicationFd + dataFileToRefer + ".xlsx", dataSheetToRefer);
                DataRow[] oDataRows = oDTable.Select("Iteration_Run = 'Y'");
                foreach (DataRow oDataRow in oDataRows)
                {
                    ExcelUtil.oCurrentDataRow = oDataRow;
                    CustomerPage oCreateNewCustomer = new CustomerPage(TestDriver.webDriver);
                    oCreateNewCustomer.createCustomer();
                   /* EditCustomerPage oEditCustomer = new EditCustomerPage(TestDriver.webDriver);
                    oEditCustomer.CustomerEdit();
                    DeleteCustomer oDeleteCustomer = new DeleteCustomer(TestDriver.webDriver);
                    oDeleteCustomer.deleteCustomer();*/
                 }

                Assert.Fail("Known Fail");
                //Reporter.EndTest();
            }

            catch (Exception Ex)
            {
                //Reporter.EndTest();
                Console.WriteLine("Create Customer" + Ex.Message);
                Assert.Fail("Known Fail");
            }

        }
       [TestMethod]
        public void EditCustomer_01()
        {
            try
            {
                Console.WriteLine("Create Customer");
                Reporter.startTest("ReCreate_Cutomer", "Adding onec again new customer scenario");
                Reporter.AssignCategory("ReCreate_Cutomer", "Customer2");
                Reporter.strCurrentTestID = "TS002";
                //*******************************************************************************************************
                string dataFileToRefer = null; string dataSheetToRefer = null; string RunFlag = null;
                Helper.getTestDataDetails("'TS002'", out dataFileToRefer, out dataSheetToRefer, out  RunFlag);
                DataTable oDTable = ExcelUtil.ExcelToTable(Env.strRelativePath + Env.strApplicationFd + dataFileToRefer + ".xlsx", dataSheetToRefer);
                DataRow[] oDataRows = oDTable.Select("Iteration_Run = 'Y'");
                foreach (DataRow oDataRow in oDataRows)
                {
                    ExcelUtil.oCurrentDataRow = oDataRow;
                    CustomerPage oCreateNewCustomer = new CustomerPage(TestDriver.webDriver);
                    oCreateNewCustomer.createCustomer();
                    /*EditCustomerPage oEditCustomer = new EditCustomerPage(TestDriver.webDriver);
                    oEditCustomer.CustomerEdit();
                    DeleteCustomer oDeleteCustomer = new DeleteCustomer(TestDriver.webDriver);
                    oDeleteCustomer.deleteCustomer();*/
                }

                Assert.Fail("Known Fail");
                Reporter.EndTest();
                //Process.Start("firefox.exe", Reporter.strTestResHTMLFilePath);
            }

            catch (Exception Ex)
            {
                Reporter.EndTest();
                Console.WriteLine("Create Customer" + Ex.Message);
                Assert.Fail("Known Fail");
                //Process.Start("firefox.exe", Reporter.strTestResHTMLFilePath);
            }

        }
    }
}
