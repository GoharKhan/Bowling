using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public enum FrameState
    {
        First,
        Second,
        Strike,
        Spare,
        End,
    }

    public class Frame : ObservableObject
    {
        public int FirstBall { get; private set; }
        public int SecondBall { get; private set; }
        public int StrikeBall { get; private set; }
        public int SpareBall { get; private set; }
        public bool IsStrike { get; private set; }
        public bool IsSpare { get; private set; }
        public int Score { get; private set; }
        public FrameState State { get; private set; }
        public bool IsExtension { get; private set; }
        public bool IsEnded { get { return State == FrameState.End; } }

        public Frame() : this(false) { }

        public Frame(bool isExtension)
        {
            State = FrameState.First;
            IsExtension = IsExtension;
        }

        public bool CanAddScore(int score)
        {
            return (score >= 0 && score <= 10) &&
                (State < FrameState.Strike && FirstBall + score <= 10);
        }

        public void AddScore(int score)
        {
            if (score > 10 || score < 0)
                throw new ArgumentException("Score must be a number between 0 and 10");
            if (State < FrameState.Strike && FirstBall + score > 10)
                throw new ArgumentException(string.Format("Score must be less than or equal {0}", 10 - FirstBall));

            CheckState(score);

            if (State == FrameState.End && !IsExtension)
                ComputeScore();
        }

        private void CheckState(int score)
        {
            switch (State)
            {
                case FrameState.First:
                    FirstBall = score;
                    if (score < 10)
                        State = FrameState.Second;
                    else
                    {
                        State = FrameState.Strike;
                        IsStrike = true;
                    }
                    RaisePropertyChanged<int>(() => FirstBall);
                    break;

                case FrameState.Second:
                    SecondBall = score;
                    if (FirstBall + SecondBall < 10)
                        State = FrameState.End;
                    else
                    {
                        State = FrameState.Spare;
                        IsSpare = true;
                    }
                    RaisePropertyChanged<int>(() => SecondBall);
                    break;

                case FrameState.Strike:
                    StrikeBall = score;
                    State = FrameState.Spare;
                    break;

                case FrameState.Spare:
                    SpareBall = score;
                    State = FrameState.End;
                    break;

                case FrameState.End:
                    throw new Exception("Frame completed!");
            }
            RaisePropertyChanged<bool>(() => IsEnded);
        }

        private void ComputeScore()
        {
            Score = FirstBall + SecondBall + SpareBall + StrikeBall;
            RaisePropertyChanged<int>(() => Score);
        }
    }
}
