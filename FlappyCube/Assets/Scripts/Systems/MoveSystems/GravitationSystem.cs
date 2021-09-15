using Components.Objects.Moves;
using Leopotam.Ecs;
using UnityComponents.Common;
using UnityEngine;

namespace Systems.MoveSystems
{
	public class GravitationSystem : IEcsRunSystem
	{
		private StaticData _staticData;
		
		private EcsFilter<Gravitational> _filter = null;
		
		public void Run()
		{
			var deltaTime = Time.fixedTime;
			foreach (int index in _filter)
			{
				ref EcsEntity entity = ref _filter.GetEntity(index); 
				ref Velocity velocity = ref entity.Get<Velocity>();

				velocity.Value -= _staticData.GlobalGravitation * deltaTime;
			}
		}
	}
}