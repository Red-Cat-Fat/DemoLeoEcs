using Systems.CoreSystems.BaseGameplay;
using Systems.InputSystems;
using Systems.MoveSystems;
using Systems.Spawners;
using Components.Common.Input;
using Components.Core;
using Components.GameStates.GameplayEvents;
using Components.PhysicsEvents;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityComponents.Common;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
	private const string Coregameplay = "CoreGameplay";
	private const string Movable = "Movable";
	private const string Spawn = "Spawn";

	[SerializeField]
	private StaticData _staticData;
	[SerializeField]
	private SceneData _sceneData;
	[SerializeField] 
	private EcsUiEmitter _uiEmitter;
	
	private EcsWorld _world;
	private EcsSystems _systems;
	private EcsSystems _fixedSystem;

	private int _spawnSystems;
	private int _coreGameplaySystems;
	private int _movableSystems;

	private void Start()
	{
		_world = new EcsWorld();
		_systems = new EcsSystems(_world, "UpdateSystems");
		_fixedSystem = new EcsSystems(_world, "FixedUpdateSystems");

		InitializeObserver();
		InitializedUpdateSystems();
		InitializeFixedSystems();
		CalcSystemIndexes();
		SetGameplayState(false);
	}

	private void InitializeObserver()
	{
#if UNITY_EDITOR
		Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystem);
#endif
	}

	private void InitializedUpdateSystems()
	{
		EcsSystems inputSystems = InputSystems();
		EcsSystems spawnSystems = SpawnSystems(Spawn);

		_systems
			.Add(inputSystems)
			.Add(spawnSystems)
			.Inject(_staticData)
			.Inject(_sceneData)
			.InjectUi(_uiEmitter)
			.Init();
	}

	private void InitializeFixedSystems()
	{
		EcsSystems coreSystems = CoreGameplaySystems(Coregameplay);
		EcsSystems movableSystems = MovableSystems(Movable);

		_fixedSystem
			.Add(coreSystems)
			.Add(movableSystems)
			.OneFrame<OnCollisionEnterEvent>()
			.Inject(_sceneData)
			.Inject(_staticData)
			.Init();
	}

	private void CalcSystemIndexes()
	{
		_spawnSystems = _systems.GetNamedRunSystem(Spawn);
		_coreGameplaySystems = _fixedSystem.GetNamedRunSystem(Coregameplay);
		_movableSystems = _fixedSystem.GetNamedRunSystem(Movable);
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

	public void StartGame()
	{
		SetGameplayState(true);
	}

	private void PauseGame()
	{
		SetGameplayState(false);
	}
	
	private void SetGameplayState(bool value)
	{
		_systems.SetRunSystemState(_spawnSystems, value);
		_fixedSystem.SetRunSystemState(_coreGameplaySystems, value);
		_fixedSystem.SetRunSystemState(_movableSystems, value);
	}

	private EcsSystems SpawnSystems(string name)
	{
		return new EcsSystems(_world, name)
			.Add(new SpawnPlayer())
			.Add(new ObstacleSpawner())
			.Add(new SpawnSystem());;
	}

	private EcsSystems InputSystems()
	{
		return new EcsSystems(_world)
			.OneFrame<AnyKeyDownTag>()
			.Add(new KeyInputSystem())
			.Add(new AddVelocityInputSystem())
			.Add(new ClampVelocitySystem());;
	}

	private EcsSystems MovableSystems(string name)
	{
		return new EcsSystems(_world, name)
			.Add(new GravitationSystem())
			.Add(new MoveSystem())
			.Add(new UpdateRigidbodyPosition());
	}

	private EcsSystems CoreGameplaySystems(string name)
	{
		return new EcsSystems(_world, name)
			.OneFrame<OnObstacleCollisionEvent>()
			.Add(new ObstacleCollisionCheckerSystem())
			.OneFrame<DeadEvent>()
			.Add(new DeadByObstacleCollisionSystem())
			.Add(new DeadCheckerGameplaySystem());
	}
}