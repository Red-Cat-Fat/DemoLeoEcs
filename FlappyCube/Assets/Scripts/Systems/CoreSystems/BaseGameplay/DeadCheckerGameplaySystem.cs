using Components.GameStates;
using Components.GameStates.GameplayEvents;
using Leopotam.Ecs;
using UnityComponents.Common;
using UnityEngine;

namespace Systems.CoreSystems.BaseGameplay
{
	public class DeadCheckerGameplaySystem : IEcsInitSystem, IEcsRunSystem
	{
		private SceneData _sceneData;
		private EcsWorld _world = null;
		private EcsFilter<DeadEvent> _deadFilter = null;
		private EcsFilter<GameProgress> _gameProgress;

		public void Init()
		{
			_world.NewEntity().Get<GameProgress>() = new GameProgress
			{
				IsPause = true
			};
		}

		public void Run()
		{
			if (_deadFilter.IsEmpty()) 
				return;
			
			foreach (int index in _gameProgress)
			{
				ref GameProgress progress = ref _gameProgress.Get1(index);
				progress.IsPause = true;
				_sceneData.Hud.ShowGameOver();
			}
		}
	}
}