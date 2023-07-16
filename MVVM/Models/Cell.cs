using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fifteen_The_Game.MVVM.Core;
using System.Windows.Input;

namespace Fifteen_The_Game.MVVM.Models
{
    internal class Cell
    {
        public string Num { get => _num.ToString(); set { _num = int.Parse(value); }}
        private int _num;

        public int Margin { get; set; }

        public int Index;

        public Cell(int Num, int margin)
        {
            Margin = margin;
            Index = _num = Num;
        }
    }
}
