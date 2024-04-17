using System.Diagnostics;
using lab6._3;

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
                
                
                Console.WriteLine("Wpisz tresc wiadomosci do serwera, jesli chcesz przestac wpisz !end");
                string ?wiad = Console.ReadLine();
                while (wiad is null)
                {
                    Console.WriteLine("Wprowadz poprawna wiadomosc.");
                    wiad = Console.ReadLine();
                }

                if (wiad == "!end")
                {
                    client.SendMessage("!end");
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