using System.Diagnostics;
using lab6._1;

class Program
{
    static void Main(string[] args)
    {
        Thread serverThread = new Thread(() =>
        {
            Server server = new Server();
            server.Start();
        });
        serverThread.Start();
        
        Thread clientThread = new Thread(() =>
        {
            Client client = new Client();
            while (true)
            {
                
                
                Console.WriteLine("Wpisz tresc wiadomosci do serwera, jesli chcesz przestac wpisz 0");
                string ?wiad = Console.ReadLine();
                while (wiad is null)
                {
                    Console.WriteLine("Wprowadz poprawna wiadomosc.");
                    wiad = Console.ReadLine();
                }

                if (wiad == "0")
                {
                    break;
                }
                client.SendMessage(wiad);
                client.ReceiveMessage();
            }
            client.Stop();
        });
        clientThread.Start();
        
        serverThread.Join();
        clientThread.Join();
        
    }
}