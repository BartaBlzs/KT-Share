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

namespace Ints
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<int> list = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
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

		private void Res(IEnumerable<int> collection)
		{
			output.Items.Clear();
			collection.ToList().ForEach(x => output.Items.Add(x));
		}

		private void Res(double item)
		{
			output.Items.Clear();
			output.Items.Add(item);
		}

		private void _1(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x > 5));
		}

		private void _2(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x > 50));
		}

		private void _3(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x > 5));
		}

		private void _4(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x > 50));
		}

		private void _5(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x > 2 && x < 6));
		}

		private void _6(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(x => x % 2 == 0));
		}

		private void _7(object sender, MouseButtonEventArgs e)
		{
			Res(list.LastOrDefault(x => x % 2 == 0));
		}

		private void _8(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x % 2 == 0));
		}

		private void _9(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => x * x)); // math.pow returns double
		}

		private void _10(object sender, MouseButtonEventArgs e)
		{
			Res(list.Select(x => x * x * x));
		}

		private void _11(object sender, MouseButtonEventArgs e)
		{
			Res(list.Sum());
		}

		private void _12(object sender, MouseButtonEventArgs e)
		{
			Res(list.Sum(x => x * x));
		}

		private void _13(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x % 2 == 0).Sum());
		}

		private void _14(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.Average());
			}
			catch { }
		}

		private void _15(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.Where(x => x % 2 == 0).Average());
			}
			catch { }
		}

		private void _16(object sender, MouseButtonEventArgs e)
		{
			Res(list.Count(x => x % 2 == 0));
		}

		private void _17(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.Max());
			}
			catch { }
		}

		private void _18(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.IndexOf(list.Max()));
			}
			catch { }
		}

		private void _19(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.Where(x => x > 0).Max());
			}
			catch { }
		}

		private void _20(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Res(list.Where(x => x < 0).Max());
			}
			catch { }
		}
	}
}