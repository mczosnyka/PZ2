namespace lab6._2;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private Socket socketSerwera;
    private IPAddress ipAddress;
    private IPEndPoint localEndPoint;

    public Server()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        ipAddress = host.AddressList[0];
        localEndPoint = new IPEndPoint(ipAddress, 12345);
        socketSerwera = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Start()
    {
        socketSerwera.Bind(localEndPoint);
        socketSerwera.Listen(100);
        Socket socketKlienta = socketSerwera.Accept();
        int siz = GetMessageSize(socketKlienta);
        HandleClient(socketKlienta, siz);
    }

    public int GetMessageSize(Socket socketKlienta)
    {
        byte[] bufor = new byte[4];
        int received = socketKlienta.Receive(bufor, SocketFlags.None);
        String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
        var echoBytes = Encoding.UTF8.GetBytes(wiadomoscKlienta);
        int wiadomosc = int.Parse(wiadomoscKlienta);
        socketKlienta.Send(echoBytes, 0);
        return wiadomosc;
    }
    
    public void HandleClient(Socket socketKlienta, int sizer)
    {
        try
        {
            while (true)
            {
                byte[] bufor = new byte[sizer];
                int received = socketKlienta.Receive(bufor, SocketFlags.None);
                String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
                var echoBytes = Encoding.UTF8.GetBytes("odczytalem: " + wiadomoscKlienta);
                socketKlienta.Send(echoBytes, 0);
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

    
    