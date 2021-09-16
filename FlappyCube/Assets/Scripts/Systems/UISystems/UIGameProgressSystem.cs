using Components.GameStates;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UISystems
{
	public class UIGameProgressSystem : IEcsRunSystem
	{
		private const string Startgamebtn = "StartGameBtn";
		
		private EcsFilter<EcsUiClickEvent> _filter = null;

		private EcsFilter<GameProgress> _filterGameProgress = null;
		// auto-injected fields.
		[EcsUiNamed(Startgamebtn)] GameObject _btnGo;
		[EcsUiNamed(Startgamebtn)] Transform _btnTransform;
		[EcsUiNamed(Startgamebtn)] Button _btn;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				EcsUiClickEvent click = _filter.Get1(index);
				if (click.WidgetName.Equals(Startgamebtn))
				{
					ref GameProgress gameProgress = ref _filterGameProgress.Get1(0);
					gameProgress.IsPause = false;
				}
			}
		}
	}
}