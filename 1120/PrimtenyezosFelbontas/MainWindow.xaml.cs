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

namespace PrimtenyezosFelbontas
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

		private void Tin(object sender, TextCompositionEventArgs e)
		{
			if (!int.TryParse(e.Text, out _)) e.Handled = true;
		}

		private void Start(object sender, RoutedEventArgs e)
		{
			Dictionary<long, long> data = new();

			var i = 2;
			var s = "";
			var inp = int.Parse(tbin.Text);
			while (inp != 1)
			{
				if (inp % i == 0)
				{
					s += $"{inp}\t{i}\n";
					inp /= i;
					if (data.ContainsKey(i)) data[i] += 1;
					else data.Add(i, 1);
				}
				else i++;
			}
			tbout.Text = $"{s}\n{string.Join("*", data.Select(x => $"{x.Key}^{x.Value}"))}={tbin.Text}";
		}
	}
}
