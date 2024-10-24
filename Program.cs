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
                

               

                var msgNumber = Reader.ReadLine();
                var num = Reader.ReadLine();

                Console.WriteLine(msgNumber);
                Console.Write(num);
                string choise;
                if (num == "X")
                {
                     choise = "X";
                }
                else
                {
                    choise = "O";
                }

                Board board = new Board(choise);
                board.Print();



                if (choise == "X")
                {
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
                            var msgWin = "Победил игрок 1";
                            Writer.WriteLine(msgWin);
                            Console.WriteLine(msgWin);
                            break;
                        }



                        while (true)
                        {

                            string msgHod2 = Reader.ReadLine();
                            string hod2 = Reader.ReadLine();


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
                            var msgWin = "Победил игрок 2";
                            Writer.WriteLine(msgWin);
                            Console.WriteLine(msgWin);
                            break;
                        }
                    }
                }

                else
                {
                    while (true)
                    {

                        while (true)
                        {

                            string msgHod2 = Reader.ReadLine();
                            string hod2 = Reader.ReadLine();


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
                            var msgWin = "Победил игрок 2";
                            Writer.WriteLine(msgWin);
                            Console.WriteLine(msgWin);
                            break;
                        }

                        while (true)
                        {
                            Console.WriteLine("Введите ход игрок 2");
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
                            var msgWin = "Победил игрок 1";
                            Writer.WriteLine(msgWin);
                            Console.WriteLine(msgWin);
                            break;
                        }

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
