using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace RTerminal
{
    /// <summary>
    /// Логика взаимодействия для Zagruzka.xaml
    /// </summary>
    public partial class Zagruzka : Window
    {
        public Zagruzka()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Time;
            timer.Start();
        }
        public void Time(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}
