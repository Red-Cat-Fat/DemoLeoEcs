using System;

namespace Services
{
	public class ScoreService
	{
		private int _score = 0;
		public int Score => _score;
		
		public ScoreService(int startScore = 0)
		{
			_score = startScore;
		}

		public void AddScore(int score)
		{
			_score += score;
		}
	}
}