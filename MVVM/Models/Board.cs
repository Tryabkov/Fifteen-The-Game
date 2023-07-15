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

        private int EmptyCellIndex;
        public ObservableCollection<Cell> Cells { get => _cells; set => _cells = value; }
        private ObservableCollection<Cell> _cells;

        public int Margin { get; set; }

        public Board(int rows, int margin)
        {
            GenerateBoard(rows, margin);
        }

        private void GenerateBoard(int rows, int margin)
        {
            int rowsInCube = rows * rows;
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
            
            if (true)
            {
                Swap(cell.Index);
            }

        }

        private void Swap(int CellIndex)
        {
            var temp = Cells[CellIndex];
            temp.Index = EmptyCellIndex;

            Cells[CellIndex] = Cells[EmptyCellIndex];

            Cells[EmptyCellIndex] = temp;
            EmptyCellIndex = CellIndex;

        private bool IsWin()
        {
            return false;
        }
    }
}
