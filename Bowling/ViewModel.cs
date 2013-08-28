using MicroMvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bowling
{
    public class ViewModel : ObservableObject
    {
        private int _score;
        private Scoreboard _scoreboard = new Scoreboard();

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                RaisePropertyChanged<int>(() => Score);
            }
        }

        public Scoreboard Scoreboard
        {
            get
            {
                return _scoreboard;
            }
            set
            {
                _scoreboard = value;
                RaisePropertyChanged<Scoreboard>(() => Scoreboard);
            }
        }

        public ICommand UpdateScore
        {
            get
            {
                return new RelayCommand(AddScore, CanAddScore);
            }
        }

        public ICommand ResetTable
        {
            get
            {
                return new RelayCommand(() => Scoreboard = new Scoreboard(), () => true);
            }
        }

        public void AddScore()
        {
            _scoreboard.AddScore(_score);
            RaisePropertyChanged<Scoreboard>(() => Scoreboard);
        }

        public bool CanAddScore()
        {
            return _scoreboard.CanAddScore(Score);
        }
    }
}
