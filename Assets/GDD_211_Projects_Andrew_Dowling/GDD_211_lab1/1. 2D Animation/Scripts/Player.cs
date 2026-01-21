using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private SpriteRenderer sr;

    void Update()
    {
        float walkSpeed = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);

        if(walkSpeed != 0) // Walking
		{
            ani.Play("walk");

            if(walkSpeed > 0)
			{
                sr.flipX = false;
			}
			else
			{
                sr.flipX = true;
            }
		}
		else // Not moving
		{
            ani.Play("idle");
        }
    }
}
