using System.Net;
using System.Net.Sockets;
using System.Text;
namespace lab6._3;

public class Client
{
    private Socket socketSerwera { get; set; }
    private IPAddress ipAddress { get; set; }
    private IPEndPoint localEndPoint { get; set; }



    public Client()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        ipAddress = host.AddressList[0];
        localEndPoint = new IPEndPoint(ipAddress, 12345);
        socketSerwera = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        socketSerwera.Connect(localEndPoint);
    }

    public void SendMessage(string message)
    {

        byte[] wiadomoscBajty = Encoding.UTF8.GetBytes(message);
        socketSerwera.Send(wiadomoscBajty, SocketFlags.None);
    }
    
    
    public void ReceiveMessage()
    {
        var bufor = new byte[4096];
        int liczbaBajtów = socketSerwera.Receive(bufor, SocketFlags.None);
        String odpowiedzSerwera = Encoding.UTF8.GetString(bufor, 0, liczbaBajtów);
        Console.WriteLine(odpowiedzSerwera);
    }

    public void Stop()
    {
        try {
            socketSerwera.Shutdown(SocketShutdown.Both);
            socketSerwera.Close();
        }
        catch{}
    }
}