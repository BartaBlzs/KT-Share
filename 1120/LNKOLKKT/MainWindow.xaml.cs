using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace LNKOLKKT
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

        private int GetLnko(int elso, int masodik)
        {
            int a = Math.Max(elso, masodik);
            int b = Math.Min(elso, masodik);
            var m = 1;
            while (m != 0 && b != 0)
            {
                m = a % b;
                a = b;
                b = m;
            }
            return a;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var n1val = int.Parse(n1.Text);
            var n2val = int.Parse(n2.Text);
            var sz1val = int.Parse(sz1.Text);
            var sz2val = int.Parse(sz2.Text);
            var lnko = GetLnko(n1val, n2val);
            var lkkt = n1val * n2val / lnko;
            var elso = lkkt/n1val*sz1val;
            var masodik = lkkt/n2val*sz2val;
            tbout.Text = $"osszeadas: {Egyszerusit(elso + masodik, lkkt)}" +
                $"\nkivonas: {Egyszerusit(elso - masodik, lkkt)}" +
                $"\nszorzas: {Egyszerusit(sz1val * sz2val, n1val * n2val)}" +
                $"\nosztas: {Egyszerusit(sz1val * n2val, sz2val * n1val)}";
        }

        private string Egyszerusit(int sz, int n)
        {
            var lnko = GetLnko(sz, n);
            return $"{sz/lnko}/{n/lnko}";
        }

        private void Tin(object sender, TextCompositionEventArgs e)
        {
            if(!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        
    }
}
