namespace Absztrakt1
{
	class Dodekaeder : SzabalyosTestek
	{
		public Dodekaeder(double a) : base(a) { }

		public override double Felszin()
		{
			return 3 * Math.Pow(A, 2) * Math.Sqrt(25 + 10 * Math.Sqrt(5));
		}

		public override double Terfogat()
		{
			return Math.Pow(A, 3) * (15 + 7 * Math.Sqrt(5)) / 4;
		}
	}
}