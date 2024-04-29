namespace Absztrakt1
{
	class Ikozaeder : SzabalyosTestek
	{
		public Ikozaeder(double a) : base(a) { }

		public override double Felszin()
		{
			return 5 * Math.Pow(A, 2) * Math.Sqrt(3);
		}

		public override double Terfogat()
		{
			return Math.Pow(A, 3) * (15 + 5 * Math.Sqrt(5)) / 12;
		}
	}
}