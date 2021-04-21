using System;
using System.Collections.Generic;
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
using System.Threading;

namespace VerificaAsincronoGramellini
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r;
        List<Char> lettere;
        string parolaEstratta;
        public MainWindow()
        {
            InitializeComponent();
            r = new Random();
            AggiungiLettereALista();
            EstraiLettera();
        }

        public async void EstraiLettera()
        {
            await Task.Run(() =>
            {
                char ultimaLetteraEstratta;
                while (true)
                {
                    Thread.Sleep(300);
                    ultimaLetteraEstratta = lettere[r.Next(0, 10)];
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lblLettera.Content = ultimaLetteraEstratta;
                    }));
                }

            });
        }
        public void AggiungiLettereALista()
        {
            lettere = new List<char>();
            lettere.Add('A');
            lettere.Add('G');
            lettere.Add('H');
            lettere.Add('S');
            lettere.Add('J');
            lettere.Add('X');
            lettere.Add('E');
            lettere.Add('T');
            lettere.Add('O');
            lettere.Add('I');
        }

        private void btnEstrai_Click(object sender, RoutedEventArgs e)
        {
            parolaEstratta += lblLettera.Content;
            lblParolaEstratta.Content = parolaEstratta;
        }
    }
}
