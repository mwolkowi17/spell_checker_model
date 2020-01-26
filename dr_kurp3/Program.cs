using System;
using System.Collections.Generic;
namespace Zakupy
{
    enum Rodzaj { spożywcze, komunikacja, kultura }
    class Zakup
    {
        public Rodzaj rodzaj { get; private set; }
        public Double wydatek { get; private set; }
        public Boolean zaplacono { get; set; }
        public Zakup(Rodzaj typ, double kwota)
        { rodzaj = typ; wydatek = kwota; zaplacono = false; }
        public void Zaplac() { zaplacono = true; }
        public override String ToString()
        {
            return String.Format("\n{0} {1:C} zapłacono: {2}",
          rodzaj, wydatek, zaplacono);
        }
    }
    class Ewidencja
    {
        private Queue<Zakup> zakupy = new Queue<Zakup>();
        public void DodajZakup(Rodzaj typ, double wydatek)
        { zakupy.Enqueue(new Zakup(typ, wydatek)); }
        public Zakup PierwszyDoZaplaty()
        {
            foreach (Zakup zakup in zakupy)
                if (!zakup.zaplacono) return zakup;
            return null;
        }
        public void PokazKolejke()
        {
            foreach (Zakup zakup in zakupy) Console.Write(zakup);
            Console.WriteLine();
        }
        public double RazemDoZaplaty()
        {
            double suma = 0;
            foreach (Zakup zakup in zakupy)
                
        if (!zakup.zaplacono) suma += zakup.wydatek;
            return suma;
        }
    }
    class Klient
    {
        private String login = "abc";
        private String haslo = "123";
        private Boolean zalogowany { get; set; } = false;
        public Zakup zakupObslugiwany { get; set; } = null;
        public void Zaloguj(String log, String pass)
        {
            if ((log == login) && (pass == haslo))
                zalogowany = true;
            else zalogowany = false;
        }
        public void Wyloguj() { zalogowany = false; }
        public void Kupuje(Ewidencja ewid, Rodzaj rodzaj, double kwota)
        {
            if (zalogowany) ewid.DodajZakup(rodzaj, kwota);
            else Console.WriteLine("Nie jesteś zalogowany");
        }
        public void Zaplac(Ewidencja ewid)
        {
            if (zalogowany) zakupObslugiwany = ewid.PierwszyDoZaplaty();
            if (zakupObslugiwany != null) zakupObslugiwany.Zaplac();
        }
        public void PokazKolejke(Ewidencja ewid) { ewid.PokazKolejke(); }
        public double RazemDoZaplaty(Ewidencja ewid)
        { return ewid.RazemDoZaplaty(); }
    }
    class Program
    {
        static void Main(string[] args)
        { //debugowanie
            Ewidencja grudzien = new Ewidencja();
            Klient Nowak = new Klient();
            Nowak.Zaloguj("abc", "123");
            //Nowak.Wyloguj();
            Nowak.Kupuje(grudzien, Rodzaj.spożywcze, 26);
            Nowak.Kupuje(grudzien, Rodzaj.kultura, 75);
            Nowak.PokazKolejke(grudzien);
            Console.WriteLine("Pozostało do zapłaty: " + Nowak.RazemDoZaplaty(grudzien));
            Nowak.Zaplac(grudzien);
            Nowak.PokazKolejke(grudzien);
            Console.WriteLine("Pozostało do zapłaty: " + Nowak.RazemDoZaplaty(grudzien));
            Nowak.Zaplac(grudzien);
            Nowak.PokazKolejke(grudzien);
            Console.WriteLine("Pozostało do zapłaty: " + Nowak.RazemDoZaplaty(grudzien));
            Console.ReadKey();
        }
    }
}