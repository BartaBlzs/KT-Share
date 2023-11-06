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

namespace Templomok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] imgSources;
        string[] data;
        int pageIndex = 0;
        public MainWindow()
        { 
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string curpath = Directory.GetCurrentDirectory();
            leftimg.Source = new BitmapImage(new Uri($@"{curpath}\bal.png"));
            rightimg.Source = new BitmapImage(new Uri($@"{curpath}\jobb.png"));
            imgSources = Directory.GetFiles($@"{curpath}\templomok");
            data = File.ReadAllLines("mt9k.txt");
            Render();
        }

        private void Render()
        {
            var tempi = pageIndex % imgSources.Length;
            var i = tempi < 0 ? tempi + imgSources.Length : tempi;
            img.Source = new BitmapImage(new Uri(imgSources[i]));
            lbl.Content = data[i];
        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            pageIndex--;
            Render();
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            pageIndex++;
            Render();
        }
    }
}
