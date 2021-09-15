using Components.Objects;
using Leopotam.Ecs;

namespace Systems.MoveSystems
{
	public class MoveSystem : IEcsRunSystem
	{
		private EcsFilter<Velocity> _filter = null;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				ref EcsEntity entity = ref _filter.GetEntity(index);
				ref Position position = ref entity.Get<Position>();
				Velocity velocity = _filter.Get1(index);

				position.Value += velocity.Value;
			}
		}
	}
}