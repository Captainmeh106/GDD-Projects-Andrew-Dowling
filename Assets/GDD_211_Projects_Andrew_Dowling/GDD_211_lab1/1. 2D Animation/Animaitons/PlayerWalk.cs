using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
	[SerializeField] private Animator ani;
	[SerializeField] private SpriteRenderer sprRend;

	private void Update()
	{
		//Get input
		float walk = Input.GetAxis("Horizontal");

		//Animate
		if(walk != 0)
		{
			ani.Play("walk");
			transform.position += new Vector3(walk * Time.deltaTime, 0, 0);

			if(walk < 0) { sprRend.flipX = true; }
			else if(walk > 0) { sprRend.flipX = false; }
		}
		else
		{
			ani.Play("idle");
		}
	}
}
