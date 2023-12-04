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

namespace Kaputelefon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly string exeFolder;
		readonly string validCode = "1205";
		string inputCode = "";
		public MainWindow()
		{
			exeFolder = Directory.GetCurrentDirectory();
			InitializeComponent();
			Render();
		}

		private void Render()
		{
			inputCode = "";
			wp.Children.Clear();
			var l = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Random rnd = new Random();
			l = l.OrderBy(x => Guid.NewGuid()).ToList();
			foreach (var i in l.Take(l.Count - 1))
			{
				Image img = new ButtonImg(i, exeFolder, wp.Width, false);
				img.MouseUp += Img_MouseUp;
				wp.Children.Add(img);
			}
			Image centerImg = new ButtonImg(l.Last(), exeFolder, wp.Width, true);
			centerImg.MouseUp += Img_MouseUp;
			wp.Children.Add(centerImg);
		}

		private void Img_MouseUp(object sender, MouseButtonEventArgs e)
		{
			inputCode += (sender as ButtonImg).Number;
			if (inputCode.Length == 4)
			{
				if (inputCode == validCode) MessageBox.Show("Jó a kód, az ajtó kinyílt!");
				else MessageBox.Show("Rossz a kód, kérlek üsd be újra!");
				inputCode = "";
			}
		}

		private void Reset(object sender, RoutedEventArgs e)
		{
			Render();
		}
	}
	public class ButtonImg : Image
	{
		public int Number { get; set; }
		public ButtonImg(int number, string exeFolder, double WpWidth, bool center)
		{
			Number = number;
			Source = new BitmapImage(new Uri($"{exeFolder}/kepek/{number}.png", UriKind.Absolute));
			Width = WpWidth / 3;
			if (center) Margin = new Thickness((WpWidth - Width) / 2, 0, 0, 0);
		}
	}
}
