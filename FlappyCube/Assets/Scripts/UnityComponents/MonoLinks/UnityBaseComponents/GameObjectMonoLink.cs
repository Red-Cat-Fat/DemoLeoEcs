using Components.Common.MonoLinks;
using Leopotam.Ecs;
using UnityComponents.MonoLinks.Base;

namespace UnityComponents.MonoLinks.UnityBaseComponents
{
    public class GameObjectMonoLink : MonoLink<GameObjectLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Get<GameObjectLink>() = new GameObjectLink
            {
                Value = gameObject
            };
        }
    }
}