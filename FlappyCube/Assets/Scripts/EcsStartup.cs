using Systems.CoreSystems.BaseGameplay;
using Systems.InputSystems;
using Systems.MoveSystems;
using Systems.Spawners;
using Systems.UISystems;
using Components.Common.Input;
using Components.Core;
using Components.GameStates.GameplayEvents;
using Components.PhysicsEvents;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Services;
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

	private PauseService _pauseService;
	
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
		
		InitializedServices();
		InitializeObserver();
		
		InitializedUpdateSystems();
		InitializeFixedSystems();
		
		CalcSystemIndexes();

		Subscribe();
		
		SetGameplayState(false);
	}

	private void InitializedServices()
	{
		InitializePauseService(true);
	}

	private void InitializePauseService(bool startState)
	{
		_pauseService = new PauseService(startState);
	}
	
	private void OnChangePauseState(bool isPause)
	{
		SetGameplayState(!isPause);
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
		EcsSystems uiSystems = UISystems();
		
		_systems
			.Add(uiSystems)
			.Add(inputSystems)
			.Add(spawnSystems)
			.Inject(_staticData)
			.Inject(_sceneData)
			.InjectUi(_uiEmitter)
			.Inject(_pauseService)
			.Init();
	}

	private EcsSystems UISystems()
	{
		return new EcsSystems(_world)
			.Add(new UIGameProgressSystem());
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
			.Inject(_pauseService)
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
		if (_fixedSystem != null)
		{
			_fixedSystem.Destroy();
			_fixedSystem = null;
		}

		if (_systems != null)
		{
			_systems.Destroy();
			_systems = null;
		}

		if (_world != null)
		{
			_world.Destroy();
			_world = null;
		}

		Unsubscribe();
	}

	private void SetGameplayState(bool value)
	{
		_systems.SetRunSystemState(_spawnSystems, value);
		_fixedSystem.SetRunSystemState(_coreGameplaySystems, value);
		_fixedSystem.SetRunSystemState(_movableSystems, value);
	}
	
	private void Subscribe()
	{
		_pauseService.ChangeStateEvent += OnChangePauseState;
	}
	
	private void Unsubscribe()
	{
		_pauseService.ChangeStateEvent -= OnChangePauseState;
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