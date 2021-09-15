using UnityEngine;

namespace UnityComponents.Common
{
	[CreateAssetMenu(menuName = "Config/StaticData", fileName = "StaticData", order = 0)]
	public class StaticData : ScriptableObject
	{
		public GameObject PlayerPrefab;
		public Vector3 GlobalGravitation;
	}
}