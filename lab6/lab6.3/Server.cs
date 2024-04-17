namespace lab6._3;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private Socket socketSerwera { get; set; }
    private IPAddress ipAddress { get; set; }
    private IPEndPoint localEndPoint { get; set; }
    private string my_dir { get; set; }

    public Server()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        ipAddress = host.AddressList[0];
        localEndPoint = new IPEndPoint(ipAddress, 12345);
        socketSerwera = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        my_dir = Directory.GetCurrentDirectory();
    }

    public void Start()
    {
        socketSerwera.Bind(localEndPoint);
        socketSerwera.Listen(100);
        Socket socketKlienta = socketSerwera.Accept();
        HandleClient(socketKlienta);
    }
    
    
    public void HandleClient(Socket socketKlienta)
    {
        try
        {
            while (true)
            {
                byte[] bufor = new byte[4096];
                int received = socketKlienta.Receive(bufor, SocketFlags.None);
                String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
                if (wiadomoscKlienta == "list")
                {
                    string[] files = Directory.GetFiles(my_dir);
                    string response = "Aktualna sciezka: " + my_dir + "\nLista plików i folderów:" + "\n";
                    foreach (string file in files)
                    {
                        string formated = Path.GetFileName(file);
                        response += (formated + "\n");
                    }

                    string[] directories = Directory.GetDirectories(my_dir);
                    foreach (string directory in directories)
                    {
                        string formated = Path.GetFileName(directory);
                        response += (formated + "\n");
                    }

                    var echoBytes = Encoding.UTF8.GetBytes(response);
                    socketKlienta.Send(echoBytes, 0);
                }

                else if (wiadomoscKlienta == "!end")
                {
                    Console.WriteLine("Koniec pracy serwera");
                    Stop();
                    Environment.Exit(0); 
                }
                else if (wiadomoscKlienta.Contains("in"))
                {
                    string newpath = my_dir;
                    if (wiadomoscKlienta.Substring(3) == "..")
                    {
                        newpath = Path.GetDirectoryName(my_dir);
                    }
                    else if (wiadomoscKlienta.Substring(3) != "..")
                    {
                         newpath+= ("\\" + wiadomoscKlienta.Substring(3));
                    }

                    try
                    {
                        string[] files = Directory.GetFiles(newpath);
                        string response = "Aktualna sciezka: " + newpath + "\nLista plików i folderów:" + "\n";
                        foreach (string file in files)
                        {
                            string formated = Path.GetFileName(file);
                            response += (formated + "\n");
                        }

                        string[] directories = Directory.GetDirectories(newpath);
                        foreach (string directory in directories)
                        {
                            string formated = Path.GetFileName(directory);
                            response += (formated + "\n");
                        }

                        my_dir = newpath;
                        var echoBytes = Encoding.UTF8.GetBytes(response);
                        socketKlienta.Send(echoBytes, 0);
                    }
                    catch
                    {
                        var echoBytes = Encoding.UTF8.GetBytes("nie istnieje taki katalog");
                        socketKlienta.Send(echoBytes, 0);
                    }
                }
                else
                {
                    var echoBytes = Encoding.UTF8.GetBytes("nieznane polecenie");
                    socketKlienta.Send(echoBytes, 0);
                }

            }
        }
        catch
        {
            Console.WriteLine("Koniec pracy serwera");
            Stop();
            Environment.Exit(0); 
        }
    }
    public void Stop()
    {
        try
        {
            socketSerwera.Shutdown(SocketShutdown.Both);
            socketSerwera.Close();

        }
        catch{}
    }
}

    
    