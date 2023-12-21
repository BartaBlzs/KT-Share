namespace foatlo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[,] test1 =
			{
				{0, 2, 3, 0, 2, 3, 0, 2, 3},
				{4, 0, 6, 4, 0, 6, 4, 0, 6},
				{7, 8, 0, 7, 8, 0, 7, 8, 0},
				{0, 2, 3, 0, 2, 3, 0, 2, 3},
				{4, 0, 6, 4, 0, 6, 4, 0, 6},
				{7, 8, 0, 7, 8, 0, 7, 8, 0},
				{0, 2, 3, 0, 2, 3, 0, 2, 3},
				{4, 0, 6, 4, 0, 6, 4, 0, 6},
				{7, 8, 0, 7, 8, 0, 7, 8, 0}
			};

			int[,] test2 =
			{
				{2, 2, 3, 0, 2, 3, 0, 2, 3},
				{4, 3, 6, 4, 0, 6, 4, 0, 6},
				{7, 8, 4, 7, 8, 0, 7, 8, 0},
				{0, 2, 3, 5, 2, 3, 0, 2, 3},
				{4, 0, 6, 4, 6, 6, 4, 0, 6},
				{7, 8, 0, 7, 8, 7, 7, 8, 0},
				{0, 2, 3, 0, 2, 3, 8, 2, 3},
				{4, 0, 6, 4, 0, 6, 4, 9, 6},
				{7, 8, 0, 7, 8, 0, 7, 8, 0}
			};

			Console.WriteLine(IsFoatloAllZeros(test1, test1.GetLength(0)));
			Console.WriteLine(IsFoatloAllZeros(test2, test2.GetLength(0)));

		}

		private static bool IsFoatloAllZeros(int[,] matrix, int n)
		{
			for(int i = 0, j = 0; i < n; i++, j++)
			{
				if (matrix[i, j] != 0) return false;
            }
			return true;
		}
	}
}
