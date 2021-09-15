using Systems.Demo;
using Leopotam.Ecs;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
	private EcsWorld _world;
	private EcsSystems _systems;

	private void Start()
	{
		_world = new EcsWorld();
		_systems = new EcsSystems(_world);
		
#if UNITY_EDITOR
		Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
		_systems
			.Add(new DemoSystem())
			.Init();
	}

	private void Update()
	{
		_systems?.Run();
	}

	private void OnDestroy()
	{
		if (_systems != null)
		{
			_systems.Destroy();
			_systems = null;
			_world.Destroy();
			_world = null;
		}
	}
}