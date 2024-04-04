using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Objects
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Alkoto> list = new();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Start(object sender, RoutedEventArgs e)
		{
			var f = File.ReadAllLines("alkoto.txt");

			for(int i = 0; i < f.Length; i+=2)
			{
				list.Add(new(f[i], int.Parse(f[i + 1].Split(" ")[0]), int.Parse(f[i + 1].Split(" ")[1])));
			}

			lb.Items.Clear();
			list.ForEach(x => lb.Items.Add(x));
			vElemei.Visibility = Visibility.Visible;
			lb.Visibility = Visibility.Visible;
			disabledSp.IsEnabled = true;
		}

		private void Res(IEnumerable<Alkoto> collection)
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
			Res(list.Sum(x => x.Valid).ToString());
		}

		private void _2(object sender, MouseButtonEventArgs e)
		{
			Res(list.Average(x => x.NotValid).ToString());
		}

		private void _3(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(y => y.Valid == list.Max(x => x.Valid)).ToString());
		}

		private void _4(object sender, MouseButtonEventArgs e)
		{
			var s = list.FirstOrDefault(x => x.NotValid > 10);
			if (s == null) Res("Nem volt ilyen alkotó.");
			else Res(s.Name);
		}

		private void _5(object sender, MouseButtonEventArgs e)
		{
			Res(list.FirstOrDefault(y => y.Valid == list.Where(x => x.NotValid == 0).Max(x => x.Valid)).ToString());
		}

		private void _6(object sender, MouseButtonEventArgs e)
		{
			Res(list.Where(x => x.Valid > 50));
		}

		private void _7(object sender, MouseButtonEventArgs e)
		{
			Res(list.OrderByDescending(x => x.Valid));
		}
	}

	class Alkoto
	{
		public string Name { get; set; }
		public int NotValid { get; set; }
		public int Valid { get; set; }
		public int All => Valid + NotValid;

		public Alkoto(string name, int notValid, int valid)
		{
			Name = name;
			NotValid = notValid;
			Valid = valid;
		}

		public override string ToString()
		{
			return $"{Name}, össz: {All}, nem érvényes: {NotValid}, érvényes: {Valid}";
		}
	}
}