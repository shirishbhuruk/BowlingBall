using NUnit.Framework;

namespace BowlingBall.Tests
{
    [TestFixture]
    public class BowlingBallTests
    {
        Frames noScoreFrames = new();
        Frames maxScoreFrames = new();
        Frames spareFrames = new();

        [SetUp]
        public void Setup()
        {
            noScoreFrames.FrameList = new System.Collections.Generic.List<Frame> { 
                    new Frame()
                    {
                         Id = 0,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 1,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 2,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 3,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 4,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 5,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 6,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 7,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 8,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 9,
                         BallThrows = new char[] { '0', '0' },
                         FrameScore = -1,
                    },
            };


            maxScoreFrames.FrameList = new System.Collections.Generic.List<Frame> {
                    new Frame()
                    {
                         Id = 0,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 1,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 2,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 3,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 4,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 5,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 6,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 7,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 8,
                         BallThrows = new char[] { 'X', '0' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 9,
                         BallThrows = new char[] { 'X', 'X', 'X' },
                         FrameScore = -1,
                    },
            };

            spareFrames.FrameList = new System.Collections.Generic.List<Frame> {
                    new Frame()
                    {
                         Id = 0,
                         BallThrows = new char[] { '1', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 1,
                         BallThrows = new char[] { '2', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 2,
                         BallThrows = new char[] { '3', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 3,
                         BallThrows = new char[] { '4', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 4,
                         BallThrows = new char[] { '5', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 5,
                         BallThrows = new char[] { '6', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 6,
                         BallThrows = new char[] { '7', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 7,
                         BallThrows = new char[] { '8', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 8,
                         BallThrows = new char[] { '9', '/' },
                         FrameScore = -1,
                    },
                    new Frame()
                    {
                         Id = 9,
                         BallThrows = new char[] { '2', '/', 'X' },
                         FrameScore = -1,
                    },
            };
        }

        [Test]
        public void TestNoScoreFrames()
        {
            int score = 0;
            foreach (var frame in noScoreFrames.FrameList) 
            {
                score += noScoreFrames.CalculateScore(frame);
            }
            Assert.AreEqual(0, score);
        }

        [Test]
        public void TestMaximumScoreFrames()
        {
            int score = 0;
            foreach (var frame in maxScoreFrames.FrameList)
            {
                score += maxScoreFrames.CalculateScore(frame);
            }
            Assert.AreEqual(300, score);
        }

        [Test]
        public void TestSpareFrames()
        {
            int score = 0;
            foreach (var frame in spareFrames.FrameList)
            {
                score += spareFrames.CalculateScore(frame);
            }
            Assert.AreEqual(156, score);
        }

    }
}