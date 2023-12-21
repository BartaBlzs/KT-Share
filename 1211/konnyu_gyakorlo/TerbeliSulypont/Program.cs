using System.Numerics;

namespace TerbeliSulypont
{
	class Program
	{
		static List<(double X, double Y, double Z)> Points = new();
		static void Main(string[] args)
		{
			StartRead();
			GetPoints();
		}

		private static void GetPoints()
		{
            Console.WriteLine($"Sulypont: ({Points.Average(x => x.X)}, {Points.Average(x => x.Y)}, {Points.Average(x => x.Z)})");
        }

		private static void StartRead()
		{
            Console.WriteLine("X Y Z (space-el elválasztva)");

			while (true)
			{
				var cur = ReadOne();
				if (cur == null) break;
				Points.Add(cur.Value);
			}
		}

		private static (double X, double Y, double Z)? ReadOne()
		{
			var spl = Console.ReadLine().Split(' ');
			if (spl.Length == 3)
			{
				var splI = Array.ConvertAll(spl, double.Parse);
				return (splI[0], splI[1], splI[2]);
			}
			return null;
		}
	}
}