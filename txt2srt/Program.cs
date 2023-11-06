using System;
using System.Text;

namespace txt2srt
{
    class Program
    {
        static List<IdozitettFelirat> feliratok = new();
        static void Main(string[] args)
        {
            beolvas();
            feladat5();
            feladat7();
            feladat9();
        }

        private static void feladat9()
        {
            StringBuilder sb = new();
            for (int i = 0; i < feliratok.Count; i++)
            {
                sb.Append($"{i+1}\n{feliratok[i].StrIdozites()}\n\n");
            }
            File.WriteAllText("felirat.srt", sb.ToString());
        }

        private static void feladat7()
        {
            Console.WriteLine($"A legtöbb szóból álló felirat: {feliratok.Max(x => x.Szoszam)}");
        }

        private static void feladat5()
        {
            Console.WriteLine($"Feliratok száma: {feliratok.Count}");
        }

        private static void beolvas()
        {
            var f = File.ReadAllLines("feliratok.txt");
            for (int i = 0; i < f.Length- 1; i += 2)
            {
                feliratok.Add(new IdozitettFelirat(f[i], f[i + 1]));
            }
        }
    }

    class IdozitettFelirat
    {
        public string Idozites { get; set; }
        public string Felirat { get; set; }
        public int Szoszam {  get; set; }

        public IdozitettFelirat(string idozites, string felirat)
        {
            Idozites = idozites;
            Felirat = felirat;
            SzavakSzama();
            StrIdozites();
        }

        public string StrIdozites()
        {
            var kezd = Idozites.Split('-')[0];
            var veg = Idozites.Split('-')[1];
            string srt = $"{int.Parse(kezd.Split(':')[0])/60:00}:{int.Parse(kezd.Split(':')[0])%60:00}:{kezd.Split(':')[1]:00} --> " +
                         $"{int.Parse(veg.Split(':')[0]) / 60:00}:{int.Parse(veg.Split(':')[0]) % 60:00}:{veg.Split(':')[1]:00}\n" +
                         $"{Felirat}";
            return srt;
        }

        private void SzavakSzama()
        {
            Szoszam = Felirat.Split().Length;
        }
    }
}