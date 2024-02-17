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
//using AutoItX3Lib;
using System.Diagnostics;
using System.Net.Mail;
using OpenQA.Selenium.Edge;

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
        public bool popupConfirm(IWebDriver driver, WebDriverWait wait, List<string> tabs_list, string currentWindow, string xpath, string xpath1 = " ")
        {
            Thread.Sleep(4000);
            bool rs = false;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            try
            {
                foreach (string item in driver.WindowHandles)
                {

                    if (!tabs_list.Contains(item))
                    {
                        driver.SwitchTo().Window(item);
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath1))).Click();
                            Thread.Sleep(1000);
                        }
                        catch (Exception)
                        { }
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath))).Click();
                            rs = true;
                            break;
                        }
                        catch (Exception)
                        {
                            driver.Close();
                        }

                    }
                }

            }
            catch (Exception)
            { }

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.SwitchTo().Window(currentWindow);
            return rs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new ChromeOptions();
            var eoptions = new ChromeOptions();
            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";
            string profilePATH = File.ReadAllText(baseDir + "profilePATH.txt");
            string PWD = File.ReadAllText(baseDir + "Pwd.txt");
            string reCaptchaPATH = baseDir + "recaptcha.crx";
            string outputPath = baseDir + "output.txt";
            eoptions.AddArgument(@"user-data-dir=C:\Users\TUNGHU~1\AppData\Local\Temp\scoped_dir3104_2001862021\");
            eoptions.AddArgument("profile-directory=Default");

            ChromeDriver edriver = new ChromeDriver(eoptions);
            WebDriverWait ewait = new WebDriverWait(edriver, TimeSpan.FromSeconds(20));

            //options.AddArgument($"user-data-dir={@profilePATH}");
            options.AddArgument(@"user-data-dir=E:\tempChrome\chrome_capx");
            options.AddArguments("start-maximized");
            //int[] id = { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            int[] id = Array.ConvertAll(File.ReadAllLines(@"E:\id.txt"), int.Parse);
            string[] name = File.ReadAllLines(@"E:\name.txt");
            string[] mailAdr = File.ReadAllLines(@"E:\mail.txt");
            string[] adr = File.ReadAllLines(@"E:\street.txt");
            string[] city = File.ReadAllLines(@"E:\city.txt");
            string[] state = File.ReadAllLines(@"E:\state.txt");
            string[] zip = File.ReadAllLines(@"E:\zip.txt");
            int j = 2;
            for (int i = 3; i < mailAdr.Length; i++)
            {
                string points = "";

                try
                {
                    options.AddArgument("profile-directory=Profile " + (i).ToString());
                    //options.AddArgument("--start-maximized");
                    ChromeDriver driver = new ChromeDriver(options);
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    string currentWindow = driver.CurrentWindowHandle;
                    List<string> tabs_list = (driver.WindowHandles).ToList();
                    

                    try
                    {
                        bool loop = true;
                        while (loop)
                        {
                            try
                            {
                                driver.Navigate().GoToUrl("https://app.zoop.club/sign-up/ref09771992");
                                loop = false;
                            }
                            catch (Exception)
                            { }

                        }

                        string[] astring = name[i].Split(' ');
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-1"))).SendKeys(string.Join(" ", astring.Skip(1)));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-2"))).SendKeys(astring[0]);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-3"))).SendKeys(mailAdr[i]);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-4"))).SendKeys(PWD);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-5"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-yellow"))).Click();
                        Thread.Sleep(4000);
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div.Modal_modal__yEgVW.MuiModal-root.css-8ndowl > div.OnboardingModal_modal__5XdB\\+.MuiBox-root.css-0 > div.OnboardingModal_modalActions__fqbia.MuiBox-root.css-0 > button.MuiButtonBase-root.MuiButton-root.MuiButton-outlined.MuiButton-outlinedPrimary.MuiButton-sizeMedium.MuiButton-outlinedSizeMedium.MuiButton-root.MuiButton-outlined.MuiButton-outlinedPrimary.MuiButton-sizeMedium.MuiButton-outlinedSizeMedium.css-1x7vq5q"))).Click();
                        }
                        catch (Exception)
                        {}
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-sizeSmall"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div/form/div[2]/button"))).Click();
                        // lay ma mail o edge
                        Thread.Sleep(2000);
                        loop = true;
                        string otp = "";
                        while (loop)
                        {
                            try
                            {
                                edriver.Navigate().GoToUrl("https://mail.google.com/mail/u/0/#inbox/FMfcgzGtwMXqCvGCjQKbcbZlFTwlqlKq");
                                ewait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[1]/div/div")));
                                Thread.Sleep(3000);
                                try
                                {
                                    otp = edriver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div[2]/div/table/tr/td/div[2]/div[2]/div/div[3]/div/div/div/div/div/div[1]/div[2]/div[3]/div[3]/div/div[1]/table/tbody/tr/td/div/div/div/div/div/div/table[2]/tbody/tr/td")).Text;
                                    loop = false;
                                    break;
                                }
                                catch (Exception)
                                {}
                               
                                otp = ewait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div/table/tr/td/div[2]/div[2]/div/div[3]/div[" + (i+j).ToString() + "]/div/div/div/div/div[1]/div[2]/div[3]/div[3]/div/div[1]/table/tbody/tr/td/div/div/div/div/div/div/table/tbody/tr/td"))).Text;
                                loop = false;
                            }
                            catch (Exception)
                            { }
                        }


                        driver.FindElement(By.Name("verificationCode")).SendKeys(otp);

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div/form/div[2]/button"))).Click();
                        Thread.Sleep(5000);
                        driver.Navigate().GoToUrl("https://app.zoop.club/profile");
                        Thread.Sleep(500);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".Profile_walletBtn__9v07r"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-contained"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButtonBase-root:nth-child(2)"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("countries-input"))).SendKeys("vietnam");
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("countries-input-option-0"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-root"))).Click();

                        Random rnd = new Random();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div/form/div[3]/div/div/div/div/input"))).SendKeys((rnd.Next() % 12 + 1).ToString());
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div/form/div[3]/div/div/div[2]/div/input"))).SendKeys((rnd.Next() % 27 + 1).ToString());
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div/form/div[3]/div/div/div[3]/div/input"))).SendKeys((rnd.Next() % 5 + 1999).ToString());
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mui-6"))).Click();

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div/form/div[1]/div/div/div/div[1]/div/input"))).SendKeys(adr[i].Replace(".", "").Replace("/", "").Replace("(", "").Replace(")", ""));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("city"))).SendKeys(city[i].Replace(".", "").Replace("/", "").Replace("(", "").Replace(")", ""));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("county"))).SendKeys(state[i].Replace(".", "").Replace("/", "").Replace("(", "").Replace(")", ""));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("zipPostcode"))).SendKeys(zip[i]);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div/form/div[7]/button"))).Click();
                        Thread.Sleep(3000);
                        driver.Navigate().GoToUrl("https://app.zoop.club/profile");
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[2]/div/div[1]/div[1]/div[2]/div[3]/a/div/div/span[2]")));
                        points = driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/div[1]/div[1]/div[2]/div[3]/a/div/div/span[2]")).Text;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(5000);
                    }


                    Thread.Sleep(1000);



                    if (points == "")
                    {
                        j--;
                    }
                    File.AppendAllText(outputPath, points + "|" + i.ToString() + "|" + mailAdr[i] + '\n');


                    driver.Quit();
                }
                catch (Exception)
                {
                    i--;

                }
                
            }
        }
    }
}
