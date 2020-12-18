using System;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Media;

namespace RTerminal.Classes
{
    /// <summary>
    /// Логика взаимодействия для ForPrint.xaml
    /// </summary>
    public partial class ForPrint : Window
    {
        string nomerTurn;
        string language;
        public ForPrint(string nomerT,string languageValue, string services=null)
        {
            InitializeComponent();
            nomerTurn = nomerT;
            language = languageValue;
            date();
        }
        private void date()
        {
            DateTime dt = DateTime.Now;
            Data_txt.Text = dt.ToShortDateString().ToString() + "  " + dt.ToShortTimeString().ToString();
            Profesia_text.Text = language;
            Ochered_Number.Text = nomerTurn;
            //MessageBox.Show(nomerTurn);
        }
        public void Check_Print()
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                //if (printDialog.ShowDialog() == true)
                //{
                //    grd.LayoutTransform = new ScaleTransform(5, 5);
                //    MessageBox.Show("тест");
                //}
                grd.Measure(new System.Windows.Size(printDialog.PrintableAreaWidth, double.PositiveInfinity));

                grd.Arrange(new Rect(grd.DesiredSize));

                grd.UpdateLayout();

                printDialog.PrintTicket.PageMediaSize = new PageMediaSize(printDialog.PrintableAreaWidth, grd.ActualHeight);

                printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

                printDialog.PrintVisual(grd, "Canon MF3010");
            }
            catch { }

        }       
    }
}
