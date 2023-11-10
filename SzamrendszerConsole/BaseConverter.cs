using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzamrendszerAtvalto
{
	class BaseConverter
	{
		public string AnyConvert(string fromNumber, int fromBase, int toBase)
		{
			return DecimalToAnyBase(AnyBaseToDecimal(fromNumber, fromBase), toBase);
		}

		private int AnyBaseToDecimal(string anyBase, int fromBase)
		{
			int val(char c)
			{
				if (c >= '0' && c <= '9')
					return (int)c - '0';
				else
					return (int)c - 'A' + 10;
			}

			int len = anyBase.Length;
			int power = 1;
			int num = 0;
			int i;

			for (i = len - 1; i >= 0; i--)
			{

				if (val(anyBase[i]) >= fromBase)
				{
					return -1;
				}

				num += val(anyBase[i]) * power;
				power = power * fromBase;
			}
			return num;
		}

		private string DecimalToAnyBase(int decimalNumber, int toBase)
		{
			string result = string.Empty;
			do
			{
				result = "0123456789ABCDEF"[decimalNumber % toBase] + result;
				decimalNumber /= toBase;
			}
			while (decimalNumber > 0);

			return result;
		}
	}
}