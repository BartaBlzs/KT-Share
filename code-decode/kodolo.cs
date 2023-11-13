using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
	class kodolo
	{
		protected Dictionary<char, char> kod;
		public kodolo()
		{
			kod = new Dictionary<char, char>();
			var f = File.ReadAllLines("ABC.txt");
			foreach (var line in f)
			{
				var spl = line.Split('\t');
				kod.Add(spl[0][0], spl[1][0]);
			}
        }

		public string GetCodedString(string s)
		{
			StringBuilder sb = new();
			foreach (var i in s)
			{
				sb.Append(kod[i]);
			}
			return sb.ToString();
		}
	}

	class dekodolo : kodolo
	{
		public string GetDeCodedString(string s)
		{
			
            StringBuilder sb = new();
            foreach (var i in s)
            {
                sb.Append(kod.FirstOrDefault(x => x.Value == i).Key);
            }
            return sb.ToString();
        }
    }
}
