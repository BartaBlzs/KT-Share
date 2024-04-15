using System.Reflection;

namespace Absztrakt1
{
	class Program
	{
		static void Main(string[] args)
		{
			var inp = GetInp();

			var derived = GetAllDerived<SzabalyosTestek>();
			foreach (var i in derived)
			{
				var s = Activator.CreateInstance(i, inp);
                Console.WriteLine($"{s.GetType().Name}:");
                Console.WriteLine($"\tFelszin: {(s as SzabalyosTestek).Felszin()}");
				Console.WriteLine($"\tTerfogat: {(s as SzabalyosTestek).Terfogat()}");
            }
        }

		static List<Type> GetAllDerived<T>()
		{
			var baseType = typeof(T);
			return Assembly.GetAssembly(baseType).GetTypes()
				.Where(t => t != baseType &&
					baseType.IsAssignableFrom(t))
				.ToList();
		}
		
		static double GetInp()
		{
			while (true)
			{
				Console.Write("Adj meg egy számot: ");
				var s = Console.ReadLine();
				if (double.TryParse(s, out double inp)) return inp;
			}
		}
	}
}