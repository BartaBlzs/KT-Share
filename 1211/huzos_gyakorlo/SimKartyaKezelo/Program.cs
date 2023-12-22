using System.Security.Cryptography;
using System.Text;

//kód szépségét ne nézzük, mert szar lett.

namespace SimKartyaKezelo
{
	class Program
	{
		static List<SIM> Sims = new();
		static void Main(string[] args)
		{
			HomePage();
        }

		private static void HomePage()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine(ASCIITexts.HomePage);
                switch (Console.ReadKey().KeyChar)
				{
					case 'u':
						NewSIM();
						break;
					case 'b':
						Login();
						break;
					case 'k':
						GoodBye();
						break;
				}
			}
		}

		private static void GoodBye()
		{
			Console.Clear();
			Console.WriteLine(ASCIITexts.Goodbye);
			Console.ReadKey();
			Environment.Exit(0);
		}

		private static void Account(SIM currentSim)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine(ASCIITexts.Account);
				Console.WriteLine($"\nSIM adatai:\n\n\tAzonosító(ICCID): {currentSim.ICCID}\n\tEgyenleg: {currentSim.Balance} Ft");
				Console.WriteLine(message);
				message = "";

				switch (Console.ReadKey().KeyChar)
				{
					case 'p':
						ChangePin(currentSim);
						break;
					case 'e':
						BalanceUpdate(currentSim);
						break;
					case 'k':
						return;
				}
			}
		}

		private static void ChangePin(SIM currentSim)
		{
			BackSpace();
			var remainingAttemptCount = 4;
			string curPin;
			string newPin;
			while (--remainingAttemptCount != 0)
			{
				while(true)
				{
					Console.Write("Kérem adjon meg egy új PIN kódot (hossz: 4 - 12): ");
					newPin = Console.ReadLine();
					if (!int.TryParse(newPin, out _))
					{
						Console.WriteLine("A PIN csak számokat tartalmazhat!");
						continue;
					}
					if (newPin.Length > 12 || newPin.Length < 4)
					{
						Console.WriteLine("A PIN hossza nem megfelelő, kérem próbálja újra!");
						continue;
					}
					message = "PIN változtatás sikeres!";
					currentSim.ChangePin(newPin);
					return;
				}
				
			}
			currentSim.LockSim();
			HomePage();
		}

		private static void BalanceUpdate(SIM currentSim)
		{
			BackSpace();
			while (true)
			{
				Console.Write("Kérem adja meg a feltölteni kívánt pénz értékét: ");
				if (long.TryParse(Console.ReadLine(), out long val))
				{
					currentSim.ChangeBalance(val);
					message = "Sikeres Feltöltés!";
					break;
				}
                Console.WriteLine("A megadott érték nem megfelelő!");
            }
		}

		private static void Login()
		{
			Console.Clear();
			Console.WriteLine(ASCIITexts.Login);

			Console.Write(message);
			message = "";

			Console.Write("Kérem adja meg a SIM kártya azonosítóját vagy adjon meg egy 'k' betűt a kilépéshez: ");
			var iccid = Console.ReadLine();
			if (iccid == "k") return;

			var curSim = Sims.FirstOrDefault(x => x.ICCID == iccid);
			if (curSim is null)
			{
				message = "Nem található ilyen azonosítóval rendelkező SIM kártya, próbálja újra!\n";
				Login();
				return;
			}

			if (curSim.IsDead)
			{
				RenderDead();
				return;
			}

			if (curSim.IsLocked)
			{
				RenderLock(curSim);
				return;
			}

			var remainingAttemptCount = 4;
			
			do {
                if (--remainingAttemptCount == 0)
				{
					curSim.LockSim();
					RenderLock(curSim);
					return;
				}
				Console.Write($"Kérem adja meg a PIN kódot: (még {remainingAttemptCount} lehetőség): ");
			} while (!curSim.IsPINMatch(Console.ReadLine()));
			Account(curSim);
			return;
		}

		static void RenderDead()
		{
			Console.WriteLine("Ezt a SIM-et véglegesen zárolták, kérjük keresse fel ügyfélszolgálatunkat!");
			Console.WriteLine("Nyomjon meg bármilyen gombot a főmenübe való visszatéréshez!");
			Console.ReadKey();
			HomePage();
			return;
		}

		static void RenderLock(SIM curSim)
		{
			Console.WriteLine("Ez a SIM 3 sikertelen próbálkozás miatt érvénytelenítve van.");
			if (curSim is TovabbfejlesztettSIM)
			{
				var PUKAttemptCount = 4;
				while (--PUKAttemptCount != 0)
				{
					Console.Write($"Kérem a PUK kódot! (még {PUKAttemptCount} lehetőség): ");
					if (!(curSim as TovabbfejlesztettSIM).IsPUKMatch(Console.ReadLine())) continue;
					Account(curSim);
					return;
				}
				curSim.KillSim();
				RenderDead();
			}
			else
			{
				Console.WriteLine("Ez a SIM nem rendelkezik PUK kóddal. Kérem keresse fel ügyfélszolgálatunkat.");
				Console.WriteLine("Nyomjon meg bármilyen gombot a főmenübe való visszatéréshez!");
				Console.ReadKey();
			}
		}

		static string message = "";
		private static void NewSIM()
		{
			Console.Clear();
			Console.WriteLine(ASCIITexts.NewSIM);
			Console.Write(message);
			message = "";

			bool SIMPlus;
			while (true)
			{
				Console.Write("Továbbfejlesztett SIM? (i: igen, n: nem): ");
				var cin = Console.ReadLine();
				if (cin != "i" && cin != "n") continue;
				SIMPlus = cin == "i";
				break;
			}

			Console.Write("Kérem adjon meg egy PIN kódot (hossz: 4 - 12) vagy lépjen ki a 'b' betű megadásával: ");
			var pin = Console.ReadLine();
			if (pin == "b") return;

			if(!int.TryParse(pin, out _))
			{
				message = "A PIN csak számokat tartalmazhat!\n";
				NewSIM();
				return;
			}

			if (pin.Length > 12 || pin.Length < 4)
			{
				message = "A PIN hossza nem megfelelő, kérem próbálja újra!\n";
				NewSIM();
				return;
			}

			if (SIMPlus)
			{
				TovabbfejlesztettSIM NewSim = new(pin);
				Sims.Add(NewSim);
				Console.WriteLine($"Létrehozás sikeres!\nAz új sim adatai:\n\n\tAzonosító(ICCID): {NewSim.ICCID}\n\tEgyenleg: {NewSim.Balance} Ft\n\tPUK kód: {NewSim.CreatePUK()}\n\nNyomjon meg bármilyen gombot a főmenübe való visszatéréshez!");
			}
			else
			{
				SIM NewSim = new(pin);
				Sims.Add(NewSim);
				Console.WriteLine($"Létrehozás sikeres!\nAz új sim adatai:\n\n\tAzonosító(ICCID): {NewSim.ICCID}\n\tEgyenleg: {NewSim.Balance} Ft\n\nNyomjon meg bármilyen gombot a főmenübe való visszatéréshez!");
			}
			Console.ReadKey();
		}

		static void BackSpace()
		{
			Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
        }
	}

	class SIM
	{
		//ICCID => length: 18 - 22, unique for every sim, here i only use numbers, but it's not limited to them.
		private static HashSet<string> ICCIDSet = new();
		private protected string PIN { get; set; }
		public string ICCID { get; set; }
		public long Balance { get; set; }
		public bool IsLocked { get; private protected set; }
		public bool IsDead { get; private protected set; }
		private static Random rnd = new();

		public SIM(string pin)
		{
			PIN = Crypto.GetHashString(pin);
			ICCID = GetUniqueICCID();
			Balance = 0;
        }

		private static string GetUniqueICCID()
		{
			Random rnd = new();
			while (true)
			{
				var code = GetRandomNumberStringFromLength(20);
				if (ICCIDSet.Add(code))
					return code;
			}
		}

		protected static string GetRandomNumberStringFromLength(int length)
		{
			var code = "";
			for (int i = 0; i < length; i++)
			{
				code += rnd.Next(10);
			}
			return code;
		}
		
		public bool IsPINMatch(string PIN)
		{
			return this.PIN.Equals(Crypto.GetHashString(PIN));
		}

		public void LockSim()
		{
			IsLocked = true;
		}

		public void ChangePin(string newPin)
		{
			PIN = Crypto.GetHashString(newPin);
		}

		public void ChangeBalance(long change)
		{
			Balance += change;
		}

		public void KillSim()
		{
			IsDead = true;
		}
	}

	class TovabbfejlesztettSIM : SIM
	{
		public string PUK { get; set; }
		public TovabbfejlesztettSIM(string pin) : base(pin)
		{
        }

		public string CreatePUK()
		{
			if(PUK == null)
			{
				var puk = GetRandomNumberStringFromLength(6);
				PUK = Crypto.GetHashString(puk);
				return puk;
			}
			return "PUK already exists.";
		}

		public bool IsPUKMatch(string PUK)
		{
			if(!this.PUK.Equals(Crypto.GetHashString(PUK))) return false;
			UnlockSim();
			return true;
		}

		private void UnlockSim()
		{
			IsLocked = false;
		}
	}
}