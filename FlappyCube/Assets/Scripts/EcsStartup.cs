using Systems.Demo;
using Systems.InputSystems;
using Systems.Spawners;
using Components.Common.Input;
using Leopotam.Ecs;
using UnityComponents.Common;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
	[SerializeField]
	private StaticData _staticData;
	[SerializeField]
	private SceneData _sceneData;
	
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
			.OneFrame<AnyKeyDownTag>()
			.Add(new KeyInputSystem())
			.Add(new SpawnPlayer())
			.Add(new SpawnSystem())
			.Add(new DemoSystem())
			.Inject(_staticData)
			.Inject(_sceneData)
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