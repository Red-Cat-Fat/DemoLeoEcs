using Components.Common;
using Leopotam.Ecs;
using UnityComponents.Common;
using UnityEngine;

namespace Systems.Spawners
{
	public class SpawnPlayer : IEcsInitSystem
	{
		private EcsWorld _world = null;
		private StaticData _sceneData;
		
		public void Init()
		{
			_world.NewEntity().Get<SpawnPrefab>() = new SpawnPrefab
			{
				Prefab = _sceneData.PlayerPrefab,
				Position = Vector3.zero,
				Rotation = Quaternion.identity,
				Parent = null
			};
		}
	}
}