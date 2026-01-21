using UnityEngine;

public class PlayerWalkDemo : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private SpriteRenderer spriteRenderer;

	private void Update()
	{
		if(Input.GetAxisRaw("Horizontal") > 0)
		{	
			animator.Play("PlayerWalk");
			transform.position += new Vector3(Time.deltaTime, 0, 0);
			spriteRenderer.flipX = false;
		}
		else if (Input.GetAxisRaw("Horizontal") < 0)
		{
			animator.Play("PlayerWalk");
			transform.position += new Vector3(-Time.deltaTime, 0, 0);
			spriteRenderer.flipX = true;
		}
		else
		{
			animator.Play("PlayerIdle");
		}
	}
}