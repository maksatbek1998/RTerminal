using RTerminal.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RTerminal
{
   
    public partial class MainWindow : Window
    {
        #region переменные
        DataBase db;
        System.Data.DataTable dynamicButtonName;
        string language;
        bool forNazadButtonBool = true;
        Stack<int> forNazadButton = new Stack<int>();
        #endregion
        public MainWindow()
        {
            InitializeComponent();       
            LanguageButton();//создание кнопок языка
         
        }
        #region Создание кнопок языка
        private void LanguageButton() 
        {
            db = new DataBase();
            db.del += bd =>
            {
                dynamicButtonName = bd;
                if (bd.Rows.Count > 0)
                {
                    for (int ii = 0; ii < bd.Rows.Count; ii++)
                    {
                        Button button = new Button();
                        button.Style = (Style)this.TryFindResource("Language_Button1");
                        button.Content = bd.Rows[ii][1].ToString();
                        button.Click += new RoutedEventHandler(Button_Click_Language);
                        button.Tag = bd.Rows[ii][0].ToString();
                        //Dynamic_InCategry_Button
                        languageStackPanel.Children.Add(button);
                    }
                }
            };
            db.SoursData("SELECT * FROM `terminal`.`langs`");
        }
        #endregion

        #region Создание кнопок услуг

        private void Window_Loaded(object sender = null, RoutedEventArgs e=null)
        {
            forNazadButton = new Stack<int>();
            NazadButton.Visibility =Visibility.Collapsed;
            NazadButton.Tag = 0;
            Category_Panel.Children.Clear();
            PodCategoeyPanel.Children.Clear();
            db = new DataBase();
            db.del += bd =>
            {
                dynamicButtonName = bd;
                if (bd.Rows.Count > 0)
                {
                    
                    for (int ii = 0; ii < bd.Rows.Count; ii++)
                    {   
                        if (bd.Rows[ii][0].ToString()== bd.Rows[ii][2].ToString()) {
                            
                            Button button = new Button();
                            button.Style = (Style)this.TryFindResource("Dynamic_Categry_Button");
                            button.Content = bd.Rows[ii][3].ToString();
                            button.Click += new RoutedEventHandler(Button_Click);
                            button.Tag = bd.Rows[ii][0].ToString();
                            //Dynamic_InCategry_Button
                            Category_Panel.Children.Add(button);
                        }
                    }
                }
            };
            db.SoursData("SELECT t.id, t.name, t.parent_id,(SELECT h.name FROM `terminal`.services_langs AS h WHERE h.services_id=t.id and h.locale="+(language?? "(SELECT g.`value` FROM `terminal`.terminal_options AS g WHERE g.terminal_id=1 AND g.`key`='язык')) AS Perevod") + " FROM `terminal`.`services` AS t WHERE t.is_active=1");
        }
        #endregion

        #region Логика динамических кнопок для услуг
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            forNazadButton.Push(int.Parse((sender as Button).Tag.ToString()));
            NazadButton.Visibility = Visibility.Visible;
            NazadButton.Uid = (PodCategoeyPanel.Children.Count + 1).ToString();
            #region Если есть подкатегории
            //создание кнопок категории при входе в услугу
            if (forNazadButtonBool) { 
                Button buttCategory = new Button();
                buttCategory.Content = (sender as Button).Content;
                buttCategory.Style = (Style)this.TryFindResource("Pod_Category");
                buttCategory.Click += new RoutedEventHandler(Button_Click_Category);
                buttCategory.Tag = (sender as Button).Tag;
                buttCategory.Uid = (PodCategoeyPanel.Children.Count + 1).ToString();
                PodCategoeyPanel.Children.Add(buttCategory);
            }
            forNazadButtonBool = true;
            #endregion

            #region кнопки услуг подкатегории
            //создание кнопок подкатегории услуг
            bool lastButton = false;
            int dynamicStyleCheck = 0;
            string dynamicStyleUrl = "Dynamic_Categry_Button";
            Category_Panel.Children.Clear();
            for (int ii = 0; ii < dynamicButtonName.Rows.Count; ii++)
            {
                if ((sender as Button).Tag.ToString() == dynamicButtonName.Rows[ii][2].ToString() && dynamicButtonName.Rows[ii][0].ToString() != dynamicButtonName.Rows[ii][2].ToString())
                {
                    dynamicStyleCheck++;
                }
                if (dynamicStyleCheck > 3) 
                {
                    dynamicStyleUrl = "Dynamic_InCategry_Button";
                    break; 
                }
            }
            for (int ii = 0; ii < dynamicButtonName.Rows.Count; ii++)
            {
                
                if ((sender as Button).Tag.ToString() == dynamicButtonName.Rows[ii][2].ToString()&& dynamicButtonName.Rows[ii][0].ToString()!= dynamicButtonName.Rows[ii][2].ToString())
                {
                    lastButton = true;
                    Button button = new Button();
                    button.Style = (Style)this.TryFindResource(dynamicStyleUrl);
                    button.Content = dynamicButtonName.Rows[ii][3].ToString();
                    button.Click += new RoutedEventHandler(Button_Click);
                    button.Tag = dynamicButtonName.Rows[ii][0].ToString();
                    Category_Panel.Children.Add(button);
                }
                if (ii == dynamicButtonName.Rows.Count - 1 && lastButton) return;
            }
            //это условие выводит на главное меню если конец подкатегории, после условии идёт создание очереди
            if (!lastButton) Window_Loaded();
            #endregion

            #region база данных и принтер
            //принтер
            //метод из класса для базы данных управляет принтером и регистрирует очередь
            db = new DataBase();
            // в метод вводится id услуги(в базе данных) и язык на котором принтер должен работать
            // если языка нет, то она берет язык по умолчанию из базы данных 
            db.RegistrTurn((sender as Button).Tag.ToString(), language ?? db.DisplayReturnOne("SELECT t.value FROM `terminal`.`terminal_options` AS t WHERE t.terminal_id=1 AND t.`key`='язык'"));
            #endregion
        }
        #endregion

        #region Логика динамических кнопок для категорий
        private void Button_Click_Category(object sender, RoutedEventArgs e)
        {
            int c = int.Parse((sender as Button).Uid.ToString());
            
            for (; c-1  < PodCategoeyPanel.Children.Count;)
            {
                if (c-1  == PodCategoeyPanel.Children.Count)
                {
                    //MessageBox.Show(KategoryMenu.Children.Count.ToString() + " " + c.ToString());
                    break;
                }
                PodCategoeyPanel.Children.RemoveAt(PodCategoeyPanel.Children.Count - 1);
            }
            Button_Click(sender,e);
            
        }
        #endregion

        #region Логика смены языка динамических кнопок
        private void Button_Click_Language(object sender, RoutedEventArgs e)
        {
            language = "'"+(sender as Button).Content.ToString()+ "')";
            Window_Loaded();
        }
        #endregion
        private void Button_Click_Nazad(object sender, RoutedEventArgs e)
        {
            
            if (forNazadButton.Count > 1) 
            {
                forNazadButton.Pop();
                (sender as Button).Tag = forNazadButton.Pop();
                // (sender as Button).Uid = (int.Parse((sender as Button).Uid.ToString())-1).ToString();
                forNazadButtonBool = false;
                //forNazadButton.Pop();
                Button_Click_Category(sender, e);              
            }
            else 
            {
                Window_Loaded();
            }
        }
    }
}
