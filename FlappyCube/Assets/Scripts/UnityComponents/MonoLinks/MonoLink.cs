using Leopotam.Ecs;
using UnityEngine;

namespace UnityComponents.MonoLinks
{
	public class MonoLink<T> : MonoLinkBase where T : struct
	{
		public T Value;
        
		public override void Make(ref EcsEntity entity)
		{
			entity.Get<T>() = Value;
		}
	}
}