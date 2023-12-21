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
            Console.WriteLine($"7. feladat\nA legtöbb szóból álló felirat:\n{feliratok.Where(y => y.Szoszam == feliratok.Max(x => x.Szoszam)).FirstOrDefault()}");
        }

        private static void feladat5()
        {
            Console.WriteLine($"5. feladat\nFeliratok száma: {feliratok.Count}");
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
        }

        public string StrIdozites()
        {
            var start = Idozites.Split('-')[0];
            var end = Idozites.Split('-')[1];

            var spl = Array.ConvertAll(start.Split(':'), int.Parse);
            TimeSpan startTs = new(spl[0] / 60, spl[0] % 60, spl[1]);

            spl = Array.ConvertAll(end.Split(':'), int.Parse);
            TimeSpan endTs = new(spl[0] / 60, spl[0] % 60, spl[1]);

            return $"{startTs:hh\\:mm\\:ss} --> {endTs:hh\\:mm\\:ss}\n{Felirat}";
        }

        private void SzavakSzama()
        {
            Szoszam = Felirat.Split().Length;
        }

        public override string ToString()
        {
            return $"{Idozites}\n{Felirat}";
        }
    }
}