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
	/// Interaction logic for ArcNev.xaml
	/// </summary>
	public partial class ArcNev : Page
	{
		int pageIndex = 0;
		string[] files;
		int pontszam = 0;
		bool disabled = false;
		MainWindow Mw;
		public ArcNev(MainWindow mw)
		{
			Mw = mw;
            Mw.Width = 600;
            Mw.Height = 700;
            
            InitializeComponent();

			files = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\kepek").OrderBy(x => Guid.NewGuid()).ToArray();
			Render();
		}

        private void Render()
		{

			if (pageIndex != files.Length)
			{
				(sp.Children[0] as Image).Source = new BitmapImage(new Uri(files[pageIndex]));
				var rand = files.OrderBy(x => Guid.NewGuid()).Take(4).ToArray();
				for (int i = 1; i < 5; i++)
				{
					(sp.Children[i] as Button).Background = Brushes.LightGray;
					(sp.Children[i] as Button).Content = GetNameFromPath(rand[i - 1]);
				}
				if (!rand.Contains(files[pageIndex]))
					(sp.Children[new Random().Next(1, 5)] as Button).Content = GetNameFromPath(files[pageIndex]);
			}
			else
			{
                MessageBox.Show($"Elért pontszám: {pontszam}/{files.Length} -> {Math.Floor((double)pontszam / files.Length * 100)}%");
				NavigationService.Navigate(new UtolsoMondat());
            }
        }

        private void Check(object sender, RoutedEventArgs e)
        {
			if (!disabled)
			{
				disabled = true;
                var senderbtn = (sender as Button);
                if (senderbtn.Content.ToString() == GetNameFromPath(files[pageIndex]))
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

		private string GetNameFromPath(string path)
		{
			return path.Split('\\')[^1].Split('.')[0].Replace('_', ' ');
        }
    }
}
