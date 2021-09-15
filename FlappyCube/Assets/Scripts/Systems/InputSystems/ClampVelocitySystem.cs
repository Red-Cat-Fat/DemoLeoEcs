using Components.Objects.Moves;
using Components.Objects.Tags;
using Leopotam.Ecs;
using UnityComponents.Common;
using UnityEngine;

namespace Systems.InputSystems
{
	public class ClampVelocitySystem : IEcsRunSystem
	{
		private StaticData _staticData;
		private EcsFilter<Velocity, PlayerTag> _filter = null;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				ref Velocity velocity = ref _filter.Get1(index);
				velocity.Value = Vector3.ClampMagnitude(velocity.Value, _staticData.ClampVelocity);
			}
		}
	}
}