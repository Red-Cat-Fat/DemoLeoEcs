using Components.Common.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.InputSystems
{
	public class KeyInputSystem : IEcsRunSystem
	{
		private EcsWorld _world = null;

		public void Run()
		{
			if (Input.anyKeyDown)
			{
				_world.NewEntity().Get<AnyKeyDownTag>(); //read it in DemoSystem
			}
		}
	}
}