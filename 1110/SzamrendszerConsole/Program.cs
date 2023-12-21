using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzamrendszerAtvalto
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseConverter converter = new BaseConverter();
            while (true)
            {
                try
                {
                    Console.Write("Az átváltandó szám: ");
                    var fromNumber = Console.ReadLine();

                    Console.Write("Az alap számrendszer: ");
                    var fromBase = int.Parse(Console.ReadLine());

                    Console.Write("A vég számrendszer: ");
                    var toBase = int.Parse(Console.ReadLine());
                    var res = converter.AnyConvert(fromNumber, fromBase, toBase);
                    Console.WriteLine($"{res}\n");
                    File.AppendAllText("konzol_valtasok.txt", $"{DateTime.Now.ToLocalTime()}\n{fromNumber} ({fromBase}) => {res} ({toBase})\n\n");
                }
                catch
                {
                    Console.WriteLine("Hibás számot adtál meg!\n");
                }
                
            }
        }
    }
}
