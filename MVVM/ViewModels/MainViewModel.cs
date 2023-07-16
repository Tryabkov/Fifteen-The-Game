using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Fifteen_The_Game.MVVM.Core;
using System.Windows.Input;
using Fifteen_The_Game.MVVM.Models;
using System.Windows;

namespace Fifteen_The_Game.MVVM.ViewModels
{
    internal class MainViewModel : Base.ViewModel
    {
        #region Constants
        const int SIDE_SIZE = 60;
        const int BAR = 20;
        public string WINMESSAGE { get; } = "You win!";
        #endregion

        #region Window Size
        public int Width { get => _width; set { _width = value; OnPropertyChanged(); } }
        private int _width;
        public int Height { get => _height; set { _height = value; OnPropertyChanged(); } }
        private int _height;
        #endregion

        #region Window States
        public bool IsPlayScreenEnabled { get => _isPlayScreenEnabled; set { _isPlayScreenEnabled = value; OnPropertyChanged(); } }
        public bool IsWinScreenEnabled { get => _isWinScreenEnabled; set { _isWinScreenEnabled = value; OnPropertyChanged(); } }
        public bool IsSettingsScreenEnabled { get => !_IsSettingsScreenEnabled; set { _IsSettingsScreenEnabled = !value; OnPropertyChanged(); } }

        private bool _isPlayScreenEnabled;
        private bool _isWinScreenEnabled;
        private bool _IsSettingsScreenEnabled;
        #endregion

        public Board Board { get => _board; set => _board = value; }
        private Board _board;

        public int Rows { get; set; }

        public MainViewModel()
        {
            Rows = 4;
            int margin = 2;
            _board = new Board(Rows);
            Board.OnWin += OnWin;
            IsPlayScreenEnabled = true;


            Width = (((margin * 2) + SIDE_SIZE) * Rows);
            Height = BAR + ((margin * 2 + SIDE_SIZE) * Rows);
        }

        public ICommand PlayButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Board = new Board(Rows);
                    IsWinScreenEnabled = false;
                });
            }
        }

        public ICommand SettingsButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                });
            }
        }

        public ICommand CellButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Board.ButtonClicked((Cell)obj);
                });
            }
        }

        private void OnWin()
        {
            IsPlayScreenEnabled = false;
            IsWinScreenEnabled = true;
        }
    }
}
