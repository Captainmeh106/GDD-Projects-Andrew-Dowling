using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	public float Speed;

	private void Update()
	{
		transform.Rotate(new Vector3(0f, Speed * Time.deltaTime, 0f));
	}
}
