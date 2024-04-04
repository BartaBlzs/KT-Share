using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mozijegy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var blockSize = SystemParameters.PrimaryScreenHeight / 12 - 20;
			for (int i = 0; i < 12; i++)
			{
				var sp = new StackPanel()
				{
					Orientation = Orientation.Horizontal
				};
				for (int j = 0; j < 8; j++)
				{
					var seat = new Seat(i, j, blockSize);
					seat.PreviewMouseUp += (s, e) =>
					{
						if(!seat.Disabled)
						{
							if (seat.selected)
							{
								seat.selected = false;
								cont.Children.Remove(cont.Children.OfType<ContGrid>().FirstOrDefault(x => x.Position == seat.Position));
							}
							else
							{
								seat.selected = true;
								cont.Children.Add(CreateGrid(seat.Position));
							}
						}
					};
					sp.Children.Add(seat);
				}
				outersp.Children.Add(sp);
			}
		}
		private Grid CreateGrid((int row, int col) pos)
		{
			var g = new ContGrid(pos);
			g.ColumnDefinitions.Add(new());
			g.ColumnDefinitions.Add(new());
			Label rowlbl = new()
			{
				Content = pos.row
			};
			Label collbl = new()
			{
				Content = pos.col
			};
			g.Children.Add(collbl);
			g.Children.Add(rowlbl);
			Grid.SetColumn(collbl, 1);
			return g;
		}
	}

	public class ContGrid : Grid
	{
		public (int row, int col) Position { get; set; }
		public ContGrid((int row, int col) position)
		{
			Position = position;
		}
	}

	public class Seat : Label
	{
		ColorAnimation hoverOnAnimation = new((Color)ColorConverter.ConvertFromString("#3b9c3b"), TimeSpan.FromMilliseconds(250));
		ColorAnimation hoverOffAnimation = new((Color)ColorConverter.ConvertFromString("#3ec93e"), TimeSpan.FromMilliseconds(250));
		ColorAnimation clickAnimation = new((Color)ColorConverter.ConvertFromString("#0f4a0f"), TimeSpan.FromMilliseconds(250));
		public bool selected = false;
		public bool Disabled { get; set; }
		public (int row, int col) Position { get; set; }
		public Seat(int row, int col, double size, bool disabled = false)
		{
			Disabled = disabled;
			Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3ec93e"));
			Position = (row, col);
			Height = size;
			Width = size;
			if (Position.row == 4) Margin = new(3, 20, 3, 5);
			if(!Disabled)
			{
				MouseEnter += HoverOnEvent;
				MouseLeave += HoverOffEvent;
				PreviewMouseUp += Click;
			}
		}

		private void Click(object sender, MouseButtonEventArgs e)
		{
			if(selected)
			{
				Background.BeginAnimation(SolidColorBrush.ColorProperty, hoverOffAnimation);
			}
			else
			{
				Background.BeginAnimation(SolidColorBrush.ColorProperty, clickAnimation);
			}
		}

		private void HoverOffEvent(object sender, MouseEventArgs e)
		{
			if(!selected) Background.BeginAnimation(SolidColorBrush.ColorProperty, hoverOffAnimation);
		}

		private void HoverOnEvent(object sender, MouseEventArgs e)
		{
			if (!selected) Background.BeginAnimation(SolidColorBrush.ColorProperty, hoverOnAnimation);
		}

	}
}
