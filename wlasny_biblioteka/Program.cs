using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace wlasny_biblioteka
{
    class BibliotekaBaza: DbContext
    {
        public DbSet<Ksiazka>KsiazkiBaza { get; set; }
        public DbSet<Czytelnik>Czytelnicy { get; set; }
    }
    class Ksiazka
    {
        public int KsiazkaID { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string DataWydania { get; set; }
        public bool dostępna { get; set; }
        public Czytelnik Wypozyczajacy { get; set; }

        public Ksiazka (string tytul,string autor)
        {
            Tytul = tytul;
            Autor = autor;
        }
        public override string ToString()
        {
            return Tytul + " - " + Autor;
        }
    }
    class Czytelnik
    {
        public int CzytelnikID { get; set; }
        public string ImieNazisko { get; set; }
       // public DateTime DataZapisania { get; set; }
        public bool Zapisany { get; set; }


        public List<Ksiazka> wypozyczone { get; set; }
        public Queue<Ksiazka> historia_wypozyczen { get; set; }
        public Czytelnik()
        {

        }
        public Czytelnik(int id, string imienazwisko)
        {
            ImieNazisko = imienazwisko;
            wypozyczone = new List<Ksiazka>();
            historia_wypozyczen = new Queue<Ksiazka>();
            CzytelnikID = id;
        }

        public override string ToString()
        {

            if (Zapisany == true)
                return "Czytelnik: " + ImieNazisko + " zapisany do Biblioteki";
            return "Czytelnik: " + ImieNazisko + " niezapisany do Biblioteki";
        }
    }

    class Biblioteka
    {
        public int IloscKsiazek;

        public List<Ksiazka> ksiegozbior;
        public List<Czytelnik> listaczytelnikow;
        public Biblioteka()
        {
            ksiegozbior = new List<Ksiazka>();
            listaczytelnikow = new List<Czytelnik>();
        }

        public void DodajKsiazke( string tytul, string autor)
        {
            Ksiazka nowa = new Ksiazka(tytul, autor);
            ksiegozbior.Add(nowa);
            IloscKsiazek++;
            nowa.dostępna = true;
        }

        public void UsunKsiazke(string tytul)
        {
            foreach (var n in ksiegozbior)
            {
                if (n.Tytul == tytul)
                {
                    ksiegozbior.Remove(n);
                }
            }
            IloscKsiazek--;
        }

        public void Wypozycz(string tytul,string wypozycajacyA)
        {
            Ksiazka ksiazkaSelect = (from Ksiazka ksiazka in ksiegozbior
                                where ksiazka.Tytul == tytul
                                select ksiazka).First();
            ksiazkaSelect.dostępna = false;
            Czytelnik czytelnikSelect = (from Czytelnik czytelnik in listaczytelnikow
                                         where czytelnik.ImieNazisko == wypozycajacyA
                                         select czytelnik).First();
            ksiazkaSelect.Wypozyczajacy = czytelnikSelect;
            czytelnikSelect.wypozyczone.Add(ksiazkaSelect);
            czytelnikSelect.historia_wypozyczen.Enqueue(ksiazkaSelect);
        }

        public void Zwroc(string tytul)
        {
            
            Ksiazka ksiazkaSelect = (from Ksiazka ksiazka in ksiegozbior
                                     where ksiazka.Tytul == tytul
                                     select ksiazka).First();
            

            foreach(var n in ksiazkaSelect.Wypozyczajacy.wypozyczone)
            {
                if (n.Tytul == tytul)
                {
                    ksiazkaSelect.Wypozyczajacy.wypozyczone.Remove(n);
                    ksiazkaSelect.dostępna = true;
                    ksiazkaSelect.Wypozyczajacy = null;
                    return;
                }
            }
           

           
        }

        public void DodajCzytelnika (Czytelnik czytelnik)
        {
            listaczytelnikow.Add(czytelnik);
            czytelnik.Zapisany = true;
        }

        public void WykreslCzytelnika (Czytelnik czytelnik)
        {
            listaczytelnikow.Remove(czytelnik);
            czytelnik.Zapisany = false;
        }
    }

    enum Rodzaj
    {
        Komiks,
        Powiesc,
        Biografia
    }
    class Program
    {
        static void Main(string[] args)
        {
            using(BibliotekaBaza context=new BibliotekaBaza()) { 
            Czytelnik Marcin = new Czytelnik(1,"Marcin Wołlkowicz");
            Czytelnik Ryba = new Czytelnik(2,"Iwona Wołkowicz");
            Czytelnik Franek = new Czytelnik(3,"Franek Wołkowicz");
              //  Marcin.CzytelnikID = 1;
               // Ryba.CzytelnikID = 2;
              //  Franek.CzytelnikID = 3;
               // context.Czytelnicy.Add(Marcin);
              //  context.Czytelnicy.Add(Ryba);
             //   context.Czytelnicy.Add(Franek);
              //  context.SaveChanges();
            Console.WriteLine(  Marcin);
            Console.WriteLine(Ryba);
            Console.WriteLine(Franek);
            Console.WriteLine("===========================");
            Biblioteka biblioteka = new Biblioteka();
            
            biblioteka.DodajCzytelnika(Marcin);
            biblioteka.DodajCzytelnika(Ryba);
            biblioteka.DodajCzytelnika(Franek);
                var zBazy = new List<Czytelnik>();
                
                 zBazy = context.Czytelnicy.ToList();
                Console.WriteLine("===========================");
                Console.WriteLine(zBazy[2].ImieNazisko);
                
                    
            foreach (Czytelnik n in biblioteka.listaczytelnikow)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("===================================");

            biblioteka.DodajKsiazke("Ogniem i mieczem", "Sienkiewicz");
            biblioteka.DodajKsiazke("Winnetou", "May");
            biblioteka.DodajKsiazke("Narrenturn", "Sapkowski");

            foreach(var n in biblioteka.ksiegozbior)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("================");
            biblioteka.Wypozycz("Ogniem i mieczem", "Marcin Wołlkowicz");
            biblioteka.Wypozycz("Winnetou", "Marcin Wołlkowicz");

            foreach(var n in biblioteka.ksiegozbior)
            {
                if (n.dostępna == false)
                {
                    Console.WriteLine(n.Tytul + " - " + n.Wypozyczajacy.ImieNazisko);
                }
                
            }
            Console.WriteLine("===============================po zwrocie");
            biblioteka.Zwroc("Ogniem i mieczem");
            foreach (var n in biblioteka.ksiegozbior)
            {
                if (n.dostępna == false)
                {
                    Console.WriteLine(n.Tytul + " - " + n.Wypozyczajacy.ImieNazisko);
                }

            }
            Console.WriteLine("==========================once again");
            biblioteka.Wypozycz("Narrenturn", "Marcin Wołlkowicz");
            foreach (var n in biblioteka.ksiegozbior)
            {
                if (n.dostępna == false)
                {
                    Console.WriteLine(n.Tytul + " - " + n.Wypozyczajacy.ImieNazisko);
                }

            }
            Console.ReadKey();
            }
        }
    }
}
