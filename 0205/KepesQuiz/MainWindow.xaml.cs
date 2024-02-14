using MySqlConnector;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KepesQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection;
        DoubleAnimation op1 = new(1, TimeSpan.FromMilliseconds(500), FillBehavior.HoldEnd);
        DoubleAnimation op0 = new(0, TimeSpan.FromMilliseconds(500), FillBehavior.HoldEnd);
        DateTime startTime;
        int currentQ;
        int score;
        public MainWindow()
        {
            InitializeComponent();
            connection = new("server=localhost;uid=root;pwd=root;database=kviz;");
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            nameCont.Visibility = Visibility.Visible;
            startbtn.Visibility = Visibility.Visible;
            curqlbl.Visibility = Visibility.Collapsed;
            re.Visibility = Visibility.Collapsed;
            nextbtn.Visibility = Visibility.Collapsed;
            resultGrid.Visibility = Visibility.Collapsed;
            currentQ = 1;
            score = 0;
            var i = 0;
            while(true) // elmult ejfel, ezt a szar kodot vagyok kepes irni
            {           // mukodik csak szarul nez ki
                try     // ez a 7. oraja hogy csinalom megallas nelkul
                {
                    if(quizContainer.Children[i] is Grid)
                    {
                        quizContainer.Children.RemoveAt(i);
                        i = 0;
                    }
                    i++;
                }
                catch
                {
                    break;
                }
            }
            MySqlCommand cmd = new("SELECT * FROM kerdes ORDER BY RAND() LIMIT 25", connection);
            DataTable qs = new();
            connection.Open();
            qs.Load(cmd.ExecuteReader());
            List<int> ids = new(qs.AsEnumerable().Select(x => x.Field<int>("id")));

            cmd.CommandText = $"SELECT * FROM valasz WHERE k_id IN ({string.Join(",", ids)}) ORDER BY RAND()";
            DataTable ans = new();
            ans.Load(cmd.ExecuteReader());
            connection.Close();
            foreach (DataRow dr in qs.Rows)
            {
                var grid = CreateQuestion(dr, ans.AsEnumerable().Where(x => x.Field<int>("k_id") == dr.Field<int>("id")).ToList());
                quizContainer.Children.Add(grid);
                Grid.SetColumn(grid, 1);
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if(nametb.Text != "")
            {
                connection.Open();
                MySqlCommand cmd = new($"INSERT INTO jatekos (nev) VALUES ('{nametb.Text}')", connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch { } // name already exists
                connection.Close();
                nameCont.Visibility = Visibility.Collapsed;
                startbtn.Visibility = Visibility.Collapsed;
                (quizContainer.Children[1] as Grid).Visibility = Visibility.Visible;
                (quizContainer.Children[1] as Grid).BeginAnimation(OpacityProperty, op1);
                startTime = DateTime.Now;
                curqlbl.Visibility = Visibility.Visible;
                re.Visibility = Visibility.Visible;
            }
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            nextbtn.Visibility = Visibility.Collapsed;
            (quizContainer.Children[currentQ] as Grid).BeginAnimation(OpacityProperty, op0);

            Task.Run(() =>
            {
                Thread.Sleep(500);
                Dispatcher.Invoke(() =>
                {
                    if (currentQ + 1 != 26)
                    {
                        (quizContainer.Children[currentQ] as Grid).Visibility = Visibility.Collapsed;
                        (quizContainer.Children[++currentQ] as Grid).Visibility = Visibility.Visible;
                        (quizContainer.Children[currentQ] as Grid).BeginAnimation(OpacityProperty, op1);
                        startTime = DateTime.Now;
                        curqlbl.Content = currentQ;
                    }
                    else
                    {
                        scorelbl.Content = score;
                        connection.Open();
                        MySqlCommand cmd = new($"INSERT INTO pontszam (jatekos_id, pont, date) VALUES ((SELECT id FROM jatekos WHERE nev = '{nametb.Text}'), {score}, NOW())", connection);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"Select pont, date FROM pontszam WHERE jatekos_id = (SELECT id FROM jatekos WHERE nev = '{nametb.Text}')";
                        DataTable dt = new();
                        dt.Load(cmd.ExecuteReader());
                        dg.ItemsSource = dt.AsDataView();
                        connection.Close();
                        resultGrid.Visibility = Visibility.Visible;
                        resultGrid.BeginAnimation(OpacityProperty, op1);
                        curqlbl.Visibility = Visibility.Collapsed;
                    }
                });
            });
        }

        private Grid CreateQuestion(DataRow dr, List<DataRow> answers)
        {
            Grid cont = new()
            {
                Visibility = Visibility.Collapsed,
                Opacity = 0
            };
            cont.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            });

            cont.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            });

            StackPanel QandImg = new();
            QandImg.Children.Add(new TextBlock()
            {
                Text = dr.Field<string>("kerdes")
            });
            QandImg.Children.Add(new Image()
            {
                Source = GetImage(dr.Field<byte[]>("kep")) 
            });

            StackPanel AnswersSp = new();
            foreach (DataRow answer in answers)
            {
                var correct = answer.Field<bool>("helyes");
                var anslbl = new Answer(answer.Field<string>("valasz"), correct);
                anslbl.Clicked += (s, e) =>
                {
                    nextbtn.Visibility = Visibility.Visible;
                    if (anslbl.Correct)
                    {
                        var delta = DateTime.Now - startTime;
                        if (delta.TotalSeconds <= 10) score += 1000;
                        else score += (int)Math.Max(1000 - ((Math.Floor(delta.TotalSeconds) - 10) * 50), 50);
                    }

                    scorelbl.Content = score;
                };
                AnswersSp.Children.Add(anslbl);
            }

            cont.Children.Add(QandImg);
            cont.Children.Add(AnswersSp);
            Grid.SetRow(AnswersSp, 1);
            return cont;
        }

        private static ImageSource GetImage(byte[] buffer)
        {
            BitmapImage bi = new();
            using (MemoryStream ms = new(buffer))
            {
                ms.Position = 0;
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
            }
            return bi;
        }
    }

    class Answer : Label
    {
        public bool Correct { get; set; }
        ColorAnimation enter;
        ColorAnimation leave;
        bool answered = false;
        public event EventHandler Clicked;

        public Answer(string content, bool correct)
        {
            Correct = correct;
            Content = content;
            Background = new SolidColorBrush(Colors.White);
            BorderBrush = new SolidColorBrush(Colors.Gray);
            enter = new(Color.FromRgb(210, 210, 210), TimeSpan.FromMilliseconds(250), FillBehavior.HoldEnd);
            leave = new(Color.FromRgb(255, 255, 255), TimeSpan.FromMilliseconds(250), FillBehavior.HoldEnd);
            PreviewMouseUp += Answer_PreviewMouseUp;
            MouseEnter += Answer_MouseEnter;
            MouseLeave += Answer_MouseLeave;
        }

        private void Answer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if(!answered)
            {
                foreach (var child in (Parent as StackPanel).Children.Cast<Answer>())
                {
                    if (child.Correct)
                    {
                        child.Background.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(Color.FromRgb(34, 153, 34), TimeSpan.FromMilliseconds(300)));
                        child.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(Color.FromRgb(34, 153, 34), TimeSpan.FromMilliseconds(300)));
                    }
                    child.answered = true;
                }
                if (!Correct)
                {
                    Background.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(Color.FromRgb(214, 43, 43), TimeSpan.FromMilliseconds(300)));
                    BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation(Color.FromRgb(214, 43, 43), TimeSpan.FromMilliseconds(300)));
                }
                Clicked.Invoke(this, e);
            }
        }

        private void Answer_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!answered)
                Background.BeginAnimation(SolidColorBrush.ColorProperty, enter);
        }

        private void Answer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!answered)
                Background.BeginAnimation(SolidColorBrush.ColorProperty, leave);
        }
    }
}