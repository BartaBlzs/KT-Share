using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absztrakt1
{
	class Kocka : SzabalyosTestek
	{
		public Kocka(double a) : base(a) { }

		public override double Felszin()
		{
			return 6 * Math.Pow(A, 2);
		}

		public override double Terfogat()
		{
			return Math.Pow(A, 3);
		}
	}
}