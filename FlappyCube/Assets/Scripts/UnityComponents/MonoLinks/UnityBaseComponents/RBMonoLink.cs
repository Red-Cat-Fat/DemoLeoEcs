using System;
using Components.Common.MonoLinks;
using UnityComponents.MonoLinks.Base;
using UnityEngine;

namespace UnityComponents.MonoLinks.UnityBaseComponents
{
    [RequireComponent(typeof(Rigidbody))]
    public class RBMonoLink : MonoLink<RigidbodyLink>
    {
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Value.Value == null)
            {
                Value = new RigidbodyLink
                {
                    Value = GetComponent<Rigidbody>()
                };
            }
        }
#endif
    }
}