using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CineExample
{
    public class PlayerWalk : MonoBehaviour
    {
        [SerializeField] private float speed;
		[SerializeField] private CharacterController controller;

		private void Update()
		{
			transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));

			Vector3 move = transform.forward * Input.GetAxis("Vertical") + 
				transform.right * Input.GetAxis("Horizontal");

			controller.Move(move * speed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			VirtualCameraTrigger trigger = other.GetComponent<VirtualCameraTrigger>();
			if(trigger != null) 
			{
				trigger.ActivateCamera(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			VirtualCameraTrigger trigger = other.GetComponent<VirtualCameraTrigger>();
			if (trigger != null)
			{
				trigger.ActivateCamera(false);
			}
		}
	}
}