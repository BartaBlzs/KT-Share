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

namespace Szammisztika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string alp = " AÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ";
        static string name;
        static string dob;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTogether()
        {
            int sum = name.ToUpper().Replace(" ", "").Sum(alp.IndexOf);
            var e = 0;
            foreach (var i in dob)
            {
                if (int.TryParse(i.ToString(), out int k))
                {
                    e += k;
                    
                }
            }
            outlb.Content = "Szerencseszám: " + rec(sum * e);
        }

        private int rec(int prev)
        {
            if (prev <= 9) return prev;
            var s = 0;
            foreach (var k in prev.ToString())
            {
                s += int.Parse(k.ToString());
            }
            return rec(s);
        }

        private void Calc(object sender, RoutedEventArgs e)
        {
            name = nametb.Text;
            dob = dobtb.Text;
            AddTogether();
        }
    }
}
