using Leopotam.Ecs;
using UnityEngine;

namespace UnityComponents.MonoLinks
{
	public abstract class MonoLinkBase : MonoBehaviour
	{
		public abstract void Make(ref EcsEntity entity);
	}
}