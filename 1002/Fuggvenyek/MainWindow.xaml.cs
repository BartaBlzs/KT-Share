using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		TabControl tc = new();

		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var dt = new DataTable();
			var f = File.ReadAllLines("fuggvenyek.dat");
			int[] ET = new int[] { 1, 2, 3, 4, 5, 10 };
			
			foreach (var line in f)
			{
				PointCollection points = new();
				Canvas canvas = new()
				{
					Width = 600,
					Height = 350
				};

				foreach (var et in ET)
				{
					var p = new System.Windows.Point(et * 20, (double.Parse(dt.Compute(line.Split('=')[1].Replace(",", ".").Replace(" x", et.ToString()).Replace("x", $"*{et}"), "").ToString())*20) * -1);
					points.Add(p);

					TextBlock textBlock = new()
					{
						Text = et.ToString()
					};
					Line vrl = new()
					{
						X1 = et*20,
						X2 = et*20,
						Y1 = canvas.Height / 2 - 7,
						Y2 = canvas.Height / 2 + 7,
						Stroke = Brushes.SlateGray,
						StrokeThickness = 2,
						SnapsToDevicePixels = true
					};
					Canvas.SetLeft(textBlock, et * 20 - 7);
					Canvas.SetTop(textBlock, canvas.Height/2+10);

					canvas.Children.Add(vrl);
					canvas.Children.Add(textBlock);
				}
				TabItem ti = new()
				{
					Header = line.Split("=")[0],
					Content = CreateCanvas(points, canvas)
				};
				tc.Items.Add(ti);
				
			}
			grid.Children.Add(tc);
		}
		
		private Canvas CreateCanvas(PointCollection points, Canvas Canvas)
		{
			Canvas canvas = Canvas;
			
			var myPolyline = new Polyline
			{
				Stroke = Brushes.SlateGray,
				StrokeThickness = 2,
				FillRule = FillRule.EvenOdd,
				Points = points
			};

			var hr = new Line()
			{
				X1 = 0,
				X2 = canvas.Width,
				Y1 = canvas.Height / 2,
				Y2 = canvas.Height / 2,
				Stroke = Brushes.SlateGray,
				StrokeThickness = 2,
				SnapsToDevicePixels = true
			};

			var vr = new Line()
			{
				X1 = 0,
				X2 = 0,
				Y1 = 0,
				Y2 = canvas.Height,
				Stroke = Brushes.SlateGray,
				StrokeThickness = 2,
				SnapsToDevicePixels = true
			};

			Canvas.SetTop(myPolyline, canvas.Height / 2);

			canvas.Children.Add(myPolyline);
			canvas.Children.Add(hr);
			canvas.Children.Add(vr);
			return canvas;
		}
	}
		
}