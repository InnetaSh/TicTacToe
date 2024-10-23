using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TicTacToe
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Введите свое имя: ");
            string? userName = Console.ReadLine();

            using TcpClient tcpClient = new TcpClient();
            Console.WriteLine($"Игрок {userName} подключился");
            tcpClient.Connect("127.0.0.1", 13000);

            var stream = tcpClient.GetStream();
            StreamReader? Reader = null;
            StreamWriter? Writer = null;

            if (tcpClient.Connected)
            {

                Reader = new StreamReader(tcpClient.GetStream());
                Writer = new StreamWriter(tcpClient.GetStream());
                if (Writer is null || Reader is null) return;





                Board board = new Board("X");
                board.Print();

                while (true)
                {

                    while (true)
                    {
                        Console.WriteLine("Введите ход игрок 1");
                        string hod = Console.ReadLine();
                        var msg = board.Move(hod, true);
                        if (!String.IsNullOrEmpty(msg))
                        {
                            Console.WriteLine(msg);
                            Console.WriteLine("Повторите ход");
                        }

                        else
                        {
                           
                            byte[] hodData = Encoding.UTF8.GetBytes(hod);
                            stream.Write(hodData, 0, hodData.Length);





                            Console.Clear();
                            board.Print();
                            break;
                        }
                    }
                    if (board.WinLogic())
                    {
                        Console.WriteLine("Победил игрок 1");
                        break;
                    }



                    while (true)
                    {
                        //Console.WriteLine("Введите ход игрок 2");
                        //string Hod2 = Console.ReadLine();


                        byte[] buffer = new byte[256];

                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string hod2 = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                     



                        var msg2 = board.Move(hod2, false);
                        if (!String.IsNullOrEmpty(msg2))
                        {
                            Console.WriteLine(msg2);
                            Console.WriteLine("Повторите ход");
                        }

                        else
                        {
                            Console.Clear();
                            board.Print();
                            break;
                        }
                    }
                    if (board.WinLogic())
                    {
                        Console.WriteLine("Победил игрок 2");
                        break;
                    }
                }
            }

            else
                Console.WriteLine("Не удалось подключиться");
            Writer?.Close();
            Reader?.Close();
        }
    }
}
