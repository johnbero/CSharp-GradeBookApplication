using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
	public class RankedGradeBook : BaseGradeBook
	{
		public RankedGradeBook(string name) : base(name)
		{
			Type = GradeBookType.Ranked;
		}

		public override char GetLetterGrade(double averageGrade)
		{
			if (Students.Count < 5)
			{
				throw new InvalidOperationException();
			}

			Students = Students.OrderBy(x => x.AverageGrade).ToList();
			double topTwentyPercent = Students[GetPercentileValue(80.0)].AverageGrade;
			double topFourtyPercent = Students[GetPercentileValue(60.0)].AverageGrade;
			double topSixtyPercent = Students[GetPercentileValue(40.0)].AverageGrade;
			double topEightyPercent = Students[GetPercentileValue(20.0)].AverageGrade;

			if (averageGrade > topTwentyPercent)
			{
				return 'A';
			}
			else if (averageGrade <= topTwentyPercent && averageGrade > topFourtyPercent)
			{
				return 'B';
			}
			else if (averageGrade <= topFourtyPercent && averageGrade > topSixtyPercent)
			{
				return 'C';
			}
			else if (averageGrade <= topSixtyPercent && averageGrade > topEightyPercent)
			{
				return 'D';
			}
			return 'F';
		}

		public override void CalculateStatistics()
		{
			if (Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
				return;
			}

			base.CalculateStatistics();
		}

		public override void CalculateStudentStatistics(string name)
		{
			if (Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
				return;
			}

			base.CalculateStudentStatistics(name);
		}

		private int GetPercentileValue(double percentile)
		{
			return (int)((percentile / 100) * Students.Count) - 1;
		}
	}
}
