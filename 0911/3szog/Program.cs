namespace _3szog
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Adja meg a haromszog 1. oldalát: ");
			var a = double.Parse(Console.ReadLine());
			Console.Write("Adja meg a haromszog 1. oldalát: ");
			var b = double.Parse(Console.ReadLine());
			Console.Write("Adja meg a haromszog 1. oldalát: ");
			var c = double.Parse(Console.ReadLine());

			if(a + b > c && a + c > b &&  b + c > a)
			{
				var s = (a + b + c) / 2;
				Console.WriteLine($"A haromszog terulete: {Math.Sqrt(s * (s - a) * (s - b) * (s - c))}");
            }
			else Console.WriteLine("Az adatok nem lehetnek 3szög oldalai!");
        }
	}
}
