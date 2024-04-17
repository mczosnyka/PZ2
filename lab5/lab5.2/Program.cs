public class Program{
    static bool threadRunning = true;
    static void Main(string[] args)  
    {
        Console.WriteLine("podaj sciezke do katalogu");
        string path = Console.ReadLine();
        List<string> filesAndDirectories = GetFilesAndDirectories(path);
        Thread monitoringThread = new Thread(() => ChangesDetector(filesAndDirectories, path));
        monitoringThread.Start();
        while (Console.ReadKey().Key != ConsoleKey.Q) ;
        threadRunning = false;
    }

    static List<String> GetFilesAndDirectories(string path){
        List<string> filesAndDirectories = new List<string>();
        string [] fileEntries = Directory.GetFiles(path);
        string[] directories = Directory.GetDirectories(path);
        foreach(string str in fileEntries){
            filesAndDirectories.Add(str);
        }
        foreach(string str in directories){
            filesAndDirectories.Add(str);
        }
        return filesAndDirectories;
    }

    static void ChangesDetector(List<String> files, string path){
        while (threadRunning)
        {
            List<string> currentFiles = GetFilesAndDirectories(path);

            foreach (string file in currentFiles)
            {
                if (!files.Contains(file))
                {
                    Console.WriteLine($"Dodano plik: {file}");
                }
            }

            // Sprawdzenie czy pliki zostały usunięte
            foreach (string file in files)
            {
                if (!currentFiles.Contains(file))
                {
                    Console.WriteLine($"Usunięto plik: {file}");
                }
            }

            // Aktualizacja listy plików
            files = currentFiles;

            // Poczekaj 1 sekundę przed kolejnym sprawdzeniem
            Thread.Sleep(1000);
        }
    }
    } 