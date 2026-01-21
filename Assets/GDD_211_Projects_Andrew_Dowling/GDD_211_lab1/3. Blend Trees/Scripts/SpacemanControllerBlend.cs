using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanControllerBlend : MonoBehaviour
{
    [SerializeField] private Animator animator;
	[SerializeField] private float walkPower;
	[SerializeField] private float walkSpeedMax = 1f;
	[SerializeField] private float aniSpeedMax = 1f;

	private float facing = 1;
	private float walkSpeed = 0f;
	private float aniSpeed = 0f;
	
	private void Update()
	{
		float input = (Input.GetAxisRaw("Horizontal"));

		facing = input;

		// Movement
		if (input != 0)
		{
			walkSpeed += input * walkPower * Time.deltaTime;
			if (Mathf.Abs(walkSpeed) > walkSpeedMax)
			{
				walkSpeed = walkSpeedMax * Mathf.Sign(walkSpeed);
			}
		}
		else
		{
			walkSpeed *= 1 - (walkPower * 500 * Time.deltaTime); // Slow down 500x faster than accelerating.
		}
		transform.position += new Vector3(walkSpeed, 0f);

		// Animation
		aniSpeed = Mathf.Lerp(0, aniSpeedMax, Mathf.Abs(walkSpeed) / walkSpeedMax); // Remap walkSpeed onto aniSpeed.

		animator.SetFloat("speed", aniSpeed);

		// Rotate
		if (facing < 0)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 270f, 0f), Time.deltaTime * 4f);
		}
		else if (facing > 0)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 90f, 0f), Time.deltaTime * 4f);
		}

	}
}
