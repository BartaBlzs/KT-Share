abstract class Jarmu
{
	public abstract string SzallithatoEgyseg(); //fő, darab, liter, köbméter, stb., csak az egységet küldi vissza
	public abstract double SzallithatoMennyiseg();
	public abstract int KerekekSzama(); //2-3-4-6-8-10-18
	public abstract string Leiras(); //gyártó, típus, szín, üzemanyag, lóerő, tömeg, 
	public abstract string EgyebTulajdonsagok(); //Amit még Te fontosnak tartasz
}

class Motor : Jarmu
{
	public override string EgyebTulajdonsagok()
	{
		return "...";
	}

	public override int KerekekSzama()
	{
		return 2;
	}

	public override string Leiras()
	{
		return "...";
	}

	public override string SzallithatoEgyseg()
	{
		return "fo";
	}

	public override double SzallithatoMennyiseg()
	{
		return 2;
	}
}

class Szemelygepjarmu : Jarmu
{
	public override string EgyebTulajdonsagok()
	{
		return "...";
	}

	public override int KerekekSzama()
	{
		return 4;
	}

	public override string Leiras()
	{
		return "...";
	}

	public override string SzallithatoEgyseg()
	{
		return "fo";
	}

	public override double SzallithatoMennyiseg()
	{
		return 5;
	}
}

class KisTeherauto : Jarmu
{
	public override string EgyebTulajdonsagok()
	{
		return "nagy 3.5 tonnas";
	}

	public override int KerekekSzama()
	{
		return 4;
	}

	public override string Leiras()
	{
		return "passz";
	}

	public override string SzallithatoEgyseg()
	{
		return "kobmeter";
	}

	public override double SzallithatoMennyiseg()
	{
		return 2;
	}
}

sealed class SuzukiSwift1_6GX : Szemelygepjarmu
{
    public string LeirasProp { get; set; }
    public double SzallithatoMenyProp { get; set; }
    public string SzallithatoEgysegProp { get; set; }
    public string EgyebProp { get; set; }
    public int KerekekSzamaProp { get; set; }

    public SuzukiSwift1_6GX(string leiras, double szallithatoMeny, string szallithatoEgyseg, string egyeb, int kerekekSzama)
	{
		LeirasProp = leiras;
		SzallithatoMenyProp = szallithatoMeny;
		SzallithatoEgysegProp = szallithatoEgyseg;
		EgyebProp = egyeb;
		KerekekSzamaProp = kerekekSzama;

	}

	public override string EgyebTulajdonsagok()
	{
		return "nagy 3.5 tonnas";
	}

	public override int KerekekSzama()
	{
		return 4;
	}

	public override string Leiras()
	{
		return "passz";
	}

	public override string SzallithatoEgyseg()
	{
		return "darab"; //????
	}

	public override double SzallithatoMennyiseg()
	{
		return 1;
	}
}