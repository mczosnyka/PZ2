    using System;
    using System.Collections.Generic;
using System.Reflection;
using System.Threading;
    public class Producent{
        public int numer_porzadkowy {get; set;}
        public int timestamp {get; set;}
        public bool running {get; set;}
        public Producent(int nr, int timestamp_){
            numer_porzadkowy = nr;
            timestamp = timestamp_;
            running = true;
        }
        public void Watek(){
            while (running)
            {
            Data dane = new Data(numer_porzadkowy);
            Program.datalist.Add(dane);
            Thread.Sleep(timestamp);
            }
        }
        
    }

    public class Konsument{
        public int numer_porzadkowy {get; set;}
        public int timestamp {get; set;}
        Dictionary<string, int> consumed = new Dictionary<string, int>();
        public bool running {get; set;}
        public Konsument(int nr, int timestamp_, int n){
            numer_porzadkowy = nr;
            timestamp = timestamp_;
            for(int i=0;i<n;i++){
                string stringer = "producent" + " " + i;
                consumed.Add(stringer, 0);
            }
            running = true;
        }
        public void PrintDict(){
            Console.WriteLine("KONSUMENT NUMER " + numer_porzadkowy);
            foreach (var tr in consumed)
            {
                Console.WriteLine($"{tr.Key} - {tr.Value}");
            }
        }

        public void Watek(){
            while (running)
            {
            Data dane = null;
            if (Program.datalist.Count > 0)
            {
                dane = Program.datalist[0];
                Program.datalist.RemoveAt(0);
            }
            if(dane!=null){
                string stringer = "producent" + " " + dane.numer_porzadkowy;
                consumed[stringer] += 1;
            }
            Thread.Sleep(timestamp);
            }
        }
    }

    public class Data{
        public int numer_porzadkowy {get; set;}
        public Data(int nr){
            numer_porzadkowy = nr;
        }
    }

    public class Program{
        public static List<Data> datalist = new List<Data>();
        static void Main(string[] args){
        Console.WriteLine("podaj liczbe producentow");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine("podaj liczbe konsumentow");
        int m = int.Parse(Console.ReadLine());
        Dictionary<Konsument, Thread> konsumenci = new Dictionary<Konsument, Thread>();
        Dictionary<Producent, Thread> producenci = new Dictionary<Producent, Thread>();
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            int t = random.Next(1000, 3000);
            Producent producent = new Producent(i, t);
            Thread producentThread = new Thread(producent.Watek);
            producenci.Add(producent, producentThread);
            producentThread.Start();
        }
        for (int i = 0; i < m; i++)
        {
            int t = random.Next(1000, 3000);    
            Konsument konsument = new Konsument(i, t, n);
            Thread konsumentThread = new Thread(konsument.Watek);
            konsumenci.Add(konsument, konsumentThread);
            konsumentThread.Start();
        }
        while (Console.ReadKey().Key != ConsoleKey.Q);
        foreach (var kn in konsumenci)
            {
                kn.Key.running = false;
                kn.Key.PrintDict();

            }
        foreach (var pd in producenci)
        {
            pd.Key.running = false;
        }
        }   
        








    }
