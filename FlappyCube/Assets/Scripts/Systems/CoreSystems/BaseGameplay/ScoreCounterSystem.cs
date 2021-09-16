using Components.Core;
using Leopotam.Ecs;
using Services;
using UnityComponents.Common;

namespace Systems.CoreSystems.BaseGameplay
{
	public class ScoreCounterSystem : IEcsRunSystem
	{
		private SceneData _sceneData;
		private ScoreService _score;
		private EcsWorld _world = null;
		private EcsFilter<OnObstacleExit> _obstacleScoreFilter = null;

		public void Run()
		{
			if (_obstacleScoreFilter.IsEmpty()) 
				return;
			
			foreach (int index in _obstacleScoreFilter)
			{
				OnObstacleExit obstacleScore = _obstacleScoreFilter.Get1(index);
				_score.AddScore(obstacleScore.Score);
				_sceneData.Hud.SetScore(_score.Score);
			}
		}
	}
}