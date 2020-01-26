using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slowa_Learn
{
    class Item
    {
        public string Word_Pol { get; set; }
        public string Word_Eng { get; set; }

        public Item()
        {

        }

        public Item(string word_pol, string word_eng)
        {
            Word_Pol = word_pol;
            Word_Eng = word_eng;
        }

        public override string ToString()
        {
            return Word_Pol + " - " + Word_Eng;

        }
    }

    class Dictionary
    {
        public static List<Item> dictionary = new List<Item>();

        public void Add_To_Dictionary(string word_pol, string word_eng)
        {
            Item item = new Item(word_pol, word_eng);
            dictionary.Add(item);
        }

        public void Remove_From_Dictionary (string word_pol)
        {
            Item item = (from Item n in dictionary
                         where n.Word_Pol == word_pol
                         select n).First();
            dictionary.Remove(item);
        }

        public void Change_Item_Dictionary (string word_pol, string new_word_eng)
        {
            Item item = (from Item n in dictionary
                         where n.Word_Pol == word_pol
                         select n).First();
            item.Word_Eng = new_word_eng;
        }

        public void Dictionary_Present()
        {
            foreach( Item n in dictionary)
            {
                Console.WriteLine(n.Word_Eng+" + "+n.Word_Pol);
            }
        }
    }

    class Pupil
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Pupil()
        {

        }

        public Pupil( string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public List<Test> test_results = new List<Test>();

        
    }

    class Test
    {
        public Pupil Tested_Person { get; set; }
        public int Number_of_Items { get; set; }
        public int Number_of_Good_Answers { get; set; }
        public DateTime Test_Date { get; set; }

        //These is a place for method implementing single test

        public Test (int number_of)
        {
            Number_of_Items = number_of;
           

            for (int n = 0; n < number_of; n++)
            {
                Random random = new Random();
                int testnum = random.Next(Dictionary.dictionary.Count);
                Item roboczy = Dictionary.dictionary[testnum];
                Console.Write(roboczy.Word_Pol+" - ");
                string answer = Console.ReadLine();
                if (answer == roboczy.Word_Eng)
                {
                    Console.WriteLine("good answer");
                    Number_of_Good_Answers++;
                }
                else
                {
                    Console.WriteLine("wrong answer");
                    Console.WriteLine("good awnswer is: "+roboczy.Word_Eng);
                }
               
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Dictionary slownik = new Dictionary();
            slownik.Add_To_Dictionary("mrowka", "ant");
            slownik.Add_To_Dictionary("pokoj", "room");
            slownik.Add_To_Dictionary("dom", "home");

            Pupil lucja = new Pupil();
            Test nowy = new Test(4);
            lucja.test_results.Add(nowy);
            Console.WriteLine(lucja.test_results[0].Number_of_Good_Answers + " "+lucja.test_results[0].Number_of_Items);
            Console.ReadKey();
        }
    }
}
