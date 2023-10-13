using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TiliToli
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		static int cellCount;
		Lbl mt;
		List<Lbl> labels = new();
		public MainWindow()
		{
			InitializeComponent();
			SizeChanged += SizeChange;
		}

		// simple responsivity
		private void SizeChange(object sender, SizeChangedEventArgs e)
		{
			grid.Height = Math.Min(Height - 150, Width - 150);
			grid.Width = Math.Min(Height - 150, Width - 150);
		}

		private void Print(object sender, RoutedEventArgs e)
		{
			cellCount = int.Parse(cellCountInput.Text);
			ClearGrid();
			var counter = 1;
			for (var i = 1; i <= cellCount; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.RowDefinitions.Add(new RowDefinition());

				for (var j = 1; j <= cellCount; j++)
				{
					Lbl cell;
					if (counter == Math.Pow(cellCount, 2))
						cell = new Lbl(j, i, counter);
					else
						cell = new Lbl(j, i, counter)
						{
							Content = counter++
						};
					cell.SetValue(Grid.ColumnProperty, j - 1);
					cell.SetValue(Grid.RowProperty, i - 1);

					grid.Children.Add(cell);
					labels.Add(cell);
				}
			}
			mt = labels[^1];
			grid.Visibility = Visibility.Visible;
			suffleBtn.Visibility = Visibility.Visible;
		}

		private void ClearGrid()
		{
			grid.Children.Clear();
			grid.ColumnDefinitions.Clear();
			grid.RowDefinitions.Clear();
			labels.Clear();
		}

		private void Md(object sender, MouseButtonEventArgs e)
		{
			var pressed = sender as Lbl;
			if ((pressed.X + 1 == mt.X && pressed.Y == mt.Y) || (pressed.X == mt.X && pressed.Y + 1 == mt.Y)
			|| (pressed.X - 1 == mt.X && pressed.Y == mt.Y) || (pressed.X == mt.X && pressed.Y - 1 == mt.Y)) {
				mt.Content = pressed.Content;
				pressed.Content = "";
				mt = pressed;
			}
			if(e != null)
				if (isGameOver()) MessageBox.Show("Gratulálok, Nyertél!");
		}

		private bool isGameOver()
		{
			foreach (var lbl in labels)
			{
				if (lbl.Content.ToString() != lbl.TotalNum.ToString() && lbl.Content.ToString() != "") return false;
			}
			return true;
		}

		private void Shuffle(object sender, RoutedEventArgs e)
		{
			var rand = new Random();
			for (int i = 0; i < cellCount*1000; i++)
			{
				Md(labels[rand.Next(labels.Count)], null);
			}
		}

		private void NumberValidation(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}

	class Lbl : Label
	{
		public int X {  get; set; }
		public int Y { get; set; }
		public int TotalNum { get; set; }
		public Lbl(int x, int y, int totalNum)
		{
			X = x;
			Y = y;
			TotalNum = totalNum;
		}
	}
}
