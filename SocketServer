using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketServer
{
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }


    public static void StartServer()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ip = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ip, 8080);


        try
        { 
            Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(10);

            Console.WriteLine("Aguardando conexão...");
            Socket handler = listener.Accept();
            Console.WriteLine("Cliente conectado...");
         
            while (true)
            {
                string txt = string.Empty;
                byte[] bytes = null;
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);

                txt = Encoding.ASCII.GetString(bytes, 0, bytesRec) + "";

                Console.WriteLine("CLIENTE: {0}", txt);
                Console.WriteLine("Digite:");
                var text = Console.ReadLine();
                if (text != "-1")
                {
                    byte[] msg = Encoding.ASCII.GetBytes(text);
                    handler.Send(msg);
                }
                else
                {
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\n Pressione uma tecla para finalizar");
        Console.ReadKey();
    }
}
