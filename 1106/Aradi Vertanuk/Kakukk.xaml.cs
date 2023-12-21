using System;
using System.Collections.Generic;
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

namespace Aradi_Vertanuk
{
    /// <summary>
    /// Interaction logic for Kakukk.xaml
    /// </summary>
    public partial class Kakukk : Page
    {
        string selected = "";
        string kakukk = "Vörösmarty Mihály";
        MainWindow Mw;

        public Kakukk(MainWindow mw)
        {
            Mw = mw;
            InitializeComponent();
            List<string> Aradiak = new()
            {
                "Vécsey Károly",
                "Török Ignác",
                "Schweidel József",
                "Poeltenberg Ernő",
                "Nagy-Sándor József",
                "Leiningen-Westerburg Károly",
                "Lázár Vilmos",
                "Láhner György",
                "Knézich Károly",
                "Kiss Ernő",
                "Dessewffy Arisztid",
                "Damjanich János",
                "Aulich Lajos",
                kakukk
            };
            Random rnd = new();
            foreach (string aradi in Aradiak.OrderBy(x => rnd.Next(x.Length)).ToList())
            {
                Border b = new()
                {
                    Child = new Label()
                    {
                        Content = aradi
                    }
                };
                wp.Children.Add(b);
            }
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            foreach (Border b in wp.Children.Cast<Border>())
            {
                Console.WriteLine(b);
                b.Background = Brushes.Transparent;
            }
            (sender as Border).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ddd");
            selected = ((sender as Border).Child as Label).Content.ToString();
            okbtn.Visibility = Visibility.Visible;
        }

        private void Ellenorzes(object sender, RoutedEventArgs e)
        {
            if (selected == kakukk)
            {
                MessageBox.Show("Gratulálok Eltaláltad!");
                NavigationService.Navigate(new ArcNev(Mw));
            }
            else MessageBox.Show("Sajnos nem talált, próbáld újra!");
        }
    }
}
