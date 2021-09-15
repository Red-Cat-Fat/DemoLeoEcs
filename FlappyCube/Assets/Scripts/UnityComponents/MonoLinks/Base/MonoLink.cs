using Leopotam.Ecs;

namespace UnityComponents.MonoLinks.Base
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