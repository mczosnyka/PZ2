using Microsoft.VisualBasic;

public class Program{
    static bool threadRunning = true;
    static Dictionary<Thread, bool> threads = new Dictionary<Thread, bool>();
    static void Main(string[] args)  
    {
        Console.WriteLine("podaj liczbe watkow");
        int n = int.Parse(Console.ReadLine());
        for(int j = 0; j<n; j++){
            Thread th = new Thread(() => Threader());
            threads.Add(th, false);
            th.Start();
        }
        if(threads.Count==n){
            int z = 0;
            foreach(var thr in threads){
                if(thr.Value == false){
                    break;
                }
                z ++;
            }
            if(z == n){
                Console.WriteLine("All threads are running");
            }
            z = 0;
            threadRunning = false;
            while(true){
                foreach(var thr in threads){
                if(thr.Value == true){
                    break;
                }
                z++;
            }
            if(z == n){
                Console.WriteLine("No threads are running");
                break;
            }
            }

        }
    }

    static void Threader(){
        threads[Thread.CurrentThread] = true;
        while (threadRunning)
        {
            Thread.Sleep(1000); 
        }
        threads[Thread.CurrentThread] = false;
    }
    }
