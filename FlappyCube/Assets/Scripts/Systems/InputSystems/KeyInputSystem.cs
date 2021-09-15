using Components.Common.Input;
using Components.Objects.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.InputSystems
{
	public class KeyInputSystem : IEcsRunSystem
	{
		private EcsWorld _world = null;
		private EcsFilter<PlayerTag> _filter = null;

		public void Run()
		{
			var isHasInput = Input.anyKeyDown;
			if (!isHasInput)
			{
				return;
			}

			foreach (int index in _filter)
			{
				ref EcsEntity entity = ref _filter.GetEntity(index);
				entity.Get<AnyKeyDownTag>();
			}
		}
	}
}