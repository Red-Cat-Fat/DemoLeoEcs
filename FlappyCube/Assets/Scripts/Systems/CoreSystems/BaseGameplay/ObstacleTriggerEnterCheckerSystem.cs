using Components.Core;
using Components.Objects.Tags;
using Components.PhysicsEvents;
using Leopotam.Ecs;
using UnityComponents.MonoLinks;
using UnityEngine;

namespace Systems.CoreSystems.BaseGameplay
{
	public class ObstacleTriggerEnterCheckerSystem : IEcsRunSystem
	{
		private EcsWorld _world = null;
		private EcsFilter<PlayerTag, OnTriggerEnterEvent> _filter = null;
		
		public void Run()
		{
			if (_filter.IsEmpty())
			{
				return;
			}

			foreach (int index in _filter)
			{
				ref EcsEntity entity = ref _filter.GetEntity(index);
				var onCollisionEnterEvent = entity.Get<OnTriggerEnterEvent>();
				
				GameObject collisionGameObject = onCollisionEnterEvent.Collider.gameObject;
				var obstacle = collisionGameObject.GetComponent<ObstacleTagMonoLink>();
				if (obstacle == null) 
					continue;
				
				entity.Get<OnObstacleExit>() = new OnObstacleExit
				{
					Score = obstacle.Value.Score,
				};
			}
		}
	}
}