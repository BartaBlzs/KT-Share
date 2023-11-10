using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Szamrendszerek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseConverter converter = new BaseConverter();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void convert(object sender, RoutedEventArgs e)
        {
            outputLbl.Foreground = Brushes.Black;
            try
            {
                var res =
                    converter.AnyConvert(inputTb.Text, Convert.ToInt32((cb0.SelectedItem as ComboBoxItem).Tag), Convert.ToInt32((cb1.SelectedItem as ComboBoxItem).Tag));
                outputLbl.Content = res;
                
                if (MessageBox.Show("Elmenti a váltásokat?", "Mentés", MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
                    File.AppendAllText("konzol_valtasok.txt", $"{DateTime.Now.ToLocalTime()}\n{inputTb.Text} ({(cb0.SelectedItem as ComboBoxItem).Tag}) => {res} ({(cb1.SelectedItem as ComboBoxItem).Tag})\n\n");
            }
            catch
            {
                outputLbl.Content = "Hibás számot adtál meg!";
                outputLbl.Foreground = Brushes.Crimson;
            }
        }
    }
}
