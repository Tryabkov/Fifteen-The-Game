using System;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fifteen_The_Game.MVVM.Models
{
    internal class Board
    {
        public event Action OnWin;

        private int EmptyCellIndex;
        public ObservableCollection<Cell> Cells { get => _cells; set => _cells = value; }
        private ObservableCollection<Cell> _cells;

        private readonly int _rows;

        public int Margin { get; set; }

        public Board(int rows, int margin)
        {
            _rows = rows;
            GenerateBoard(margin);
        }

        private void GenerateBoard(int margin)
        {
            int rowsInCube = _rows * _rows;
            _cells = new ObservableCollection<Cell>();
            Random rnd = new Random();
            for (int i = 0; i < rowsInCube; i++)
            {
                _cells.Add(new Cell(i, margin));
            }
            EmptyCellIndex = 0;
            Mix();
        }

        private void Mix()
        {
            
        }

        public void ButtonClicked(Cell cell)
        {
            int cellIndex = cell.Index;
            if (!IsWin() && 
              ((cellIndex >= _rows && Cells[cellIndex - _rows].Num == "0")||
               (cellIndex + _rows < _rows*_rows && Cells[cellIndex + _rows].Num == "0")||
               (cellIndex > 0 && Cells[cellIndex - 1].Num == "0")|| 
               (cellIndex + 1 < _rows*_rows && Cells[cellIndex + 1].Num == "0")))
            {
                Swap(cellIndex);
            }

        }

        private void Swap(int CellIndex)
        {
            var temp = Cells[CellIndex];
            temp.Index = EmptyCellIndex;

            Cells[CellIndex] = Cells[EmptyCellIndex];

            Cells[EmptyCellIndex] = temp;
            EmptyCellIndex = CellIndex;
        }

        private bool IsWin()
        {
            bool result = true;
            for (int i = 0; i < Cells.Count; i++)
            {
                if (!(Cells[i].Num == i.ToString()))
                {
                    result = false; break;
                }
                if (result) { OnWin?.Invoke(); }
            }            
            return result;
        }
    }
}
