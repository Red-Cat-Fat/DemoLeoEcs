using System;
using Systems.Demo;
using Systems.InputSystems;
using Systems.MoveSystems;
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
	private EcsSystems _fixedSystem;
	private void Start()
	{
		_world = new EcsWorld();
		_systems = new EcsSystems(_world, "UpdateSystems");
		_fixedSystem = new EcsSystems(_world, "FixedUpdateSystems");
		
#if UNITY_EDITOR
		Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystem);
#endif
		EcsSystems inputSystems = InputSystems();
		EcsSystems spawnSystems = SpawnSystems();
		
		_systems
			.Add(inputSystems)
			.Add(spawnSystems)
			.Inject(_staticData)
			.Inject(_sceneData)
			.Init();

		_fixedSystem
			.Add(new GravitationSystem())
			.Add(new MoveSystem())
			.Add(new UpdateRigidbodyPosition())
			.Inject(_staticData)
			.Init();
	}

	private EcsSystems SpawnSystems()
	{
		EcsSystems spawnSystems = new EcsSystems(_world)
			.Add(new SpawnPlayer())
			.Add(new ObstacleSpawner())
			.Add(new SpawnSystem());
		return spawnSystems;
	}

	private EcsSystems InputSystems()
	{
		EcsSystems inputSystems = new EcsSystems(_world)
			.OneFrame<AnyKeyDownTag>()
			.Add(new KeyInputSystem())
			.Add(new AddVelocityInputSystem());
		return inputSystems;
	}

	private void Update()
	{
		_systems?.Run();
	}

	private void FixedUpdate()
	{
		_fixedSystem?.Run();
	}

	private void OnDestroy()
	{
		if (_systems != null)
		{
			_systems.Destroy();
			_systems = null;
			
			_fixedSystem.Destroy();
			_fixedSystem = null;
			
			_world.Destroy();
			_world = null;
		}
	}
}