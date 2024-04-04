using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace halandzsa
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly string SaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\0218";
		private string SaveFile => SaveFolder + "\\inditasi.data";
		public MainWindow()
		{
			InitializeComponent();
			if (!File.Exists(SaveFile))
			{
				Directory.CreateDirectory(SaveFolder);
				File.WriteAllText(SaveFile, "2");
				MessageBox.Show("Ennyiszer nyitottad meg ezt az appot: 1");
				Environment.Exit(0);
			}
			else
			{
				var i = int.Parse(File.ReadAllText(SaveFile));
				if(i == 21)
				{
					var abc = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
					Random rnd = new();
					HashSet<string> hs = new();
					List<string> words = new();

                    string word = "";
					
                    for (int j = 0; j < 1000; j++)
					{
						do
                        {
                            var rndm = rnd.NextDouble() > .33 ? rnd.Next(7, 12) : rnd.Next(2, 7); // 7 nel hosszabb
                            word = string.Join("", Enumerable.Range(1, rndm).Select(n => abc[rnd.Next(0, abc.Length)])); //szo generalas
                            if (rnd.Next(0, 4) == 0) words.Add(word[0].ToString().ToUpper() + word[1..]); // elso nagybetu
                            else words.Add(word);
                        } while (!hs.Add(word)); // mindegyik szo kulonbozo
                    }
					tb.Text = string.Join(" ", words);
					File.WriteAllText("aaa_halandzs1.txt", string.Join(" ", words));
					for (int j = 0; j < 100; j++)
					{
						File.WriteAllText($"halandzsszo{j + 1}.txt", words[j]);
					}
                }
				else
                {
                    File.WriteAllText(SaveFile, (i+1).ToString());
                    MessageBox.Show($"Ennyiszer nyitottad meg ezt az appot: {i}");
                    Environment.Exit(0);
                }
			}
		}
	}
}