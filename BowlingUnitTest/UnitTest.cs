using System;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bowling;

namespace BowlingUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestWithSampleData()
        {
            var sampleData = GetTestData();

            foreach (var item in sampleData)
            {
                try
                {
                    var sb = new Scoreboard();
                    foreach (var frame in item.Frames)
                    {
                        foreach (var score in frame.Scores)
                        {
                            sb.AddScore(score);
                        }
                    }

                    Assert.AreEqual(true, sb.IsCompleted);
                    Assert.AreEqual(item.TotalScore, sb.Score);
                    foreach (var frame in sb.Frames)
                    {
                        if (!frame.IsExtension)
                        {
                            Assert.AreEqual(FrameState.End, frame.State);
                            Assert.AreEqual(true, frame.IsEnded);
                        }
                        Assert.AreEqual(item.Frames[sb.Frames.IndexOf(frame)].TotalScore, frame.Score);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            Assert.Fail("No exception thrown");
        }

        private List<Board> GetTestData()
        {
            var sampleData = new List<Board> {
                    new Board {
                        Frames = new Frame[] {
                            new Frame{Scores = new int[] {1, 4}, TotalScore = 5},
                            new Frame{Scores = new int[] {4, 5}, TotalScore = 9},
                            new Frame{Scores = new int[] {6, 4}, TotalScore = 15},
                            new Frame{Scores = new int[] {5, 5}, TotalScore = 20},
                            new Frame{Scores = new int[] {10}, TotalScore = 11},
                            new Frame{Scores = new int[] {0, 1}, TotalScore = 1},
                            new Frame{Scores = new int[] {7, 3}, TotalScore = 16},
                            new Frame{Scores = new int[] {6, 4}, TotalScore = 20},
                            new Frame{Scores = new int[] {10} , TotalScore = 20},
                            new Frame{Scores = new int[] {2, 8}, TotalScore = 16},
                            new Frame{Scores = new int[] {6}, TotalScore = 0},
                        }, 
                        TotalScore = 133
                    },
                    new Board {
                        Frames = new Frame[] {
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 30},
                            new Frame{Scores = new int[] {10}, TotalScore = 0},
                            new Frame{Scores = new int[] {10}, TotalScore = 0},
                     }, 
                     TotalScore = 300
                 },
            };
            return sampleData;
        }

        public class Frame
        {
            public int[] Scores;
            public int TotalScore;
        }

        public class Board
        {
            public Frame[] Frames;
            public int TotalScore;
        }
    }
}
