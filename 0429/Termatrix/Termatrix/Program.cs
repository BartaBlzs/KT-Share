

string inp;
(int _0, int _1, int _2) dimension = (0, 0, 0);
dynamic s = null;
do
{
	Console.Clear();
	Console.Write("1. feladat: 233 vagy 345 vagy 422 vagy 555\nA választott szám: ");
	inp = Console.ReadLine();
} while (!Read(inp));

Console.WriteLine("2. feladat: A mátrix elemei: ");
Show(s);
SumAndAvg();
Middle();
Console.WriteLine("5. feladat: Az E mátrix elemei: ");
E();
Skalar();

bool Read(string num)
{
	if (!File.Exists($"M{num}.txt")) return false;
	var f = File.ReadAllLines($"M{num}.txt").Skip(1).ToList();
	var split = num.ToCharArray().Select(x => x - '0').ToList();
	dimension._0 = split[0];
	dimension._1 = split[1];
	dimension._2 = split[2];
	s = new int[dimension._0, dimension._1, dimension._2];

	for (int i = 0, line = 0; i < dimension._0; i++)
	{
		for (int j = 0; j < dimension._1; j++)
		{
			var spl = f[line++].Split('\t');
			for (int k = 0; k < dimension._2; k++)
			{
				s[i, j, k] = int.Parse(spl[k]);
			}
        }
    }

    return true;
}

void Show(dynamic s)
{
	Console.WriteLine("{");
	for (int i = 0, line = 0; i < dimension._0; i++)
	{
        Console.WriteLine("  {");
        for (int j = 0; j < dimension._1; j++)
		{
            Console.Write("    { ");
            for (int k = 0; k < dimension._2; k++)
			{
				if(k == dimension._2 - 1)
					Console.Write($"{s[i, j, k]}");
				else
					Console.Write($"{s[i, j, k]}, ");
			}
			if(j == dimension._1 - 1)
				Console.WriteLine(" }");
			else
				Console.WriteLine(" },");
        }
		if (i == dimension._0 - 1)
			Console.WriteLine("  }");
		else
			Console.WriteLine("  },");

	}
	Console.WriteLine("}");
}

void SumAndAvg()
{
	var sum = 0;
	for (int i = 0; i < dimension._0; i++)
	{
		for (int j = 0; j < dimension._1; j++)
		{
			for (int k = 0; k < dimension._2; k++)
			{
				sum += s[i, j, k];
			}
		}
	}

	Console.WriteLine($"Összeg: {sum}\nÁtlag: {Math.Round((double)sum/(dimension._0 * dimension._1 * dimension._2), 2)}");
}

int Middle(bool write = true)
{
	var _0 = GetHalf(dimension._0);
	var _1 = GetHalf(dimension._1);
	var _2 = GetHalf(dimension._2);

	if(write)
		Console.WriteLine($"A kozepso pont koordinatai: [{_0}, {_1}, {_2}]\nÉrtéke: {s[_0 - 1, _1 - 1, _2 - 1]}");
	return s[_0 - 1, _1 - 1, _2 - 1];
}

int GetHalf(int dimension)
{
	var toRound = (double)dimension / 2;
    if (toRound % 1 == 0) return (int)toRound;
	else return (int)toRound + 1;
}

void E()
{
	var middleValue = Middle(false);
	int[,,] e = new int[dimension._0, dimension._1, dimension._2];
	for (int i = 0; i < dimension._0; i++)
	{
		for (int j = 0; j < dimension._1; j++)
		{
			for (int k = 0; k < dimension._2; k++)
			{
				e[i, j, k] = s[i, j, k] - middleValue;
			}
		}
	}
	Show(e);
}

void Skalar()
{
	int skalar;
	do
	{
		Console.Write("Adjon meg egy skalárt: ");
		inp = Console.ReadLine();
	} while (!int.TryParse(inp, out skalar));

	for (int i = 0; i < dimension._0; i++)
	{
		for (int j = 0; j < dimension._1; j++)
		{
			for (int k = 0; k < dimension._2; k++)
			{
				s[i, j, k] *= skalar;
			}
		}
	}
    Console.WriteLine("\nA mátrix skaláris szorzata: ");
    Show(s);
}