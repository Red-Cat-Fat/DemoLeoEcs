using System;
using Components.PhysicsEvents;
using Leopotam.Ecs;
using UnityComponents.MonoLinks.Base;
using UnityEngine;

namespace UnityComponents.MonoLinks.PhysicsLinks
{
	public class OnTriggerEnterMonoLink : PhysicsLinkBase
	{
		private void OnTriggerEnter(Collider other)
		{
			_entity.Get<OnTriggerEnterEvent>() = new OnTriggerEnterEvent()
			{
				Collider = other,
				Sender = gameObject
			};
		}
	}
}