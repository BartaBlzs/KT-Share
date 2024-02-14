using System;
using System.Globalization;
using System.Text;

namespace Fuvar
{
	class Program
	{
		static List<Fuvar> fuvarok = new();
		static string Header;
		static void Main(string[] args)
		{
			Read();
			Feladat3();
			Feladat4();
			Feladat5();
			Feladat6();
			Feladat7();
			Feladat8();
		}

		private static void Feladat8()
		{
			StringBuilder sb = new($"{Header}\n");
			foreach(var i in fuvarok.Where(x => x.Duration > 0 && x.Price > 0 && x.DistanceInMiles == 0)
									.OrderBy(x => x.StartTime))
			{
				sb.AppendLine(i.fullLine);
			}
			File.WriteAllText("Hibak.txt", sb.ToString());
		}

		private static void Feladat7()
		{
            Console.WriteLine($"7. feladat: Leghosszabb fuvar\n{fuvarok.FirstOrDefault(y => y.Duration == fuvarok.Max(x => x.Duration))}");
        }

		private static void Feladat6()
		{
            Console.WriteLine($"6. feladat: {Math.Round(fuvarok.Sum(x => x.DistanceInKm), 2)} km");
        }

		private static void Feladat5()
		{
            Console.WriteLine("5. feladat:");
            foreach (var f in fuvarok.GroupBy(x=>x.PayMethod).Select(x => new { id = x.First().PayMethod, Count = x.Count()}))
			{
                Console.WriteLine($"\t{f.id}: {f.Count} fuvar");
            }
		}

		private static void Feladat4()
		{
			var fs = fuvarok.Where(x => x.Id == 6185);
			Console.WriteLine($"4. feladat {fs.Count()} alatt {fs.Sum(x => x.Price + x.Tip)}$");
        }

		private static void Feladat3()
		{
            Console.WriteLine($"3. feladat: {fuvarok.Count} fuvar");
        }

		private static void Read()
		{
			var f = File.ReadAllLines("fuvar.csv");
			Header = f[0];
			foreach (var line in f.Skip(1))
			{
				fuvarok.Add(new Fuvar(line));
			}
		}
	}

	class Fuvar
	{
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public double DistanceInMiles { get; set; }
		public double DistanceInKm => DistanceInMiles * 1.6;
        public double Price { get; set; }
		public double Tip { get; set; }
        public string PayMethod { get; set; }
		public string fullLine { get; set; }

		public Fuvar(string line)
		{
			fullLine = line;
			var spl = line.Split(';');
			Id = int.Parse(spl[0]);
			StartTime = DateTime.Parse(spl[1]);
			Duration = int.Parse(spl[2]);
			DistanceInMiles = double.Parse(spl[3].Replace(",", "."), CultureInfo.InvariantCulture);
			Price = double.Parse(spl[4].Replace(",", "."), CultureInfo.InvariantCulture);
			Tip = double.Parse(spl[5].Replace(",", "."), CultureInfo.InvariantCulture);
			PayMethod = spl[6];
        }

		public override string ToString()
		{
			return $"\tFuvar hossza: {Duration} masodperc\n\tTaxi azonosito: {Id}\n\tMegtett tavolsag: {DistanceInMiles} mi\n\tViteldij: {Price}$";
		}
	}
}