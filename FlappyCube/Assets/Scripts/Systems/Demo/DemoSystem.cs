using Components.Common.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Demo
{
	public class DemoSystem :IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsPostDestroySystem
	{
		private EcsFilter<AnyKeyDownTag> _inputFilter = null;
		
		public void PreInit()
		{
		}

		public void Init()
		{
		}

		public void Run()
		{
			if (!_inputFilter.IsEmpty())
			{
				Debug.Log("The button was pressed");
			}
		}

		public void Destroy()
		{
		}

		public void PostDestroy()
		{
		}
	}
}