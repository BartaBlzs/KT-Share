namespace masodfoku
{
	internal class Program
	{
		static void Main(string[] args)
		{
            Console.Write("Adja meg a másodfokú egyenlet 1. paraméterét: ");
			var a = int.Parse(Console.ReadLine());
			Console.Write("Adja meg a másodfokú egyenlet 2. paraméterét: ");
			var b = int.Parse(Console.ReadLine());
			Console.Write("Adja meg a másodfokú egyenlet 3. paraméterét: ");
			var c = int.Parse(Console.ReadLine());
			var res = Masodfoku(a, b, c);
			if(res == null)
			{
                Console.WriteLine("Nincs megoldása az egyenletnek!");
            }
			else
			{
				Console.WriteLine($"X1: {res.Value.x1}");
				Console.WriteLine($"X2: {res.Value.x2}");
			}
		}

		static (double x1, double x2)? Masodfoku(int a, int b, int c)
		{
			var preRoot = b * b - 4 * a * c;
			if (preRoot < 0)
			{
				return null;
			}
			else
			{
				return ((Math.Sqrt(preRoot) - b) / (2.0 * a), (-1.0 * Math.Sqrt(preRoot) - b) / (2.0 * a));
			}
		}
	}
}
