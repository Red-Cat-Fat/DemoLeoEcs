using Components.Common.Input;
using Components.Objects.Moves;
using Leopotam.Ecs;
using UnityComponents.Common;

namespace Systems.InputSystems
{
	public class AddVelocityInputSystem : IEcsRunSystem
	{
		private StaticData _staticData;
		private EcsFilter<AnyKeyDownTag> _filter = null;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				ref EcsEntity entity = ref _filter.GetEntity(index);
				ref Velocity velocity = ref entity.Get<Velocity>();
				velocity.Value += _staticData.PlayerAddForce;
			}
		}
	}
}