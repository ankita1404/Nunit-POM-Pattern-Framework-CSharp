using System;
using System.IO;
using System.Text;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using Rentals._02_Utility_Tier;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System.Data;
using System.Collections;

namespace Rentals._03_Application_Tier.PageObjects
{
    class LoginPage
    {
        public IWebDriver oWebDriver;
        public Hashtable oHashTable;
        public LoginPage(IWebDriver webDriver) 
        {
            oWebDriver = webDriver; 
        }
        
        public LoginPage userLogin()
        {
            try
            {
                oHashTable = ExcelUtil.poupulateHashFromExcel("Rentals_Scenario_One", "LoginPage");
                WebPageUtility.inputTextByXpath(oHashTable["User_Name"].ToString(), Env.strCurrentUserID);
                WebPageUtility.inputTextByXpath(oHashTable["Password"].ToString(), Env.strCurrentPassword);
                WebPageUtility.clickElementByXpath(oHashTable["Login_Button"].ToString());                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception :" + ex.Message);
            }
            return new LoginPage(oWebDriver);
        }
    }

}

