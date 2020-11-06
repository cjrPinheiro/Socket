using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketClient
{
    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }


    public static void StartClient()
    {
        byte[] bytes = new byte[1024];

        try
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ip, 8080);

            Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                sender.Connect(remoteEP);

                Console.WriteLine("Socket conectado {0}",
                    sender.RemoteEndPoint.ToString());
                Console.WriteLine("Digite uma mensagem ou -1 para finalizar o chat");
                while (true)
                {

                    Console.WriteLine("Digite:");
                    string text = Console.ReadLine();
                    if (text != "-1")
                    {
                        byte[] msg = Encoding.ASCII.GetBytes(text);

                
                        int bytesSent = sender.Send(msg);

              
                        int bytesRec = sender.Receive(bytes);
                        string txt = Encoding.ASCII.GetString(bytes, 0, bytesRec) + "";
                        Console.WriteLine($"SERVIDOR = {txt}");
                    }
                    else
                    {
               
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                        break;
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("Exception : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("Exception : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
