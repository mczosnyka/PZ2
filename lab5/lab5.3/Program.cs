using Microsoft.VisualBasic;

public class Program{
    static string matchingfile;
    static void Main(string[] args)  
    {
        Console.WriteLine("podaj nazwe do wyszukania");
        string matcher = Console.ReadLine();
        Console.WriteLine("podaj sciezke do katalogu bazowego");
        string path = Console.ReadLine();
        Thread finder = new Thread(() => Finder(matcher, path));
        finder.Start();
        while(matchingfile is null){
            Thread.Sleep(100);
        }
        Console.WriteLine("znaleziono plik: " + matchingfile);
    }

    static void Finder(string match, string path){
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            if(file.Contains(match)){
                matchingfile = file;
                break;
            }
        }
        if(matchingfile is null){
            string[] dirs = Directory.GetDirectories(path);
            foreach(string dir in dirs){
                Thread finder = new Thread(() => Finder(match, dir));
                finder.Start();
            }
        }
    }
    }
