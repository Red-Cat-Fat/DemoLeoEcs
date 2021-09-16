using UnityComponents.Factories;
using UnityEngine;

namespace UnityComponents.Common
{
	public class SceneData : MonoBehaviour
	{
		public PrefabFactory Factory;
		public Transform SpawnObstaclePosition;

		public GameHud Hud;
	}
}