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
using AutoItX3Lib;
using System.Diagnostics;

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
            {} 
            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.SwitchTo().Window(currentWindow);
            return rs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new ChromeOptions();
            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";  
            string profilePATH = File.ReadAllText(baseDir + "profilePATH.txt");
            string PWD = File.ReadAllText(baseDir + "Pwd.txt");
            string reCaptchaPATH = baseDir + "recaptcha.crx";


            options.AddArgument("--start-maximized");

            options.AddExtension(reCaptchaPATH);
            options.AddArgument($"--load-extension={baseDir + "anticaptcha_plugin_v0.64"}");
            options.AddArgument($"user-data-dir={@profilePATH}");
            //int[] id = { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            //int[] id = Array.ConvertAll(File.ReadAllLines(baseDir + "profileID.txt"), int.Parse);
            for (int i = 294; i < 301; i++)
            {
                options.AddArgument("profile-directory=Profile " + i.ToString());
                //options.AddArgument("--start-maximized");
                ChromeDriver driver = new ChromeDriver(options);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                string currentWindow = driver.CurrentWindowHandle;
                List<string> tabs_list = (driver.WindowHandles).ToList();
                bool loop;
                string keys = "";
                // cai extension
                try
                {
                    driver.Navigate().GoToUrl("https://chrome.google.com/webstore/detail/fuel-wallet/dldjpboieedgcmpkchcjcbijingjcgok");

                    tabs_list = (driver.WindowHandles).ToList();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("body > div.F-ia-k.S-ph.S-pb-qa > div.h-F-f-k.F-f-k > div > div > div.e-f-o > div.h-e-f-Ra-c.e-f-oh-Md-zb-k > div > div > div > div"))).Click();
                    Thread.Sleep(3000);

                    AutoItX3 auto = new AutoItX3();
                    auto.Send("{LEFT}");
                    Thread.Sleep(300);
                    auto.Send("{Enter}");


                }
                catch (Exception)
                {
                    Thread.Sleep(5000);
                }
                while (driver.WindowHandles.Count() == tabs_list.Count)
                {
                    Thread.Sleep(500);
                    //Console.WriteLine(driver.WindowHandles.Count() + " " + tabs);
                }
                Thread.Sleep(2000);
                try
                {
                    loop = true;
                    while (loop)
                    {
                        driver.SwitchTo().Window(currentWindow);
                        driver.Navigate().GoToUrl("chrome-extension://dldjpboieedgcmpkchcjcbijingjcgok/index.html#/sign-up/welcome");
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/main/div/div/div[3]/article[1]"))).Click();
                            loop = false;
                        }
                        catch (Exception)
                        { }
                    }
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"agreeTerms\"]"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Next: Seed Phrase']"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/main/div/div/div[2]/div[2]/div[1]/footer/button"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("confirmSaved"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Next: Confirm phrase']"))).Click();
                    keys = Clipboard.GetText(TextDataFormat.Text);
                    for (int k = 1; k <= 12; k++)
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/main/div/div/div[3]/div[1]/div/div/div[" + k.ToString() + "]/div/input"))).SendKeys(keys.Split(' ')[k - 1]);
                    }
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Next: Your password']"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#\\:r2\\:"))).SendKeys(PWD);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#\\:r5\\:"))).SendKeys(PWD);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Next: Finish set-up']"))).Click();


                }
                catch (Exception)
                {}
                
                loop = true;
                while (loop)
                {
                    try
                    {
                        loop = (wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/main/div/div/div[2]/h2"))).Text != "Wallet created successfully");
                    }
                    catch (Exception)
                    {}
                }
                

                loop = true;
                while (loop) {
                    driver.Navigate().GoToUrl("https://wallet.fuel.network/docs/dev/connectors/");
                    Thread.Sleep(600);
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".fuel_Box-flex-ieGAAbW-css > .fuel_Button:nth-child(1)"))).Click();
                        loop = !popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]", "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]");
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    { }
                }


                //SwaySwap


                loop = true;
                while (loop)
                {
                    driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/welcome/connect");
                    try
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Connect Wallet']"))).Click();
                            if (!popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]", "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]"))
                            { continue; }
                            loop = false;

                        }
                        catch (Exception)
                        { }
                    }
                    catch (Exception)
                    { }
                }

                loop = true;
                while (loop)
                {
                    try
                    {
                        try
                        { //captcha chua toi uw
                            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@title='reCAPTCHA']")));
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("recaptcha-anchor"))).Click();
                            driver.SwitchTo().DefaultContent();
                            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@title='recaptcha challenge expires in two minutes']")));

                            var divElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#rc-imageselect > div.rc-footer > div.rc-controls > div.primary-controls > div.rc-buttons > div.button-holder.help-button-holder")));
                            divElement.SendKeys(Keys.Enter);

                        }
                        catch (Exception)
                        { }
                        driver.SwitchTo().DefaultContent();
                        wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@title='reCAPTCHA']")));
                        

                        while (driver.FindElement(By.Id("recaptcha-anchor")).GetAttribute("aria-checked") == "false")
                        {
                            driver.SwitchTo().DefaultContent();
                            try
                            {
                                IWebElement element = driver.FindElement(By.XPath("//button[text()='Give me ETH']"));
                                if (element.Enabled && element.Displayed)
                                {
                                    break;
                                }
                                wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@title='reCAPTCHA']")));
                                Thread.Sleep(2000);
                            }
                            catch (Exception)
                            {}
                        }

                        driver.SwitchTo().DefaultContent();
                        Thread.Sleep(2000);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Give me ETH']"))).SendKeys(Keys.Enter);
                        loop = false;
                    }
                    catch (Exception)
                    {
                        driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/welcome/faucet");
                        continue;
                    }
                    driver.SwitchTo().DefaultContent();
                }

                loop = true;
                while (loop)
                {
                    try
                    {
                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[2]/div/section/div/div/button"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Add Assets']"))).Click();

                        if (!popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]"))
                        {
                            driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/welcome/add-assets");
                            continue;                            
                        }
                        loop = false;

                        Thread.Sleep(500);
                    }
                    catch (Exception)
                    { }

                }
                loop = true;
                while (loop)
                {
                    try
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Mint assets']"))).Click();
                            if (!popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]"))
                            {
                                driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/welcome/mint");
                                continue;
                            }
                        }
                        catch (Exception)
                        {}
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"accept-agreement\"]"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Get Swapping!']"))).Click();
                        loop = false;

                    }
                    catch (Exception)
                    { }

                }
                
                


                //trang swap
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//input[@value=\'\'])[2]"))).Click();
                //
                //https://fuellabs.github.io/swayswap/swap?from=DAI&to=sETH

                string[] textt = { "Balance: 500.000", "Balance: 600.000" };
                string[] swapLnk = { "https://fuellabs.github.io/swayswap/swap?from=sETH&to=DAI", "https://fuellabs.github.io/swayswap/swap?from=DAI&to=sETH" };
                for (int k = 0; k < 2; k++)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(swapLnk[k]);
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("(//input[@value=''])[" + (2 - k).ToString() + "]"))).SendKeys((100 - 50 * k).ToString());
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[2]/main/div[3]/div/div[3]/button"))).Click();
                        if (!popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]"))
                        {
                            k--;
                            continue;
                        }
                        while (wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div[2]/main/div[3]/div/div[3]/div[" + (3 - k * 2).ToString() + "]/div/div[2]/div"))).Text == textt[k])
                        {
                            Thread.Sleep(4000);
                            driver.Navigate().Refresh();
                        }
                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[2]/main/div[3]/div/div[3]/div[2]/button"))).Click();
                        Thread.Sleep(500);
                    }
                    catch (Exception)
                    {
                        k--;
                        driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/swap?from=sETH&to=DAI");
                    }                       
                }

                /*wait.Until(ExpectedConditions.ElementExists(By.XPath("(//input[@value=''])[1]"))).SendKeys("50");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[2]/main/div[3]/div/div[3]/button"))).Click();
                popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]");
                Thread.Sleep(2000);
                while (wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div[2]/main/div[3]/div/div[3]/div[1]/div/div[2]/div"))).Text == "Balance: 600.000")
                {}*/

                loop = true;
                while (loop)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("https://fuellabs.github.io/swayswap/pool/add-liquidity");
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value=\'\']")));
                        Thread.Sleep(2500);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value=\'\']"))).Click();
                        driver.FindElement(By.CssSelector(".coinInput:nth-child(1) .coinInput--input")).SendKeys("0.1");
                        //driver.FindElement(By.CssSelector(".coinInput:nth-child(2) .coinInput--input")).SendKeys("100");

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[aria-label='Add liquidity'][type='button'].button.w-full.justify-center.button--lg.button--primary[aria-disabled='false'][aria-busy='false']"))).Click();
                        loop = !popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]");
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(text(), 'Remove liquidity')]")));
                    }
                    catch (Exception)
                    {}
                }
                

                Thread.Sleep(1000);
                //Swaylend
                loop = true;
                while (loop)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("https://app.swaylend.com/#/dashboard");
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div/header/div[3]/div/button"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[3]/div/div[2]/div/div[2]/div/div/div[6]/p"))).Click();
                        loop = !popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]", "//*[@id=\"root\"]/main/div/div[2]/div[3]/button[2]");
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {}
                }
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div/header/div/div/div[3]"))).Click();
                driver.Navigate().GoToUrl("https://app.swaylend.com/#/faucet");
                for (int k = 2; k < 8; k++)
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='root']/div/div[2]/div/span/div/div/table/tbody/tr[" + k.ToString() + "]/td[4]/button"))).Click();
                        popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id='root']/main/div/div[2]/div[2]/button[2]");
                        Thread.Sleep(1000);
                        popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id='root']/main/div/div[2]/div[2]/button[2]");
                        Thread.Sleep(1000);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='root']/div/div[2]/div/span/div/div/table/tbody/tr[1]/td[4]/button")));

                    }
                    catch (Exception)
                    {
                        k--;
                        driver.Navigate().Refresh();
                    }
                }

                driver.Navigate().GoToUrl("https://app.swaylend.com/#/dashboard");


                string balance = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[1]/div[1]/p[2]"))).Text;

                
                string[] values = { "4", "0.3", "4", "0.005" };

                for (int k = 0; k < values.Length; k++)
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div[3]/div/div/div[" + (k + 2).ToString() + "]/div[3]/div[2]"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-8qy25l"))).Click();
                        driver.FindElement(By.CssSelector(".css-8qy25l")).Clear();
                        Thread.Sleep(200);
                        driver.FindElement(By.CssSelector(".css-8qy25l")).SendKeys(values[k]);
                        //driver.FindElement(By.XPath("//*[@id=\"react-collapsed-panel-21\"]/div[1]/div[2]/div[1]/div")).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[3]/div[2]/div[3]/div[2]/div[1]/div[4]/button[2]"))).Click();
                        popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]");
                        while (balance == wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[1]/div[1]/p[2]"))).Text)
                        { }

                        balance = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[1]/div[1]/p[2]"))).Text;
                        Thread.Sleep(500);
                    }
                    catch (Exception)
                    {
                        k--;
                        driver.Navigate().Refresh();
                        Thread.Sleep(2000);

                    }
                }
                try
                {
                    driver.Navigate().Refresh();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#react-collapsed-panel-5 > div > button.css-1nclyxy"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-8qy25l"))).Click();
                    driver.FindElement(By.CssSelector(".css-8qy25l")).SendKeys("0");
                    Thread.Sleep(200);
                    driver.FindElement(By.CssSelector(".css-8qy25l")).SendKeys("200");
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[3]/div[2]/div[3]/div[2]/div[1]/div[4]/button[2]"))).Click();
                    popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]");
                    while (balance == wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[1]/div[1]/p[2]"))).Text)
                    { }

                    balance = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[1]/div[1]/p[2]"))).Text;
                    Thread.Sleep(500);

                }
                catch (Exception)
                { }
                try
                {
                    driver.Navigate().Refresh();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id=\'root\']/div/div[2]/div/div[3]/div[2]/div/div[2]"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[1]/div[2]/div/div[3]/div[2]/div[3]/div[1]/div/button[1]"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".css-8qy25l"))).Click();
                    //driver.FindElement(By.CssSelector(".css-8qy25l")).SendKeys(Int32.Parse(driver.FindElement(By.CssSelector(".css-1emt5dt")).Text.Split(' ')[0]) >= 100? "100" : driver.FindElement(By.CssSelector(".css-1emt5dt")).Text.Split(' ')[0]);
                    string temp = driver.FindElement(By.CssSelector(".css-1emt5dt")).Text.Split(' ')[0];
                    float intt = float.Parse(temp);
                    driver.FindElement(By.CssSelector(".css-8qy25l")).SendKeys((float.Parse(temp) >= 100) ? "100" : temp);

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Borrow']"))).Click();
                    popupConfirm(driver, wait, tabs_list, currentWindow, "//*[@id=\"root\"]/main/div/div[2]/div[2]/button[2]");


                }
                catch (Exception)
                {}

                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    while (wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div[1]/div[2]/p[2]"))).Text == "$0.00")
                    {
                        if (stopwatch.ElapsedMilliseconds >= 10000) // Kiểm tra nếu đã trôi qua 3 giây
                        {
                            stopwatch.Stop();
                            break; // Kết thúc vòng lặp
                        }
                    } 
                    {}   
                }
                catch (Exception)
                {}

                Thread.Sleep(3000);


                //https://discord.com/invite/fuelnetwork

                string outputPath = baseDir + "output.txt";

                File.AppendAllText(outputPath, keys + "|" + i.ToString() + "|"+ balance + '\n');
                

                driver.Quit();
            }
        }
    }
}
