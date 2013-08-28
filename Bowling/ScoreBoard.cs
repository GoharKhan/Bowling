using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Scoreboard : ObservableObject
    {
        ObservableCollection<Frame> _frames = new ObservableCollection<Frame>();

        public bool IsCompleted { get; private set; }
        public int Score { get; private set; }
        public ObservableCollection<Frame> Frames { get { return _frames; } }

        public bool CanAddScore(int score)
        {
            var lastframe = _frames.LastOrDefault();
            return !IsCompleted &&
                (score <= 10 && score >= 0) &&
                (lastframe == null || lastframe.State > FrameState.Second || lastframe.CanAddScore(score));
        }

        public void AddScore(int score)
        {
            if (IsCompleted)
                throw new Exception("Scoreboard completed!");

            AddScoreToFrame(score);

            ComputeScore();
        }

        private void AddScoreToFrame(int score)
        {
            var lastframe = _frames.LastOrDefault();
            if (lastframe == null || lastframe.State > FrameState.Second)
                AddFrame();

            foreach (var frame in _frames.Where(f => f.State != FrameState.End))
                frame.AddScore(score);

            if (_frames.Count >= 10 && _frames[9].State == FrameState.End)
                IsCompleted = true;

            RaisePropertyChanged<ObservableCollection<Frame>>(() => Frames);
        }

        private void AddFrame()
        {
            if (Frames.Count < 10)
                _frames.Add(new Frame());
            else
                _frames.Add(new Frame(true));
        }

        private void ComputeScore()
        {
            Score = _frames.Where(f => !f.IsExtension && f.State == FrameState.End).Sum(f => f.Score);
            RaisePropertyChanged<int>(() => Score);
        }
    }
}
