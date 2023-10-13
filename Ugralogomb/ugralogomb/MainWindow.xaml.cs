using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ugralogomb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int[] fontSizes = { 13, 25, 40 };
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            //hogy legyen esely megnyomni a gombot ezert egy 40 ms-es szunetet rakok bele
            var t = new Thread(() =>
            {
                Thread.Sleep(40);
                Dispatcher.Invoke(() =>
                {
                    Random rnd = new();
                    var w = rnd.Next(50, 400);
                    var h = rnd.Next(50, 400);
                    btn.Width = w;
                    btn.Height = h;

                    btn.Margin = new Thickness(rnd.Next(0, (int)(ActualWidth - w)), rnd.Next(0, (int)(ActualHeight - h)), 0, 0);
                    btn.FontSize = fontSizes[rnd.Next(fontSizes.Length)];
                });
            });
            t.Start();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gratulalok, sikerult megnyomnod a gombot!");
            Application.Current.Shutdown();
        }
    }
}
