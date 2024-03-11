var l = new List<Biv>();

var f = File.ReadAllLines("biv_2022-23-8.txt");
f.Skip(1).ToList().ForEach(x => l.Add(new(x)));

int counter = 0;
l.OrderByDescending(x => x.vegeredmeny).Take(15).ToList().ForEach(x => Console.WriteLine($"{++counter}. helyezett: {x.Nev}"));

Console.WriteLine();
typeof(Biv).GetProperties().Where(x => x.PropertyType == typeof(double)).ToList().ForEach(x => Console.WriteLine($"{x.Name}: {l.Average(y => Convert.ToDouble(typeof(Biv).GetProperty(x.Name).GetValue(y)))}"));

Console.WriteLine();
l.Where(x => x.elmelet_1 == l.Max(y => y.elmelet_1)).ToList().ForEach(s => Console.WriteLine($"elmelet 1 {s.Nev} {s.elmelet_1}"));
Console.WriteLine();
l.Where(x => x.gyakorlat_1 == l.Max(y => y.gyakorlat_1)).ToList().ForEach(s => Console.WriteLine($"gyakorlat 1 {s.Nev} {s.gyakorlat_1}"));
Console.WriteLine();
l.Where(x => x.elmelet_2 == l.Max(y => y.elmelet_2)).ToList().ForEach(s => Console.WriteLine($"elmelet 2 {s.Nev} {s.elmelet_2}"));
Console.WriteLine();
l.Where(x => x.gyakorlat_2 == l.Max(y => y.gyakorlat_2)).ToList().ForEach(s => Console.WriteLine($"gyakorlat 2 {s.Nev} {s.gyakorlat_2}"));

class Biv
{
    public string Kod { get; set; }
    public string Nev { get; set; }
    public double elmelet_1 { get; set; }
    public double gyakorlat_1 { get; set; }
    public double elmelet_2 { get; set; }
    public double gyakorlat_2 { get; set; }
	public double elsoFordulo => elmelet_1 + gyakorlat_1;
	public double masodikFordulo => elmelet_2 + gyakorlat_2;
	public double vegeredmeny => elsoFordulo + masodikFordulo;

	public Biv(string line)
	{
		var spl = line.Split('\t');
		Kod = spl[0];
		Nev = spl[1];
		elmelet_1 = double.Parse(spl[2]);
		gyakorlat_1 = double.Parse(spl[3]);
		elmelet_2 = double.Parse(spl[4]);
		gyakorlat_2 = double.Parse(spl[5]);
	}
}