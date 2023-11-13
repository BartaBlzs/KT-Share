using Microsoft.Win32;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        kodolo k;
        dekodolo dk;
        public MainWindow()
        {
            InitializeComponent();
            k = new();
            dk = new();
        }

        private void Code(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sf = new() 
            { 
                AddExtension = true,
                DefaultExt = ".txt",
                Filter = "text file|*.txt"
            };

            if (sf.ShowDialog() == true)
            {
                File.WriteAllText(sf.FileName, k.GetCodedString(tb.Text));
            }
            tb.Text = k.GetCodedString(tb.Text);
        }

        private void Decode(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("LC_decoded.txt", dk.GetDeCodedString(tb.Text));
            tb.Text = dk.GetDeCodedString(tb.Text);
        }
    }
}
