using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using WpfNaplo;
using static System.Net.Mime.MediaTypeNames;

namespace WpfNaplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fajlNev = "naplo.csv";
        //Így minden metódus fogja tudni használni.
        List<Osztalyzat> jegyek = new List<Osztalyzat>();

        public MainWindow()
        {
            InitializeComponent();
            // todo Fájlok kitallózásával tegye lehetővé a naplófájl kiválasztását!
            // Ha nem választ ki semmit, akkor "naplo.csv" legyen az állomány neve. A későbbiekben ebbe fog rögzíteni a program.

            // todo A kiválasztott naplót egyből töltse be és a tartalmát jelenítse meg a datagrid-ben!
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            //todo Ne lehessen rögzíteni, ha a következők valamelyike nem teljesül!
            // a) - A név legalább két szóból álljon és szavanként minimum 3 karakterből!
            //      Szó = A szöközökkel határolt karaktersorozat.
            // b) - A beírt dátum újabb, mint a mai dátum

            //todo A rögzítés mindig az aktuálisan megnyitott naplófájlba történjen!


            //A CSV szerkezetű fájlba kerülő sor előállítása
            string csvSor = $"{txtNev.Text.Split(' ')[0]};{txtNev.Text.Split(' ')[1]};{datDatum.Text};{cboTantargy.Text};{sliJegy.Value}";
            //Megnyitás hozzáfűzéses írása (APPEND)
            StreamWriter sw = new StreamWriter(fajlNev, append: true);
            sw.WriteLine(csvSor);
            sw.Close();
            jegyek.Add(new Osztalyzat(txtNev.Text.Split(' ')[0], txtNev.Text.Split(' ')[1], datDatum.Text, cboTantargy.Text, Convert.ToInt32(sliJegy.Value)));
            dgJegyek.ItemsSource = jegyek;
            int jegyosszeg = 0;
            foreach (Osztalyzat osztalyzat in jegyek)
            {
                jegyosszeg += osztalyzat.Jegy;
            }
            lblAtlag.Content = (jegyosszeg * 1.0) / (jegyek.Count * 1.0);
            lblJegyek.Content = jegyek.Count;
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dir = new OpenFileDialog();
            if (dir.ShowDialog() == true)
            {
                jegyek.Clear();  //A lista előző tartalmát töröljük
                StreamReader sr = new StreamReader(dir.FileName); //olvasásra nyitja az állományt
                while (!sr.EndOfStream) //amíg nem ér a fájl végére
                {
                    string[] mezok = sr.ReadLine().Split(";"); //A beolvasott sort feltördeli mezőkre
                                                               //A mezők értékeit felhasználva létrehoz egy objektumot
                    Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], mezok[3], int.Parse(mezok[4]));
                    jegyek.Add(ujJegy); //Az objektumot a lista végére helyezi
                }
                sr.Close(); //állomány lezárása
                int jegyosszeg = 0;
                foreach (Osztalyzat osztalyzat in jegyek)
                {
                    jegyosszeg += osztalyzat.Jegy;
                }
                lblAtlag.Content = (jegyosszeg * 1.0) / (jegyek.Count * 1.0);
                lblHely.Content = dir.FileName;
                lblJegyek.Content = jegyek.Count;
                
                //A Datagrid adatforrása a jegyek nevű lista lesz.
                //A lista objektumokat tartalmaz. Az objektumok lesznek a rács sorai.
                //Az objektum nyilvános tulajdonságai kerülnek be az oszlopokba.
                dgJegyek.ItemsSource = jegyek;
            }
        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegy.Content = sliJegy.Value; //Több alternatíva van e helyett! Legjobb a Data Binding!
        }

        //todo Felület bővítése: Az XAML átszerkesztésével biztosítsa, hogy láthatóak legyenek a következők!
        // - A naplófájl neve
        // - A naplóban lévő jegyek száma
        // - Az átlag

        //todo Új elemek frissítése: Figyeljen rá, ha új jegyet rögzít, akkor frissítse a jegyek számát és az átlagot is!

        //todo Helyezzen el alkalmas helyre 2 rádiónyomógombot!
        //Feliratok: [■] Vezetéknév->Keresztnév [O] Keresztnév->Vezetéknév
        //A táblázatban a név azserint szerepeljen, amit a rádiónyomógomb mutat!
        //A feladat megoldásához használja fel a ForditottNev metódust!
        //Módosíthatja az osztályban a Nev property hozzáférhetőségét!
        //Megjegyzés: Felételezzük, hogy csak 2 tagú nevek vannak
    }
}