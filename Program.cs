using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
             Game();
        }
        static  void Game()
        {
            


            Console.Write("Введите свое имя: ");
            string? userName = Console.ReadLine();


            string serverIp = "25.28.51.91";
            int port = 12345;

            TcpClient tcpClient = new TcpClient(serverIp, port);


            var stream = tcpClient.GetStream();
            StreamReader? Reader = new StreamReader(stream);
            StreamWriter? Writer = new StreamWriter(stream) { AutoFlush = true };

            if (tcpClient.Connected)
            {
                

                Console.WriteLine($"Игрок {userName} подключился");
                Thread.Sleep(1000);
               
                if (Writer is null || Reader is null) return;


               
                Writer.WriteLine(userName);

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
                            Writer.WriteLine(hod);
                            Writer.Flush();
                           

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
                        
                        string hod2 =  Reader.ReadLine();


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

                stream.Close();
                tcpClient.Close();
            }

            else
                Console.WriteLine("Не удалось подключиться");
            Writer?.Close();
            Reader?.Close();
        }
    }
}
