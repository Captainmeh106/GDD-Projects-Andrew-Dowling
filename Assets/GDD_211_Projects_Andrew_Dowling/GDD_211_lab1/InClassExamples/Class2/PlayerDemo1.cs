using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDemo1 : MonoBehaviour
{
	const string SPEED = "Speed";

	[SerializeField] private Animator ani;

	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Animator[] allAnimators = FindObjectsOfType<Animator>();
			foreach(Animator animator in allAnimators)
			{
				animator.Play("Start");
			}
		}
	}
}
