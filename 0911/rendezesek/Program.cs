namespace rendezesek
{
	class Program
	{
		static Random R = new();
		int[] tomb;

		static void Main(string[] args)
		{
			int N = elemszam();
			int[] T = new int[N];
			int[] temp = new int[N];
			TombFeltoltes(T, temp);
			Rendezes r = new(T);
			Console.WriteLine($"Egyszeru cseres: {r.EgyszeruCseres()}");
			Visszatoltes(T, temp);
			r = new(T);
			Console.WriteLine($"Boborek cseres: {r.BubbleSort()}");
			Visszatoltes(T, temp);
			r = new(T);
			Console.WriteLine($"MinMax: {r.MinMax()}");
			Visszatoltes(T, temp);
			r = new(T);
			Console.WriteLine($"Shell: {r.Shell()}");
			Console.ReadKey();
		}

		static void Visszatoltes(int[] eredeti, int[] temp)
		{
			for (int i = 0; i < eredeti.Length; i++)
			{
				eredeti[i] = temp[i];
			}
		}

		static int elemszam()
		{
			int N = 0;
			do
			{
				Console.WriteLine("add meg az elemszamot (1 - 2^32-1):");
			} while (!int.TryParse(Console.ReadLine(), out N) || N < 1);
			return N;
		}

		static void TombFeltoltes(int[] tomb, int[] Temp)
		{
			for (int i = 0; i < tomb.Length; i++)
			{
				tomb[i] = R.Next(tomb.Length, 3 * tomb.Length);
				Temp[i] = tomb[i];
			}
		}

	}
	class Rendezes
	{
		int[] tomb;
		public Rendezes(int[] tomb)
		{
			this.tomb = tomb;
		}

		public TimeSpan EgyszeruCseres()
		{
			var start = DateTime.Now;

			for (int i = 0; i < tomb.Length; i++)
			{
				for (int j = 0; j < tomb.Length; j++)
				{
					if (tomb[i] < tomb[j])
						(tomb[i], tomb[j]) = (tomb[j], tomb[i]);
				}
			}
			var stop = DateTime.Now;
			return stop - start;
		}

		public TimeSpan BubbleSort()
		{
			var start = DateTime.Now;

			int temp;
			for (int write = 0; write < tomb.Length; write++)
			{
				for (int sort = 0; sort < tomb.Length - 1; sort++)
				{
					if (tomb[sort] > tomb[sort + 1])
					{
						temp = tomb[sort + 1];
						tomb[sort + 1] = tomb[sort];
						tomb[sort] = temp;
					}
				}
			}
			return DateTime.Now - start;
		}

		public TimeSpan MinMax()
		{
			var start = DateTime.Now;
			int akt_index;
			for (int i = 0; i < tomb.Length - 1; i++)
			{
				akt_index = i;
				for (int j = i + 1; j < tomb.Length; j++)
				{
					if (tomb[akt_index] > tomb[j]) akt_index = j;
				}
				(tomb[i], tomb[akt_index]) = (tomb[akt_index], tomb[i]);
			}
			var stop = DateTime.Now;
			return stop - start;
		}

		public TimeSpan Shell()
		{
			var start = DateTime.Now;
			int tavolsag = tomb.Length / 3, j = 0;
			int csere;
			while (tavolsag > 0)
			{
				for (int i = 0; i < tomb.Length; i++)
				{
					j = i;
					csere = tomb[i];
					while (j >= tavolsag && tomb[j - tavolsag] > csere)
					{
						tomb[j] = tomb[j - tavolsag];
						j = j - tavolsag;

					}
					tomb[j] = csere;
				}
				if (tavolsag / 2 != 0) tavolsag /= 2;
				else if (tavolsag == 1) tavolsag = 0;
				else tavolsag = 1;
			}
			var stop = DateTime.Now;
			return stop - start;
		}

		static void Kiiras(int[] tomb)
		{
			Console.WriteLine();
			foreach (var i in tomb)
			{
				Console.WriteLine(i);
			}
		}
	}
}
