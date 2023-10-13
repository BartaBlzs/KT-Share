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

namespace Zenekarok
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<zenedarab> zenek = new();
		List<string> oszlopok = new() { "zenekar", "album", "cím", "hossz", "ev", "kiado", "form", "mufaj", "stilus", "orszag", "terjeszto", "studio", "hangmernok", "bass", "dob", "gitar", "enek", "gitar2", "fuvos", "enek2" };
		public MainWindow()
		{
			InitializeComponent();
            Read();
        }

		private void Read()
		{
			var f = File.ReadAllLines("zenekarok.txt");
			foreach (var line in f.Skip(1))
			{
				zenek.Add(new zenedarab(line));
			}
			HashSet<string> hs = new HashSet<string>();
			foreach (var zene in zenek)
			{
				zenecimek.Items.Add(zene.cim);
				foreach (var zenesz in zene.hangszerek)
				{
					hs.Add(zenesz.Value);
				}
            }
			zeneszek.ItemsSource = hs;
			allMusicCount.Content += (f.Length - 1).ToString();
			allMusicSum.Content += (zenek.Sum(x => int.Parse(x.hossz.Split(':')[0])*60 + int.Parse(x.hossz.Split(':')[1]))/60).ToString() + "perc";
		}

        private void znChange(object sender, SelectionChangedEventArgs e)
        {
			var combobox = sender as ComboBox;
			zenelb.Content = String.Join("\n", zenek.FirstOrDefault(x => x.cim == combobox.SelectedValue).ToString().Split(", "));
        }

        private void zeneszekChange(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
			var b = zenek.Where(x => x.hangszerek.ContainsValue(combobox.SelectedValue.ToString()));
			var zenekarok = new HashSet<string>();
			foreach (var s in b)
			{
				var hangszer = s.hangszerek.FirstOrDefault(x => x.Value == combobox.SelectedValue.ToString()).Key;
				hangszer = hangszer == "gitar1" || hangszer == "gitar2" ? "gitar" : hangszer;
                hangszerlb.Content = "Hangszer: " + hangszer;
				zenekarok.Add(s.zenekar);
			}

			zenekaroksp.Children.Clear();
			zenekaroksp.Children.Add(new Label()
			{
				Content = "Zenekarok:"
			});
			foreach (var band in zenekarok)
			{
				zenekaroksp.Children.Add(new Label()
				{
					Content = band
				});
			}
        }
    }

    class zenedarab
	{
		public string zenekar { get; set; }
		public string album { get; set; }
		public string cim { get; set; }
		public string hossz { get; set; }
		public string ev { get; set; }
		public string kiado { get; set; }
		public string form { get; set; }
		public string mufaj { get; set; }
		public string stilus { get; set; }
		public string orszag { get; set; }
		public string terjeszto { get; set; }
		public string studio { get; set; }
		public string hangmernok { get; set; }
		public string bass { get; set; }
		public string dob { get; set; }
		public string gitar1 { get; set; }
		public string enek1 { get; set; }
		public string gitar2 { get; set; }
		public string fuvos { get; set; }
		public string enek2 { get; set; }

		public Dictionary<string, string> hangszerek = new();
		
		public zenedarab(string line)
		{
			var spl = line.Split('\t');
            zenekar = spl[0];
			album = spl[1];
            cim = spl[2];
			hossz = spl[3];
			ev = spl[4];
			kiado = spl[5];
			form = spl[6];
			mufaj = spl[7];
			stilus = spl[8];
			orszag = spl[9];
			terjeszto = spl[10];
			studio = spl[11];
			hangmernok = spl[12];
			bass = spl[13];
			dob = spl[14];
			gitar1 = spl[15];
            enek1 = spl[16];
            gitar2 = spl[17];
            fuvos = spl[18];
            enek2 = spl[19];

            hangszerek.Add("dob", dob);
            hangszerek.Add("gitar1", gitar1);
            hangszerek.Add("gitar2", gitar2);
            hangszerek.Add("fuvos", fuvos);
            hangszerek.Add("basszus gitar", bass);
        }

        public override string ToString()
        {
			return $"{zenekar}, {album}, {cim}, {hossz}, {ev}, {kiado}, {form}, {mufaj}, {stilus}, {orszag}, {terjeszto}, {studio}, {hangmernok}, {bass}, {dob}, {gitar1}, {enek1}, {gitar2}, {fuvos}, {enek2}";
        }
    }
}
