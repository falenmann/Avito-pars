using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Avito_car
{
    public class CarModel
    {
        public string Model { get; set; }
        public List<string> Generations { get; set; }
    }

    public class CarMake
    {
        public string Make { get; set; }
        public List<CarModel> Models { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> completedMakes = new List<string>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "manufacturer_*.json");
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var makeName = fileName.Replace("manufacturer_", "");
                completedMakes.Add(makeName);
            }

            IWebDriver driver = new ChromeDriver();
            
            try
            {
                driver.Navigate().GoToUrl("https://www.avito.ru/catalog/auto");
                Thread.Sleep(600);

                IWebElement openAll = driver.FindElement(By.CssSelector(".index-root-_5Yb_"));
                ReadOnlyCollection<IWebElement> openAll_2 = openAll.FindElements(By.CssSelector(".index-row-vfcF1"));
                IWebElement openAll_3 = openAll_2[openAll_2.Count - 1].FindElement(By.CssSelector(".styles-module-root-iSkj3" +
                                                                                                  ".styles-module-root_noVisited-qJP5D" +
                                                                                                  ".styles-module-root_preset_black-PbPLe"));
                openAll_3.Click();

                ReadOnlyCollection<IWebElement> openAll_4 = driver.FindElements(By.CssSelector(".index-root-_5Yb_"));
                IWebElement openAll_5 = openAll_4[1];
                
                Thread.Sleep(1500);
                ReadOnlyCollection<IWebElement> allMarks = openAll_5.FindElements(By.CssSelector(".index-row-vfcF1"));
                Thread.Sleep(500);
                
                Actions actions = new Actions(driver);

                int count = 0;
                foreach (var mark in allMarks)
                {
                    count++;
                    
                    /*IWebElement element = mark.FindElement(By.CssSelector("a[data-marker='rubricator/row/link'] span"));*/
                    IWebElement element = mark.FindElement(By.CssSelector("a[data-marker='rubricator/row/link']"));
                    string markName = element.Text;
                    
                    if (completedMakes.Contains(markName))
                    {
                        continue;
                    }

                    if (mark.Text == "Свернуть")
                    {
                        break;
                    }
                    
                    string currentWindowHandle = driver.CurrentWindowHandle;

                    actions.MoveToElement(element)
                        .KeyDown(Keys.Control)
                        .Click(element)
                        .KeyUp(Keys.Control)
                        .Perform();
                    Thread.Sleep(500);

                    
                        var tabs = driver.WindowHandles;
                        IList<string> windowHandles = driver.WindowHandles;


                        // Переключение на последнюю открытую вкладку
                        string newTabHandle = windowHandles[windowHandles.Count - 1];
                        driver.SwitchTo().Window(newTabHandle);
                                try
                                {
                                    string currentUrl = driver.Url;
                                    ReadOnlyCollection<IWebElement> models = driver.FindElements(By.CssSelector("a[data-marker='rubricator/row/link'] span"));
                                    
                                    //Поиск кнопки
                                    ReadOnlyCollection<IWebElement> models_1 = driver.FindElements(By.CssSelector("a[data-marker='rubricator/row/button']"));

                                    try
                                    {
                                        if (models_1[models_1.Count-2].Text == "Показать все")
                                        {
                                            models_1[models_1.Count-2].Click();
                                            IWebElement All_model = driver.FindElement(By.CssSelector("div[class='index-root-_5Yb_']"));
                                            models = All_model.FindElements(By.CssSelector("a[data-marker='rubricator/row/link'][class='styles-module-root-iSkj3 styles-module-root_noVisited-qJP5D styles-module-root_preset_black-PbPLe']"));

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        
                                    }
                                    
                                    
                                    List<CarModel> carModels = new List<CarModel>();
                                    
                                    foreach (var model in models)
                                    {
                                        string modelName = model.Text;
                                        currentWindowHandle = driver.CurrentWindowHandle;
                                        List<string> generationList = new List<string>();
                                        actions.MoveToElement(model)
                                            .KeyDown(Keys.Control)
                                            .Click(model)
                                            .KeyUp(Keys.Control)
                                            .Perform();
                                        Thread.Sleep(500);


                                        var tabs_2 = driver.WindowHandles;
                                        driver.SwitchTo().Window(tabs_2.Last());
                                        
                                        IWebElement generation_all = driver.FindElement(By.CssSelector(".styles-module-root-XfPi7" +
                                            ".styles-module-margin-top_none-yYTpc" +
                                            ".styles-module-margin-bottom_none-FnZvf" +
                                            ".styles-module-root_direction_vertical-zyzdU" +
                                            ".styles-module-root_fullWidth-ariEk"));
                                        
                                        ReadOnlyCollection<IWebElement> generation_all_1 = generation_all.FindElements(By.CssSelector(".styles-module-root-GKtmM" +
                                            ".styles-module-root-YczkZ" +
                                            ".styles-module-size_xxl-FZCAW" +
                                            ".styles-module-size_xxl-MNVR0" +
                                            ".stylesMarningNormal-module-root-S7NIr" +
                                            ".stylesMarningNormal-module-header-2xl-xKCLk"));
                                        
                                        ReadOnlyCollection<IWebElement> generation_all_2 = generation_all.FindElements(By.CssSelector(".styles-module-root-YczkZ" +
                                            ".styles-module-size_m-n6S6Y" +
                                            ".styles-module-size_m_dense-uc_Qi" +
                                            ".styles-module-size_m-StPd0" +
                                            ".styles-module-size_dense-TQdU6" +
                                            ".stylesMarningNormal-module-root-S7NIr" +
                                            ".stylesMarningNormal-module-paragraph-m-dense-po90I" +
                                            ".styles-module-root_top-p0_50" +
                                            ".styles-module-margin-top_0-Mk_hC"));

                                

                                        for (int i = 0; i < generation_all_2.Count; i++)
                                        {
                                            string generation_1 = generation_all_1[i].Text;
                                            string generation_2 = generation_all_2[i].Text;
                                            string generation = generation_1 + " " + generation_2;
                                            generationList.Add(generation);
                                        }


                                        
                                        carModels.Add(new CarModel
                                        {
                                            Model = modelName,
                                            Generations = generationList
                                        });
                                        driver.Close();
                                        driver.SwitchTo().Window(currentWindowHandle);
                                        /*string thirdTab = tabs_2[1];
                                        ((IJavaScriptExecutor)driver).ExecuteScript($"window.open('','_self').close();", tabs_2[1]);*/
                                    }
                                    CarMake carMake = new CarMake
                                    {
                                        Make = markName,
                                        Models = carModels
                                    };
                                    driver.Close();
                                    driver.SwitchTo().Window(tabs.First());
                                    
                                    // Сериализация текущей марки автомобилей в JSON строку
                                    string jsonString = JsonConvert.SerializeObject(carMake, Formatting.Indented);

                                    // Запись JSON строки в файл
                                    File.WriteAllText("manufacturer_" + markName + ".json", jsonString, Encoding.UTF8);

                                    Console.WriteLine($"Данные успешно сохранены в manufacturer_{markName}.json");
                                }
                                catch (NoSuchElementException e)
                                {
                                    Console.WriteLine("Element not found: " + e.Message);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: " + e.Message);
                                }
                            
                            //driver.Close();
                        
                        count = 0;
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
