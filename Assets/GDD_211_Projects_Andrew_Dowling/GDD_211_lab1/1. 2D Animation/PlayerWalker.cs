using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalker : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private SpriteRenderer spriteRenderer;

	private void Update()
	{
		if(Input.GetAxisRaw("Horizontal") > 0) //Right
		{
			animator.Play("playerWalk");
			transform.position += new Vector3(Time.deltaTime,0,0);
			spriteRenderer.flipX = false;
		}
		else if (Input.GetAxisRaw("Horizontal") < 0) //Left
		{
			animator.Play("playerWalk");
			transform.position += new Vector3(-Time.deltaTime, 0, 0);
			spriteRenderer.flipX = true;
		}
		else
		{
			animator.Play("playerIdle");
		}
	}
}
