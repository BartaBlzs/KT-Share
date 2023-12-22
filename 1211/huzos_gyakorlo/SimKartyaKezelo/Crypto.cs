using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimKartyaKezelo
{
	class Crypto
	{
		public static string GetHashString(string inputString)
		{
			StringBuilder sb = new();
			foreach (byte b in SHA256.HashData(Encoding.UTF8.GetBytes(inputString)))
				sb.Append(b.ToString("X2"));

			return sb.ToString();
		}
	}
}
