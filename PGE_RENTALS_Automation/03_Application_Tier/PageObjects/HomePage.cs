using System.Text;

//using Scripting;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
//using WebDriver_Demo.Libraries;
//using WebDriver_Demo.Libraries.Utils;
using Rentals._02_Utility_Tier;
using Rentals._02_Utility_Tier;
using System.Resources;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Rentals._03_Application_Tier.PageObjects
{
    class HomePage
    {
        public static IWebDriver webDriver;

        public HomePage NewCustomerDetail()
        {
            //webDriver = new ChromeDriver();
            //GlbVar.webDriver = webDriver;
            WebPageUtility.clickElementByXpath("//ul[@class='menusubnav']//li/a[@href='addcustomerpage.php']");
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='name']", Env.strTestCustName);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='dob']", Env.strTestDob);
            WebPageUtility.inputTextByXpath("//tr//tr//textarea[@name='addr']", Env.strTestCustAdd);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='city']", Env.strTestCity);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='state']", Env.strTestState);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='pinno']", Env.strTestPin);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='telephoneno']", Env.strTestMobileNum);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='emailid']", Env.strTestEmail);
            WebPageUtility.inputTextByXpath("//table[@class='layout']//td//input[@name='password']", Env.strTestCustPassword);
            if (WebPageUtility.clickElementByXpath("//table[@class='layout']//td//input[@value='Submit']"))
            {
                Reporter.ReportEvent("Pass", "Clicked", "Exception");
            }

            else
            {
                Reporter.ReportEvent("Fail", "Did not click", "Exception");
            }
            
            ;
            return new HomePage();
        }

        
    }
}
