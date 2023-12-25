using System;
using System.Collections.ObjectModel;

namespace Fifteen_The_Game.MVVM.Models
{
    internal class Board
    {
        public event Action OnWin;

        private Cell EmptyCell;
        public ObservableCollection<Cell> Cells { get => _cells; set => _cells = value; }
        private ObservableCollection<Cell> _cells;

        private int _rows;

        public Board()
        { }

        public void GenerateBoard(int rows)
        {
            _rows = rows;
            _cells = new ObservableCollection<Cell>();
            for (int i = 0; i < rows * rows; i++)
            {
                _cells.Add(new Cell(i));
            }
            EmptyCell = Cells[0];
            Mix();
        }

        private void Mix()
        {
            Random rnd = new Random();

            for (int i = 0; i < Cells.Count; i++)
            {
                Swap(i, rnd.Next(0, Cells.Count));
            }
        }

        public void ButtonClicked(Cell cell)
        {
            int cellIndex = cell.Index;
            if ((cellIndex >= _rows && Cells[cellIndex - _rows].Num == "0") || 
               (cellIndex + _rows < _rows * _rows && Cells[cellIndex + _rows].Num == "0") ||                       //checking for the possibility of square movement
               (cellIndex > 0 && (cellIndex % _rows) != 0 && Cells[cellIndex - 1].Num == "0") ||
               (cellIndex + 1 < _rows * _rows && (cellIndex + 1) % _rows != 0 && Cells[cellIndex + 1].Num == "0"))
            {
                Swap(EmptyCell.Index, cellIndex);
                EmptyCell.Index = cellIndex;
                IsWin();
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = Cells[firstIndex];

            Cells[firstIndex] = Cells[secondIndex];
            Cells[firstIndex].Index = firstIndex;

            Cells[secondIndex] = temp;
            Cells[secondIndex].Index = secondIndex;
        }

        private void IsWin()
        {
            bool result = true;
            for (int i = 0; i < Cells.Count - 1; i++)
            {
                if (!(Cells[i].Num == (i + 1).ToString()))
                {
                    result = false; break;
                }
            }
            if (result) { OnWin?.Invoke(); }
        }
    }
}
