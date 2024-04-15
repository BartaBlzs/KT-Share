using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absztrakt1
{
	class Oktaeder : SzabalyosTestek
	{
		public Oktaeder(double a) : base(a) { }

		public override double Felszin()
		{
			return 2 * Math.Pow(A, 2) * Math.Sqrt(3);
		}

		public override double Terfogat()
		{
			return Math.Pow(A, 3) * Math.Sqrt(2) / 3;
		}
	}
}