using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Statics
{
	public class CamFollow : MonoBehaviour
	{
		private Vector3 vel = Vector3.one;

		private void LateUpdate()
		{
			Vector3 target = Player.Instance.transform.position + Player.Instance.transform.right * 0.5f;
			target.z = transform.position.z;

			transform.position = Vector3.SmoothDamp(
				transform.position, 
				target, 
				ref vel, 
				0.25f);
		}
	}

}
