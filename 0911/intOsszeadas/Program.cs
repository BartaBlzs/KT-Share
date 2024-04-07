namespace intOsszeadas
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var sum = 0;
			while (true)
			{
				var s = Console.ReadLine();
				if (int.TryParse(s, out int i))
				{
					sum += i;
				}
				else break;
			}
			Console.WriteLine(sum);
		}
	}
}
