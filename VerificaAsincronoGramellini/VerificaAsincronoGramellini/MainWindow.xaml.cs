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
        List<string> paroleEstratte;
        int lunghezzaParoleDaEstrarre;
        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Benvenuto! Per estrarre solo parole lunghe N si prega di inserire un valore maggiore di 0 nella text box in alto a sinistra, se invece verrà inserito 0 o verrà lasciato vuoto, il programma funzionerà in modalità base.");
            r = new Random();
            parolaEstratta = "";
            lunghezzaParoleDaEstrarre = 0; //parte da 0, e quando è zero le parole estratte non finiranno nella lista ma rimarrà funzionante solo la parte a destra dell'interfaccia
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

        private void btnEstrai_Click(object sender, RoutedEventArgs e)
        {
            parolaEstratta += lblLettera.Content;
            lblParolaEstratta.Content = parolaEstratta;

            if (lunghezzaParoleDaEstrarre > 0 && lunghezzaParoleDaEstrarre == parolaEstratta.Length) //faccio finire le parole nella lista
            {
                lblParolaEstratta.Content = "";
                lstParole.Items.Add(parolaEstratta);
                parolaEstratta = "";
            }
            else
            {
                lblParolaEstratta.Content = parolaEstratta;
            }
        }

        private void btnLunghezzaParola_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(txtLunghezzaParolaDaEstrarre.Text) > 0) //accetto la lunghezza
                {
                    paroleEstratte = new List<string>();
                    lunghezzaParoleDaEstrarre = int.Parse(txtLunghezzaParolaDaEstrarre.Text);
                    if (parolaEstratta.Length == lunghezzaParoleDaEstrarre)
                    {
                        paroleEstratte.Add(parolaEstratta);
                        lstParole.Items.Add(parolaEstratta);
                        lblParolaEstratta.Content = "";
                        parolaEstratta = "";
                    }
                    else if(parolaEstratta.Length > lunghezzaParoleDaEstrarre) //resetto la parola estratta perchè troppo lunga
                    {
                        lblParolaEstratta.Content = "";
                        parolaEstratta = "";
                        MessageBox.Show("Parola base resettata!");
                    }
                }
                else
                {
                    txtLunghezzaParolaDaEstrarre.Text = "";
                    MessageBox.Show("inserire un valore maggiore di 0 se si vuole definire una lunghezza delle parole da estrarre.");
                    lunghezzaParoleDaEstrarre = 0; //faccio funzionare solo la parte base del programma
                }
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
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
    }
}
