using System.Windows.Input;
using Fifteen_The_Game.MVVM.Core;
using Fifteen_The_Game.MVVM.Models;
namespace Fifteen_The_Game.MVVM.ViewModels
{
    internal class MainViewModel : Base.ViewModel
    {
        #region Constants
        const int SIDE_SIZE = 60;
        const int BAR = 20;
        const int MARGIN = 2;
        public string WINMESSAGE { get; } = "You win!";
        #endregion

        #region Window Size
        public int WidthWin { get => _width; }
        public int Width { get => _width; set { _width = value; OnPropertyChanged(); } }
        private int _width;
        public int HeightWin { get => _height + 30; }
        public int Height { get => _height; set { _height = value; OnPropertyChanged(); } }
        private int _height;
        #endregion

        #region Window States
        public bool IsPlayScreenEnabled { get => _isPlayScreenEnabled; set { _isPlayScreenEnabled = value; OnPropertyChanged(); } }
        public bool IsWinScreenEnabled { get => _isWinScreenEnabled; set { _isWinScreenEnabled = value; OnPropertyChanged(); } }
        public bool IsSettingsScreenEnabled { get => _IsSettingsScreenEnabled; set { _IsSettingsScreenEnabled = value; OnPropertyChanged(); } }

        private bool _isPlayScreenEnabled;
        private bool _isWinScreenEnabled;
        private bool _IsSettingsScreenEnabled;
        #endregion

        public Board Board { get => _board; set => _board = value; }
        private Board _board;

        public int Rows { get; set; }

        public MainViewModel()
        {
            Rows = 4; // base value
            Board = new Board();
            GenerateBoard();
            _isPlayScreenEnabled = true;
            _IsSettingsScreenEnabled = false;
        }

        private void GenerateBoard()
        {
            _board.GenerateBoard(Rows);

            Width = ((MARGIN * 2) + SIDE_SIZE) * Rows;
            Height = BAR + ((MARGIN * 2 + SIDE_SIZE) * Rows);

            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(WidthWin));  //initialize PropertyChanged event 
            OnPropertyChanged(nameof(HeightWin));
            _board.OnWin += OnWin;
        }

        private void OnWin()
        {
            IsPlayScreenEnabled = false;
            IsWinScreenEnabled = true;
        }

        #region button`s handlers
        public ICommand PlayButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    GenerateBoard(); //regenerating board
                    IsWinScreenEnabled = false; //hide win screen and show play screen
                    IsPlayScreenEnabled = true;
                });
            }
        }

        public ICommand SettingsButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsSettingsScreenEnabled = !IsSettingsScreenEnabled; //hide or show setting screen
                });
            }
        }

        public ICommand CellButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Board.ButtonClicked((Cell)obj); // move square
                });
            }
        }

        public ICommand SettingsRadioButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Rows = int.Parse(obj.ToString()); //save settings and regenerate board
                    GenerateBoard();
                });
            }
        }
        #endregion   
    }
}
