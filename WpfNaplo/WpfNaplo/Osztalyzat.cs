using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNaplo
{
    public class Osztalyzat
    {
        String nev;
        String datum;
        String tantargy;
        string csaladNev;
        int jegy;

        public Osztalyzat(string csaladNev, string nev, string datum, string tantargy, int jegy)
        {
            this.csaladNev = csaladNev;
            this.datum = datum;
            this.tantargy = tantargy;
            this.jegy = jegy;
            this.nev = nev;
        }

        public string ForditottNev()
        {
            return $"{nev} {csaladNev}";
        }

        public string CsaladiNev { get => csaladNev; }

        public string Nev { get => nev; }
        public string Datum { get => datum; }
        public string Tantargy { get => tantargy; }
        public int Jegy { get => jegy; }
    }
    //todo Bővítse az osztályt! Készítsen CsaladiNev néven property-t, ami a névből a családi nevet adja vissza. Feltételezve, hogy a névnek csak az első tagja az.

    //todo Készítsen metódust ForditottNev néven, ami a két tagból álló nevek esetén megfordítja a névtagokat. Pld. Kiss Ádám => Ádám Kiss
}