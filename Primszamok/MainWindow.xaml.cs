using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Primszamok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            primesl.Content = GeneratePrimes(int.Parse(primesIn.Text));
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
			isPrimel.Content = IsPrime(int.Parse(isPrimeIn.Text));
        }

        public static TimeSpan GeneratePrimes(int n)
        {
            var start = DateTime.Now;
            List<int> primes = new();
            for (var i = 2; i <= n; i++)
            {
                var ok = true;
                foreach (var prime in primes)
                {
                    if (prime * prime > i)
                        break;
                    if (i % prime == 0)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                    primes.Add(i);
            }
            return DateTime.Now - start;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (int i = 3; i <= Math.Floor(Math.Sqrt(number)); i += 2)
                if (number % i == 0) return false;

            return true;
        }

		private void NumberValidation(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
