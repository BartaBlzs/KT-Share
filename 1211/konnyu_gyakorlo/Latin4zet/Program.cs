namespace Latin4zet
{
	internal class Program
	{
		static void Main(string[] args)
		{
			WriteLatinSqr(int.Parse(Console.ReadLine()));
		}

		private static void WriteLatinSqr(int n)
		{
			for(int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					Console.Write((i + j)%n + 1 + " ");
				}
                Console.WriteLine();
            }
		}
	}
}
