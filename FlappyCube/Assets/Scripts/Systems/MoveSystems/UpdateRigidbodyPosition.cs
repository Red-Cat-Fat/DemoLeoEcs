using Components.Common.MonoLinks;
using Components.Objects;
using Leopotam.Ecs;

namespace Systems.MoveSystems
{
	public class UpdateRigidbodyPosition : IEcsRunSystem
	{
		private EcsFilter<RigidbodyLink, Position> _filter = null;
		
		public void Run()
		{
			if (_filter.IsEmpty())
			{
				return;
			}
			
			foreach (int index in _filter)
			{
				ref RigidbodyLink rigidbody = ref _filter.Get1(index);
				var newPosition = _filter.Get2(index);
				
				rigidbody.Value.MovePosition(newPosition.Value);
			}
		}
	}
}