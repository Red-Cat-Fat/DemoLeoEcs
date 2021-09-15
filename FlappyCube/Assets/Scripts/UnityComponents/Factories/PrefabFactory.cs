using Components.Common;
using Leopotam.Ecs;
using UnityComponents.MonoLinks;
using UnityComponents.MonoLinks.Base;
using UnityEngine;

namespace UnityComponents.Factories
{
	public class PrefabFactory : MonoBehaviour
	{
		private EcsWorld _world;
		
		public void SetWorld(EcsWorld world)
		{
			_world = world;
		}
		
		public void Spawn(SpawnPrefab spawnData)
		{
			GameObject gameObject = Instantiate(spawnData.Prefab, spawnData.Position, spawnData.Rotation, spawnData.Parent);
			var monoEntity = gameObject.GetComponent<MonoEntity>();
			if (monoEntity == null) 
				return;
			EcsEntity ecsEntity = _world.NewEntity();
			monoEntity.Make(ref ecsEntity);
		}
	}
}