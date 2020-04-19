using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Rentals._01_Configuration_Tier.EnvironmentFiles;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using RelevantCodes.ExtentReports;

namespace Rentals._02_Utility_Tier
{
    // class name: Reporter (included for declaring report variable and Extent report methods )
    // created: March 21, 2016 03:00 PM
    // Author: Sanketh
    // Revisions:

    public class Reporter
    {

        public static string strExecSummaryHTMLFilePath;
        public static string strTestResHTMLFilePath;
        public static string strCurrentApplication;
        public static string strCurrentEnvironment;
        public static string strOnError;


        public static string strCurrentUserID;
        public static string strCurrentPassword;
        public static string strCurrentURL;
        public static string strCurrentBrowser;

        public static string strCurrentModule;
        public static string strCurrentScenarioID;
        public static string strCurrentScenarioDesc;

        public static string strCurrentTestID;
        public static string strCurrentTestIterationList;
        public static string strCurrentTestDesc;
        public static string strTCStatus;

        public static int intScreenshotCount = 0;
        public static int intCurrentIteration = 1;
        public static string strCurrentBusFlowKeyword = "";
        public static int intStepNumber = 1;
        public static int intPassStepCount = 0;
        public static int intFailStepCount = 0;
        public static int intPassTCCount = 0;
        public static int intFailTCCount = 0;




        //Extent
        private static ExtentReports o_ExtentReports;
        private static ExtentTest Test;

        //Constructor
        public Reporter()
        {
            //setRelativePath();
            if (!Env.boolDebugFlag) setTestResultFoders();
        }



        // method name: createReporterInstance (included for creating Reporter Instance along folder structure i.e windows Userid -> HTML, SCREENSHOTS )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:

        public static ExtentReports createReporterInstance()
        {
            try
            {
                if (string.IsNullOrEmpty(Env.strTestResHTMLFilePath) == true)
                {
                    DirectoryInfo oResDirInfo = new DirectoryInfo(Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.ReportsFolder + Env.sysFileSeperator + Env.UserName);
                    if (!oResDirInfo.Exists)
                    {
                        oResDirInfo.Create();
                        Env.strTestRunResultPath = Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.ReportsFolder + Env.sysFileSeperator + Env.UserName + Env.sysFileSeperator;
                        Directory.CreateDirectory(Env.strTestRunResultPath + Env.HTML + Env.sysFileSeperator);
                        Directory.CreateDirectory(Env.strTestRunResultPath + Env.SCREENSHOTS + Env.sysFileSeperator);
                    }
                    else
                    {
                        DirectoryInfo oResBackUpDirInfo = new DirectoryInfo(Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.ReportsFolder + Env.sysFileSeperator + Env.UserName + Env.sysFileSeperator + Env.Backup);

                        if (!oResBackUpDirInfo.Exists)
                            oResBackUpDirInfo.Create();

                        //get all the files from a directory
                        DirectoryInfo[] oResSubDirList = oResDirInfo.GetDirectories();
                        string strRunFolderName = "Run_" + getTimeStamp(true);
                        string strTempFolderName = Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.ReportsFolder + Env.sysFileSeperator + Env.UserName + Env.sysFileSeperator + Env.Backup + Env.sysFileSeperator + strRunFolderName;
                        Directory.CreateDirectory(strTempFolderName);
                        foreach (DirectoryInfo oSubDir in oResSubDirList)
                        {
                            if (oSubDir.Name.ToUpper().IndexOf("BACKUP") < 0)
                            {
                                DirectoryInfo oBackUpTargetDir = new DirectoryInfo(strTempFolderName + Env.sysFileSeperator + oSubDir.Name);
                                MoveDirectory(oSubDir.FullName, oBackUpTargetDir.FullName);
                            }
                        }
                        Env.strTestRunResultPath = Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.ReportsFolder + Env.sysFileSeperator + Env.UserName + Env.sysFileSeperator;
                        Directory.CreateDirectory(Env.strTestRunResultPath + Env.HTML + Env.sysFileSeperator);
                        Directory.CreateDirectory(Env.strTestRunResultPath + Env.SCREENSHOTS + Env.sysFileSeperator);
                    }
                    Env.strTestResHTMLFilePath = Env.strTestRunResultPath + Env.HTML + Env.sysFileSeperator +
                    strCurrentApplication + "_" + strCurrentEnvironment + "_" + strCurrentModule + "_" + getTimeStamp(true) + Env.HtmlExtention;
                }

                o_ExtentReports = new ExtentReports(Env.strTestResHTMLFilePath, false);
                o_ExtentReports.AddSystemInfo(Env.CONTEXT, Env.strContext);
                o_ExtentReports.AddSystemInfo(Env.URL, Reporter.strCurrentURL);
                o_ExtentReports.AddSystemInfo(Env.TestDeveloperName, Env.strDeveloperName);
                o_ExtentReports.AddSystemInfo(Env.Browser, Env.strBrowser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return o_ExtentReports;
        }

        // method name: startTest (included for start a Test )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:

        public static void startTest(string TestName, string TestDescription)
        {
            if (!Env.boolDebugFlag) Test = o_ExtentReports.StartTest(TestName, TestDescription);

        }

        // method name: AssignCategory (included for Assign a Category )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:

        public static void AssignCategory(string TestName, string TestDescription)
        {
            if (!Env.boolDebugFlag) Test.AssignCategory(TestName, TestDescription);

        }

        // method name: EndTest (included for End Test )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:

        public static void EndTest()
        {
            if (!Env.boolDebugFlag) o_ExtentReports.EndTest(Test);

        }

        // method name: Flush (included for Flush a Test )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:  

        public static void Flush()
        {
            if (!Env.boolDebugFlag) o_ExtentReports.Flush();

        }

        // method name: setTestResultFoders (included for set Test Result Folders for backup , screenshot and html link )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions:  

        public static void setTestResultFoders()
        {

            DirectoryInfo oResDirInfo = new DirectoryInfo(Env.strRelativePath + Env.Results_Tier);
            DirectoryInfo oResBackUpDirInfo = new DirectoryInfo(Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + Env.Backup);

            if (!oResBackUpDirInfo.Exists)
                oResBackUpDirInfo.Create();

            //get all the files from a directory
            DirectoryInfo[] oResSubDirList = oResDirInfo.GetDirectories();
            foreach (DirectoryInfo oSubDir in oResSubDirList)
            {
                if (oSubDir.Name.ToUpper().IndexOf("BACKUP") < 0)
                {
                    DirectoryInfo oBackUpTargetDir = new DirectoryInfo(oResDirInfo.FullName +
                                Env.sysFileSeperator + Env.Backup + Env.sysFileSeperator + oSubDir.Name);

                    MoveDirectory(oSubDir.FullName, oBackUpTargetDir.FullName);
                }
            }
            string strRunFolderName = "Run_" + getTimeStamp(true);
            Env.strTestRunResultPath = Env.strRelativePath + Env.Results_Tier + Env.sysFileSeperator + strRunFolderName + Env.sysFileSeperator;
            Directory.CreateDirectory(Env.strTestRunResultPath);
            Directory.CreateDirectory(Env.strTestRunResultPath + Env.HTML + Env.sysFileSeperator);
            //	Directory.CreateDirectory(GlbVar.strTestRunResultPath + "TEXT" + GlbVar.sysFileSeperator);
            Directory.CreateDirectory(Env.strTestRunResultPath + Env.SCREENSHOTS + Env.sysFileSeperator);

        }

        // method name: MoveDirectory (included for Move Directory Foldersfrom source to destination )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions: 

        public static void MoveDirectory(string strSourceDirectory, string strDestinationDirectory)
        {
            try
            {
                Directory.Move(strSourceDirectory, strDestinationDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // method name: getTimeStamp (included to get TimeS tamp for folder creation )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions: 

        public static string getTimeStamp(bool boolForFolderCreation)
        {

            if (boolForFolderCreation)
            {
                return DateTime.Now.ToString("dd-MMM-yyyy_hh-mm-ss_tt");
            }
            else
            {
                return DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }
        }

        // method name: ReportEvent (included to craete test result and exception log )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions: 

        public static void ReportEvent(string staus, string strStepName, string strException)
        {

            if (!Env.boolDebugFlag)
            {
                Console.WriteLine(staus + strStepName + strException);//new


                if (staus.ToLower() == Env.Pass) Test.Log(LogStatus.Pass, strStepName, strException);
                if (staus.ToLower() == Env.Info) Test.Log(LogStatus.Info, strStepName, strException);
                if (staus.ToLower() == Env.Warning) Test.Log(LogStatus.Warning, strStepName, strException);
                if (staus.ToLower() == Env.Fail)
                {

                    string strScreeshotPath = Env.strTestRunResultPath + Env.SCREENSHOTS + Env.sysFileSeperator + strCurrentApplication + strCurrentModule + "-" +
                                                                     strCurrentScenarioID + strCurrentTestID + "-" + intScreenshotCount + Env.ScreenShotExtention;
                    CaptureScreenShot(strScreeshotPath);

                    Console.WriteLine(staus + strStepName + strException + strScreeshotPath);
                    Test.Log(LogStatus.Fail, strStepName, strException);
                    Test.Log(LogStatus.Fail, strStepName, strScreeshotPath);
                    Test.Log(LogStatus.Info, strStepName, Env.Snapshot_Below + Test.AddScreenCapture(strScreeshotPath));
                }
            }
        }

        // method name: CaptureScreenShot (included to Capture Screen Shot )
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions: 

        private static void CaptureScreenShot(string strScreenshotPath)
        {
            try
            {
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap as Image);
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(strScreenshotPath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // method name: GetEnvConfigDetails (included to Get Environmental Configuration Details)
        // created: March 21, 2016 03:00 PM
        // Author: Sanketh
        // Revisions: 

        public static void GetEnvConfigDetails()
        {
            Reporter.strCurrentApplication = "";
            Reporter.strCurrentEnvironment = "";
            Reporter.strCurrentURL = "";
            Reporter.strCurrentUserID = "";
            Reporter.strCurrentPassword = "";
            Reporter.strCurrentBrowser = "";
            DataTable oTable = ExcelUtil.ExcelToTable(Env.strRelativePath + Env.Configuration_Tier + Env.sysFileSeperator + Env.EnvironmentFiles + Env.sysFileSeperator + Env.Configuration_xlsx, Env.Env_Config);

            DataRow[] oDataRows = oTable.Select("Execution_Flag = 'Y'");
            if (oDataRows.Length < 1)
                Assert.Inconclusive("Execution Aborted! Please Set Execution flag in the Environment Config File");

            DataRow oDataRow = oDataRows[0];
            Reporter.strCurrentApplication = oDataRow["Application_Name"].ToString();
            Reporter.strCurrentEnvironment = oDataRow["Environment"].ToString();
            Reporter.strCurrentURL = oDataRow["Environment_URL"].ToString();
            Reporter.strCurrentUserID = oDataRow["UserName"].ToString();
            Reporter.strCurrentPassword = oDataRow["Password"].ToString();
            Reporter.strCurrentBrowser = oDataRow["Browser"].ToString();

            Env.strLoginFlag = oDataRow["LoginFlag"].ToString();

            Env.strCurrentBrowser = Reporter.strCurrentBrowser;
            Env.strCurrentURL = Reporter.strCurrentURL;
            Env.strCurrentUserID = Reporter.strCurrentUserID;
            Env.strCurrentPassword = Reporter.strCurrentPassword;
        }



    }

}