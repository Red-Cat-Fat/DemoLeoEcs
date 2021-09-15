using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Demo
{
	public class DemoSystem :IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsPostDestroySystem
	{
		public void PreInit()
		{
			Debug.Log("PreInit is analogue Awake");
		}

		public void Init()
		{
			Debug.Log("Init is analogue Start");
		}

		public void Run()
		{
			Debug.Log("Run is analogue Update");
		}

		public void Destroy()
		{
			Debug.Log("Destroy is analogue Destroy");
		}

		public void PostDestroy()
		{
			Debug.Log("PostDestroy are no analogues in Unity. Called after destruction");
		}
	}
}