using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Aradi_Vertanuk
{
    /// <summary>
    /// Interaction logic for UtolsoMondat.xaml
    /// </summary>
    public partial class UtolsoMondat : Page
    {
        int pageIndex = 0;
        string[] lines;
        int pontszam = 0;
        bool disabled = false;
        public UtolsoMondat()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            lines = File.ReadAllLines("utolso_mondatok.txt", Encoding.GetEncoding("windows-1250")).OrderBy(x => Guid.NewGuid()).ToArray(); // A forrás file ANSI kódolásban lett megadva, ezért hogy az ékezeteket kiírja kell a windows-1250 code page.
            InitializeComponent();
            Render();
        }
        
        private void Render()
        {
            if (pageIndex != lines.Length)
            {
                (sp.Children[0] as TextBlock).Text = lines[pageIndex].Split(":#")[1];
                var rand = lines.OrderBy(x => Guid.NewGuid()).Take(4).ToArray();
                for (int i = 1; i < 5; i++)
                {
                    (sp.Children[i] as Button).Background = Brushes.LightGray;
                    (sp.Children[i] as Button).Content = rand[i - 1].Split(":#")[0];
                }
                if (!rand.Contains(lines[pageIndex]))
                    (sp.Children[new Random().Next(1, 5)] as Button).Content = lines[pageIndex].Split(":#")[0];
            }
            else
            {
                MessageBox.Show($"Elért pontszám: {pontszam}/{lines.Length} -> {Math.Floor((double)pontszam / lines.Length * 100)}%");
                Environment.Exit(0);
            }
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            if (!disabled)
            {
                disabled = true;
                var senderbtn = (sender as Button);
                if (senderbtn.Content.ToString() == lines[pageIndex].Split(":#")[0])
                {
                    senderbtn.Background = Brushes.Green;
                    pontszam++;
                }
                else senderbtn.Background = Brushes.Crimson;
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    pageIndex++;
                    Dispatcher.Invoke(() =>
                    {
                        Render();
                    });
                    disabled = false;
                });
            }
        }
    }
}
