using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanController2D : MonoBehaviour
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	private float walkSpeed;

	private void Update()
	{
		float moveInput = Input.GetAxisRaw("Horizontal");
		if(moveInput == 0)
		{
			Idle();
		}
		else
		{
			Walk(moveInput);
		}
	}

	private void Idle()
	{
		if (walkSpeed != 0)
		{
			animator.Play("Idle");
			walkSpeed = 0;
		}
	}

	private void Walk(float move)
	{
		if (walkSpeed == 0)
		{
			animator.Play("Walk");
			walkSpeed = move;
		}

		if (walkSpeed < 0 && !spriteRenderer.flipX)
			spriteRenderer.flipX = true;
		if (walkSpeed > 0 && spriteRenderer.flipX)
			spriteRenderer.flipX = false;

		transform.position += new Vector3(move * Time.deltaTime, 0f);
	}
}
