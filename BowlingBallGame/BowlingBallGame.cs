using System;
using System.Collections.Generic;

namespace BowlingBall
{
	public class BowlingBallGame
	{
		public static void Main(string[] args)
		{
			try
			{
				int score = 0;
				Frames frames = new();

				for (int i = 0; i < 10; i++)
				{
					Frame frame = frames.GetFrame(i);
					frame.FrameScore = score + frames.CalculateScore(frame);
					score = frame.FrameScore;
				}

				ScoreBoard.DisplayScoreBoard(frames);
				Console.Read();
			}
			catch (Exception ex) 
			{
				Console.WriteLine("Something bad happened... Here is the error {0}", ex.Message);
			}
		}
	}

	public class BBHelper
	{
		public static bool IsStrike(char score)
		{
			if (score == 'X')
				return true;
			return false;
		}

		public static bool IsSpare(char score)
		{
			if (score == '/')
				return true;
			return false;
		}

		public static int CnvToInt(char chr)
		{
			return int.Parse(chr.ToString());
		}
	}

	public class ScoreBoard
	{
		public static void DisplayScoreBoard(Frames frames)
		{
			foreach (var frame in frames.FrameList) 
			{
				Console.WriteLine("Frame {0}", frame.Id + 1);
				Console.WriteLine("----------");

				if (frame.Id == 9 && !char.IsWhiteSpace(frame.BallThrows[2]))
				{
					Console.WriteLine("|{0}   {1}   {2}|", frame.BallThrows[0], frame.BallThrows[1], frame.BallThrows[2]);
				}
                else 
				{
					Console.WriteLine("|{0}   {1}|", frame.BallThrows[0], frame.BallThrows[1]);
				}

				if (frame.FrameScore == -1)
					Console.WriteLine("|   -|");
				else
					Console.WriteLine("|   {0}|", frame.FrameScore);
				Console.WriteLine("----------");
			}
		}
	}

	public class Frames
	{
		public List<Frame> FrameList { get; set; }

		public Frames()
		{
			FrameList = new List<Frame>();
		}

		private char[] GetUserInput(int frameId)
		{
			ScoreBoard.DisplayScoreBoard(this);

			try
			{
				// input includes X, /, 0-9
				char[] ballThrows = new char[3];
				Console.WriteLine("Please enter your score (valid characters : 0-9, X, /) for frame {0}", frameId + 1);
				Console.WriteLine("Throw 1 :");
				ballThrows[0] = Convert.ToChar(Console.ReadLine());
				Console.WriteLine("Throw 2 :");
				ballThrows[1] = Convert.ToChar(Console.ReadLine());
				if (frameId == 9 && (BBHelper.IsStrike(ballThrows[0]) || BBHelper.IsSpare(ballThrows[1])))
				{
					Console.WriteLine("Throw 3 :");
					ballThrows[2] = Convert.ToChar(Console.ReadLine());
				}

				return ballThrows;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occured while reading user inputs {0}", ex.Message.ToString());
				return Array.Empty<char>();
			}
		}

		private int GetScoreForLastFrame(char[] ballThrows)
		{
			int score = 0;
			bool isBonusFlag = false;

			foreach (char c in ballThrows)
			{
				if (BBHelper.IsStrike(c) || BBHelper.IsSpare(c))
				{
					isBonusFlag = true;
					break;
				}
			}

			if (isBonusFlag)
			{
				if (BBHelper.IsStrike(ballThrows[0]) && BBHelper.IsStrike(ballThrows[1]) && BBHelper.IsStrike(ballThrows[2]))
					score = 30;
				else if ((BBHelper.IsSpare(ballThrows[1]) && BBHelper.IsStrike(ballThrows[2])) || (BBHelper.IsSpare(ballThrows[2]) && BBHelper.IsStrike(ballThrows[0])))
					score = 20;
				else
				{
					if (BBHelper.IsStrike(ballThrows[0]) && !BBHelper.IsStrike(ballThrows[1]) && !BBHelper.IsStrike(ballThrows[2]))
						score = score + 10 + BBHelper.CnvToInt(ballThrows[1]) + BBHelper.CnvToInt(ballThrows[2]);
				}
			}
			else
				score = BBHelper.CnvToInt(ballThrows[0]) + BBHelper.CnvToInt(ballThrows[1]);

			return score;
		}

		private int GetBonusPoints(int currentFrame, int requireThrows)
		{
			int bonusPoints = 0;
			char[] ballThrows = GetFrame(currentFrame + 1).BallThrows;
			if (requireThrows == 1)
			{
				if (BBHelper.IsStrike(ballThrows[0]))
					bonusPoints = 10;
				else
					bonusPoints = BBHelper.CnvToInt(ballThrows[0]);
			}
			else if (requireThrows == 2)
			{
				if (BBHelper.IsStrike(ballThrows[0]))
				{
					bonusPoints = 10;
					char[] nextFrameThrows = GetFrame(currentFrame + 2).BallThrows;
					if (BBHelper.IsStrike(nextFrameThrows[0]))
						bonusPoints += 10;
					else
						bonusPoints += BBHelper.CnvToInt(nextFrameThrows[0]);
				}
				else if (BBHelper.IsSpare(ballThrows[1]))
					bonusPoints = 10;
				else
					bonusPoints = BBHelper.CnvToInt(ballThrows[0]) + BBHelper.CnvToInt(ballThrows[1]);
			}
			return bonusPoints;
		}

		public Frames Add(int frameId, char[] ballThrows) 
		{
			Frame frame = new();
			frame.Id = frameId;
			frame.BallThrows = ballThrows;
			this.FrameList.Add(frame);
			return this;
		}

		public Frame GetFrame(int currentFrameId) 
		{
			if (this.FrameList.Where(x => x.Id == currentFrameId).Count() == 1)
			{
				return this.FrameList[currentFrameId];
			}
			else if (currentFrameId > 9) 
			{
				return this.FrameList[9];
			}
			else
			{
				this.Add(currentFrameId, GetScoreOfThrows(currentFrameId));
				return this.FrameList[currentFrameId];
			} 
		}

		public char[] GetScoreOfThrows(int frameId)
		{
			char[] scorePerThrow;
			if (this.FrameList.Where(x => x.Id == frameId).Count() == 1)
			{
				scorePerThrow = this.FrameList.FirstOrDefault(x => x.Id == frameId).BallThrows;
				return scorePerThrow;
			}
			else
			{
				return GetUserInput(frameId);
			}
		}

		public int CalculateScore(Frame frame)
		{
			int score = 0;
			if (frame.Id == 9)
			{
				score += GetScoreForLastFrame(frame.BallThrows);
				return score;
			}


			if (BBHelper.IsStrike(frame.BallThrows[0]))
			{
				score = score + 10 + GetBonusPoints(frame.Id, 2);
			}
			else if (BBHelper.IsSpare(frame.BallThrows[1]))
			{
				score = score + 10 + GetBonusPoints(frame.Id, 1);
			}
			else
				score = score + BBHelper.CnvToInt(frame.BallThrows[0]) + BBHelper.CnvToInt(frame.BallThrows[1]);

			return score;
		}

	}

	public class Frame
	{
		public int Id { get; set; }
		public char[] BallThrows { get; set; }
		public int FrameScore { get; set; }

		public Frame() 
		{
			BallThrows = new char[3];
			FrameScore = -1;
		}
	}
}
