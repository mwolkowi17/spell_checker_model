using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Magazyn_system
{
    enum Okladka
    {
        pudelkoPojedycze, pudelkoPodwojne, pudelkoPoczworne
    }

    class MagazynEntities : DbContext
    {
        public DbSet<Film>filmbaza { get; set; }
        public DbSet<Aplikacja>aplikacjabaza { get; set; }
    }


    abstract class Product
    {
        public string Tytul { get; set; }
        public int RokProdukcji { get; set; }
        public int IloscOkladek { get; set; }
        public bool Dostepnosc { get; set; }
        public Okladka Opakownie { get; set; }

        public Product()
        {

        }

        public Product(string Tytul, int RokProdukcji, Okladka Opakownie)
        {

        }
    }

    class Film : Product
    {
        public int FilmID { get; set; }
        public int StanMagazynuTytul { get; set; }
        
        public Film():base()
        {

        }

        public Film(string tytul, int rokProdukcji, Okladka opakowanie) : base(tytul, rokProdukcji, opakowanie)
        {
            Tytul = tytul;
            RokProdukcji = rokProdukcji;

            Opakownie = opakowanie;
        }

        public Film(int id,string tytul, int rokProdukcji, Okladka opakowanie) : base(tytul, rokProdukcji, opakowanie)
        {
            FilmID = id;
            Tytul = tytul;
            RokProdukcji = rokProdukcji;

            Opakownie = opakowanie;
        }


        public override string ToString()
        {
            return Tytul + " ilość okładek: " + IloscOkladek;


        }
    }

    class Aplikacja : Product
    {
        public int AplikacjaID { get; set; }
        public int StanMagazynuTytul;
        public Aplikacja(string tytul, int rokProdukcji, Okladka opakowanie) : base(tytul, rokProdukcji, opakowanie)
        {
            Tytul = tytul;
            RokProdukcji = rokProdukcji;

            Opakownie = opakowanie;
        }

        public override string ToString()
        {
            return Tytul + " ilość okładek: " + IloscOkladek;
        }
    }

    class Magazyn
    {
        public static int Stan_Magazynu_Filmy { get; set; }
        public static int Stan_Magazynu_Aplikacje { get; set; }
        public int IloscOpakowanPojed { get; set; }
        public int IloscOpakowanPodw { get; set; }
        public int IloscOpakowanPoczw { get; set; }
        public int IloscPlyt;
        public List<Film> filmy = new List<Film>();
        public List<Aplikacja> aplikacje = new List<Aplikacja>();
        public Queue<Product> katalog_produktow = new Queue<Product>();

        public void DodajFilm(string tytul, int rok, Okladka rodzajokl)
        {
            Film film = new Film(tytul, rok, rodzajokl);

            katalog_produktow.Enqueue(film);
            
            filmy.Add(film);
        }
        public Film DodajFilmObiekt(string tytul, int rok, Okladka rodzajokl)
        {
            Film film = new Film(tytul, rok, rodzajokl);

            katalog_produktow.Enqueue(film);

            filmy.Add(film);
            return film;
        }
        public void DodajAplikacje(string tytul, int rok, Okladka rodzajokl)
        {
            Aplikacja aplikacja = new Aplikacja(tytul, rok, rodzajokl);
            katalog_produktow.Enqueue(aplikacja);
            aplikacje.Add(aplikacja);
        }

        public void DodajOkladkiFilm(string tytul, int ilosc) // można by jeszcze dodać rodzaj opakowania
        {
            Film roboczy = (from Film item in filmy
                            where item.Tytul == tytul
                            select item).First();
            if (roboczy != null)
            {
                roboczy.IloscOkladek += ilosc;
                roboczy.Dostepnosc = true;
                roboczy.StanMagazynuTytul += ilosc;
                Magazyn.Stan_Magazynu_Filmy += ilosc;
            }
            else
            {
                Console.WriteLine("Nie ma takiego tytułu");
            }
        }

        public Film DodajOkladkiFilmObiekt(string tytul, int ilosc) // można by jeszcze dodać rodzaj opakowania
        {
            Film roboczy = (from Film item in filmy
                            where item.Tytul == tytul
                            select item).First();
            if (roboczy != null)
            {
                roboczy.IloscOkladek += ilosc;
                roboczy.Dostepnosc = true;
                roboczy.StanMagazynuTytul += ilosc;
                Magazyn.Stan_Magazynu_Filmy += ilosc;
            }
            else
            {
                Console.WriteLine("Nie ma takiego tytułu");
            }
            return roboczy;
        }

        public void DodajOkladkiAplikacja(string tytul, int ilosc)
        {
            Aplikacja roboczy = (from Aplikacja item in aplikacje
                                 where item.Tytul == tytul
                                 select item).First();
            if (roboczy != null)
            {
                roboczy.IloscOkladek += ilosc;
                roboczy.Dostepnosc = true;
                roboczy.StanMagazynuTytul += ilosc;
                Magazyn.Stan_Magazynu_Aplikacje += ilosc;
            }
            else
            {
                Console.WriteLine("Nie ma takiego tytułu");
            }
        }

        public void StanMagazynu()
        {
            foreach (var n in filmy)
            {
                Console.WriteLine(n);
            }
            foreach (var n in aplikacje)
            {
                Console.WriteLine(n);
            }
        }

        public void PokazDostepnosc()
        {
            foreach (var n in katalog_produktow)
            {
                if (n.Dostepnosc == true)
                {
                    Console.WriteLine(n);
                }
            }
        }

        public void ZakupOpakowan(Okladka typ,int ilosc)
        {
            if (typ == Okladka.pudelkoPojedycze)
            {
                IloscOpakowanPojed += ilosc;
            }
            if (typ == Okladka.pudelkoPodwojne)
            {
                IloscOpakowanPodw += ilosc;
            }
            if (typ == Okladka.pudelkoPoczworne)
            {
                IloscOpakowanPoczw += ilosc;
            }

            Console.WriteLine($"Ilość pojedynczych: {IloscOpakowanPojed}, Ilość podwójnych: {IloscOpakowanPodw}, Ilość poczwórnych: {IloscOpakowanPoczw}.");
        }

        public void ZakupPlyt(int ilosc)
        {
            IloscPlyt += ilosc;
            Console.WriteLine($"Ilość płyt: {IloscPlyt}");
        }

        public void ZamowienieFilm(string tytul, int ilosc)
        {
           Film roboczy = (from Film item in filmy
                              where item.Tytul == tytul
                              select item).Single();
            if(roboczy.IloscOkladek>0 && roboczy.Dostepnosc == true&& roboczy.StanMagazynuTytul>0 ) 
            
            {
                roboczy.StanMagazynuTytul -= ilosc;
                roboczy.IloscOkladek -= ilosc;
                IloscPlyt -= ilosc;
                IloscOpakowanPojed -= ilosc;
                if (roboczy.StanMagazynuTytul == 0)
                {
                    roboczy.Dostepnosc = false;
                }
            }

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            using (MagazynEntities context = new MagazynEntities())
            {
                Magazyn magazyn = new Magazyn();
                
               magazyn.filmy = context.filmbaza.ToList();
               // var bazarob= context.filmbaza.ToList();

                magazyn.DodajFilm("Przemoc", 2017, Okladka.pudelkoPojedycze);
              
                magazyn.DodajFilm("Asertywnosc", 2018, Okladka.pudelkoPojedycze);
                magazyn.DodajFilm("Bulling", 2017, Okladka.pudelkoPojedycze);
                magazyn.DodajFilm("Hejt", 2017, Okladka.pudelkoPojedycze);
                magazyn.DodajAplikacje("Dopalacze", 2014, Okladka.pudelkoPoczworne);
                //context.filmbaza.Add(magazyn.DodajFilmObiekt("Oni", 2015, Okladka.pudelkoPojedycze));
                //context.filmbaza.Add(magazyn.DodajOkladkiFilmObiekt("Oni", 22));
                //context.filmbaza.Add(magazyn.DodajOkladkiFilmObiekt("Oni", 22));
                //context.filmbaza.Append(magazyn.DodajOkladkiFilmObiekt("Oni", 22)); // zmienia wartości bazy !!
                context.SaveChanges();
                magazyn.DodajOkladkiFilm("Hejt", 40);
                magazyn.DodajOkladkiFilm("Przemoc", 20);
                magazyn.DodajOkladkiAplikacja("Dopalacze", 10);
                magazyn.DodajOkladkiFilm("Hejt", 60);

                magazyn.StanMagazynu();
                Console.WriteLine("=====dostępne========");
                magazyn.PokazDostepnosc();
                magazyn.ZakupOpakowan(Okladka.pudelkoPojedycze, 40);
                magazyn.ZakupOpakowan(Okladka.pudelkoPoczworne, 20);
                magazyn.ZakupPlyt(100);
                magazyn.ZamowienieFilm("Hejt", 12);
                magazyn.PokazDostepnosc();

                Console.ReadKey();
            }
    }
    }
}
