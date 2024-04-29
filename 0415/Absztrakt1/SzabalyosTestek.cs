using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absztrakt1
{
	abstract class SzabalyosTestek
	{
		protected double A { get; set; }
		public SzabalyosTestek(double a)
		{
			A = a;
		}
		public abstract double Felszin();
		public abstract double Terfogat();
	}
}
