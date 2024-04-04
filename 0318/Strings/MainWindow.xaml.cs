using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Strings
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<string> list = new List<string>() { "Anna", "Peti", "Gergő", "Bea", "Kristóf", "Sára", "Nikolett", "Bence", "Dominik", "Liliána", "Patrik", "Adrián", "Vivien", "László", "Hajnalka" };
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Start(object sender, RoutedEventArgs e)
		{
			lb.Items.Clear();
			list.ForEach(x => lb.Items.Add(x));
			vElemei.Visibility = Visibility.Visible;
			lb.Visibility = Visibility.Visible;
			disableGrid.IsEnabled = true;
		}


		private void Res(IEnumerable<string> collection)
		{
			output.Items.Clear();
			collection.ToList().ForEach(x => output.Items.Add(x));
        }

		private void Res(string item)
		{
			output.Items.Clear();
			output.Items.Add(item);
		}

		private void _1(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => x.ToUpper()));
		}

		private void _2(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x.ToUpper().StartsWith("L")));
		}

		private void _3(object sender, MouseButtonEventArgs e)
		{
			Res(list.LastOrDefault(x => x.ToUpper().StartsWith("L")));
		}

		private void _4(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x.ToUpper().StartsWith("T")));
		}

		private void _5(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x.Length > 5));
		}

		private void _6(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x.ToUpper().Contains("T")));
		}

		private void _7(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x.ToUpper().Contains('T')));
		}

		private void _8(object sender, MouseButtonEventArgs e)
		{
			Res(list.Any(x => x.ToUpper().StartsWith("T")).ToString());
		}

		private void _9(object sender, MouseButtonEventArgs e)
		{
			var l = output.Items.Cast<string>().ToList();
			l.RemoveAll(x => x.ToUpper().StartsWith("P") || x == "False" || x == "True");
			Res(l);
		}

		private void _10(object sender, MouseButtonEventArgs e)
		{
			var l = output.Items.Cast<string>().ToList();
			l.AddRange(list.Where(x => x.ToUpper().StartsWith("P") && !l.Contains(x)));
			Res(l);
		}

		private void _11(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x.Length >= 4));
		}

		private void _12(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => x + " okos"));
		}

		private void _13(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => x.Length.ToString()));
		}

		private void _14(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => $"{x} - {x.Length}"));
		}
	}
}