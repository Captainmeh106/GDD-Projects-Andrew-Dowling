using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanController3D : MonoBehaviour
{
	[SerializeField] private Animator ani;

	private void Update()
	{
		//Get input
		float walk = Input.GetAxis("Horizontal");

		//Animate
		if (walk != 0)
		{
			ani.SetFloat("WalkSpeed", 1);
			transform.position += new Vector3(walk * Time.deltaTime, 0, 0);
		}
		else
		{
			ani.SetFloat("WalkSpeed", 0);
		}
	}
}
