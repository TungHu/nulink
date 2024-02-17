using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Diagnostics;
using System.Net.Mail;
using OpenQA.Selenium.Edge;
using System.Windows.Media.Animation;
using OtpNet;
using System.Drawing.Imaging;
//using OpenQA.Selenium.DevTools.V113.Debugger;

namespace main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string baseDir;

        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var options = new ChromeOptions();

            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";
            string outputPath = baseDir + "output.txt";
            string reCaptchaPATH = baseDir + "recaptcha.crx";

            //string profilePATH = File.ReadAllText(baseDir + "profilePATH.txt");
            //string outputPath = baseDir + "output.txt";



            //options.AddArgument($"user-data-dir={@profilePATH}");
            //options.AddExtension(reCaptchaPATH);
            //options.AddArgument($"--load-extension={baseDir + "anticaptcha_plugin_v0.64"}");
            //options.AddArgument($"--load-extension={baseDir + "2captchaa"}");

            //options.AddArgument(@"user-data-dir=C:\Users\Surface\Desktop\fuel_chrome\profile");
            options.AddArgument(@"user-data-dir=C:\Users\Surface\AppData\Local\Google\Chrome\User Data\");
            options.AddArgument("no-sandbox");
            options.AddArguments("start-maximized");
            string[] dataa = System.IO.File.ReadAllLines(baseDir + "input_web3.txt");
            string password = dataa[0].Split(' ')[2];
            int index_startup = dataa.Length - 1;
            int wallet = int.Parse(dataa[0].Split(' ')[1]);
            int count_profiles = int.Parse(dataa[0].Split(' ')[0]);
            for (int index_profile = 1; index_profile <= count_profiles; index_profile++)
            {

                try
                {

                    options.AddArgument("profile-directory=Profile " + dataa[index_profile]);
                    ChromeDriver driver = new ChromeDriver(options);
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    string currentWindow = driver.CurrentWindowHandle;
                    //List<string> tabs_list = (driver.WindowHandles).ToList();
                    /*driver.Navigate().GoToUrl("chrome-extension://iiidjlhlgjbplmegghilmgidacnlpgec/options/options.html");
                    Thread.Sleep(500);x
                    driver.SwitchTo().Window(currentWindow);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[1]/table/tbody/tr[1]/td[2]/input"))).Clear();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[1]/table/tbody/tr[1]/td[2]/input"))).SendKeys("8289239f8f78e180f4956af0a0b29c97");
                    Thread.Sleep(500);

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("connect"))).Click();
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/form/div[1]/table/tbody/tr[4]/td[2]/select/option[2]"))).Click();

                    }
                    catch (Exception)
                    { }
                    Thread.Sleep(1000);


                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/form/div[1]/table/tbody/tr[4]/td[2]/select/option[2]"))).Click();
                    }
                    catch (Exception)
                    { }*/
                    try
                    {
                        bool loop1 = true;
                        while (loop1)
                        {
                            try
                            {
                                driver.Navigate().GoToUrl("chrome-extension://cpmkedoipcpimgecpmgpldfpohjplkpp/popup.html");
                                loop1 = false;
                            }
                            catch (Exception)
                            { }

                        }
                        driver.SwitchTo().Window(currentWindow);

                        loop1 = true;
                        while (loop1)
                        {
                            try
                            {
                                try
                                {
                                    Thread.Sleep(2000);
                                    driver.FindElement(By.XPath("/html/body/div/div[4]/div/div[2]/div[2]/div[2]/button")).Click();
                                    driver.FindElement(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div/div[1]/input")).SendKeys(password);

                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div/div[2]/button"))).Click();

                                    // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[3]/div/div[2]/div[2]/div[1]/button"))).Click();
                                }
                                catch (Exception)
                                { }
                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div[3]/input"))).SendKeys(password);
                                Thread.Sleep(200);

                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div[4]/button"))).Click();

                                wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div[2]/div/main/div/div[2]/div/div/div/div[1]")));
                                Thread.Sleep(200);
                                try
                                {
                                    Thread.Sleep(2000);
                                    driver.FindElement(By.XPath("/html/body/div/div[4]/div/div[2]/div[2]/div[2]/button")).Click();
                                    Thread.Sleep(200);
                                    driver.FindElement(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div/div[1]/input")).SendKeys(password);
                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/div/main/div/div[3]/div/div[2]/button"))).Click();

                                    // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[3]/div/div[2]/div[2]/div[1]/button"))).Click();
                                }
                                catch (Exception)
                                { }
                                for (int index_WlAcccount = 1; index_WlAcccount <= wallet; index_WlAcccount++)
                                {
                                    try
                                    {
                                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[2]/div/main/div/div[2]/div/div/div/div[1]/div[2]/div[1]/span/span[1]"))).Click();
                                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/div/main/div/div[2]/div/div/div/div/div/div[2]/div/div[" + index_WlAcccount.ToString() + "]/div[1]"))).Click();
                                        Thread.Sleep(200);


                                        for (int temp = index_startup; temp > count_profiles; temp--)

                                        {
                                            try
                                            {
                                                driver.Navigate().GoToUrl(dataa[temp]);
                                                wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[1]/section/div[1]/div[1]/div/div")));


                                                Thread.Sleep(500);
                                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div[2]/div/div[3]/div[2]/button"))).Click();
                                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[9]/div/div/div[2]/div/div[2]/div[1]/div[2]/div/div"))).Click();
                                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[9]/div/div/div[2]/div/div[2]/div[2]/button"))).Click();
                                                wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/div/div[2]/div/div[2]/div/div/div[1]")));
                                            }
                                            catch (Exception)
                                            { }
                                            Thread.Sleep(100);
                                        }
                                        driver.Navigate().GoToUrl("https://www.gate.ac/web3/startup/");
                                        /*Actions action = new Actions(driver);
                                        action.KeyDown(Keys.Control).SendKeys("t").Build().Perform();
                                        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("image.jpeg", ScreenshotImageFormat.Jpeg);

                                        Thread.Sleep(200);
                                        action.SendKeys(Keys.Control).SendKeys(Keys.Shift).SendKeys("p").Build().Perform();
                                        action.SendKeys("full size").SendKeys(Keys.Enter).Build().Perform();
                                        Thread.Sleep(3000);
                                        action.SendKeys(Keys.Control).SendKeys(Keys.Shift).SendKeys("i").Build().Perform();*/
                                        driver.Navigate().GoToUrl("chrome-extension://cpmkedoipcpimgecpmgpldfpohjplkpp/popup.html");

                                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[3]/div/div[2]/div[2]/div[1]/button"))).Click();

                                    }
                                    catch (Exception)
                                    { }

                                }


                                loop1 = false;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    driver.Navigate().Refresh();
                                }
                                catch (Exception)
                                { }
                            }
                        }
                    }
                    catch (Exception)
                    { }



                    /*Thread.Sleep(4000);
                    string otp = "";

                    loop = true;
                    while (loop)
                    {
                        try
                        {
                            edriver.Navigate().GoToUrl("https://mail.google.com/mail/u/0/#inbox/FMfcgzGtxKWBdpFcFwwWrZtKkxjcdWJJ");
                            ewait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[1]/div/div")));
                            Thread.Sleep(3000);

                            otp = ewait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div/table/tr/td/div[2]/div[2]/div/div[3]/div[" + (i+j).ToString() + "]/div/div/div/div/div[1]/div[2]/div[3]/div[3]/div/center/table/tbody/tr/td/table/tbody/tr[2]/td/table[1]/tbody/tr/td/table/tbody/tr/td/p[3]"))).Text;
                            loop = false;
                            break;


                        }
                        catch (Exception)
                        { }
                    }
                    
                    Thread.Sleep(2000);*/
                    // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div[1]/div/div/div/div[2]/div/ul[2]/li[2]/span[3]")));


                    //File.AppendAllText(outputPath, i.ToString() + "|" + man[i].Split('|')[1] + "|" + tfa + "\n");
                    Thread.Sleep(1000);
                    driver.Quit();
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }

            /*var options = new ChromeOptions();

            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";
            string outputPath = baseDir + "output.txt";

            options.AddArgument(@"user-data-dir=C:\Users\Surface\Desktop\fuel_chrome\mail\");
            options.AddArgument("profile-directory=Default");

            options.AddArguments("start-maximized");
            string[] dataa = System.IO.File.ReadAllLines(baseDir + "input.txt");



            ChromeDriver driver = new ChromeDriver(options);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(dataa[i]);
                        IWebElement WlAddr = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/div/div/div/div[2]/div/div/section[1]/div/div[7]/div[2]/div/div[2]/div/div/span[3]/div/div/a")));
                        File.AppendAllText(baseDir + "output.txt", i.ToString() + "|" + WlAddr.GetAttribute("href") + '\n');
                    }
                    catch (Exception)
                    { }
                }

            }
            catch (Exception)
            {

            }


            Thread.Sleep(2000);

            driver.Quit();*/
        }
    }
}
