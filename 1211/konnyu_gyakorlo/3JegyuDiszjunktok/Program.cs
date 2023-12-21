namespace _3JegyuDiszjunktok
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> diszj = new();
			for(int i = 100; i < 1000; i++)
			{
				var s = i.ToString();
				if (s.Distinct().Count() == 3) diszj.Add(i);
            }
            Console.WriteLine("Az összes olyan háromjegyű szám, melyben nincs kettő, vagy több azonos számjegy:");
            foreach (int i in diszj)
			{
                Console.WriteLine($"{i} ==> {i.ToString().Sum(c => c - '0')}");
            }
        }
	}
}
