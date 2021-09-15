using UnityEngine;

namespace Components.PhysicsEvents
{
	public struct OnTriggerEnterEvent
	{
		public GameObject Sender;
		public Collider Collider;
	}
}