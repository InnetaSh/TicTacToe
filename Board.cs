using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Cell;

namespace TicTacToe
{
    internal class Board
    {
        public int StartX;
        public int StartY;
        public static int Width = 3;
        public static int Height = 3;
        public Cell[,] CellMas = new Cell[Width, Height];

        string MySymb;
        string PlayerSymb;

        public Board(string mySymb)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    CellMas[i, j] = new Cell(i, j);
            }
            MySymb = mySymb;
            PlayerSymb = MySymb == "X" ? "O" : "X";
        }

        public void Print()
        {
            Console.Clear();
            Console.WriteLine("  A  B  C");
            for (int i = 0; i < Width; i++)
            {
                Console.Write($"{i + 1,2}");
                for (int j = 0; j < Height; j++)
                {

                    if (CellMas[i, j].State == CellState.Empty)
                        Console.Write(j == Height - 1 ? "  \n" : "  |");
                    else if (CellMas[i, j].State == CellState.MyOccupied)
                        Console.Write(j == Height - 1 ? $" {MySymb}\n" : $" {MySymb}|");
                    else if (CellMas[i, j].State == CellState.Occupied)
                        Console.Write(j == Height - 1 ? $" {PlayerSymb}\n" : $" {PlayerSymb}|");
                }

                if (i < Width - 1)
                    Console.WriteLine("  --+--+--");


            }
        }

        public string Move(string hod, bool isMyHod)
        {
            hod = hod.ToLower();
            var msg = string.Empty;
            if (hod.Length != 2)
                return "Некорректный формат хода ";
            if (!(hod[0] == 'a' || hod[0] == 'b' || hod[0] == 'c'))
                return "Неверный ввод первой координаты (a,b,c) ";
            if (!(hod[1] == '1' || hod[1] == '2' || hod[1] == '3'))
                return "Неверный ввод второй координаты (1,2,3) ";

            var cell = GetCell(hod);
            if (cell.State == CellState.Occupied || cell.State == CellState.MyOccupied)
                return "Клетка занята";

            cell.State = isMyHod ? CellState.MyOccupied : CellState.Occupied;

            return msg;
        }

        private Cell GetCell(string hod)
        {
            var j = hod[0] == 'a' ? 0 : hod[0] == 'b' ? 1 : 2;
            var i = hod[1] == '1' ? 0 : hod[1] == '2' ? 1 : 2;
            return CellMas[i, j];
        }

        public bool WinLogic()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CellMas[i, 0].State != CellState.Empty && CellMas[i, 0].State == CellMas[i, 1].State && CellMas[i, 1].State == CellMas[i, 2].State)
                    return true;
                if (CellMas[0, i].State != CellState.Empty && CellMas[0, i].State == CellMas[1, i].State && CellMas[1, i].State == CellMas[2, i].State)
                    return true;
            }
            if (CellMas[0, 0].State != CellState.Empty && CellMas[0, 0].State == CellMas[1, 1].State && CellMas[1, 1].State == CellMas[2, 2].State)
                return true;
            if ((CellMas[0, 2].State != CellState.Empty) && CellMas[0, 2].State == CellMas[1, 1].State && CellMas[1, 1].State == CellMas[2, 0].State)
            return true;

            return false;
        }
    }
}
