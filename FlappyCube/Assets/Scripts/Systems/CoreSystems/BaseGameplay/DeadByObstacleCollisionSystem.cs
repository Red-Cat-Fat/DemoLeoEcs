using Components.Core;
using Components.GameStates.GameplayEvents;
using Components.PhysicsEvents;
using Leopotam.Ecs;

namespace Systems.CoreSystems.BaseGameplay
{
	public class DeadByObstacleCollisionSystem : IEcsRunSystem
	{
		private EcsWorld _world = null;
		private EcsFilter<OnObstacleCollisionEvent> _filter = null;
		
		public void Run()
		{
			if (!_filter.IsEmpty())
			{
				_world.NewEntity().Get<DeadEvent>();
			}
		}
	}
}