using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
	[Range(0f,100f)] public float Speed;


	private void Update()
	{
		transform.Rotate(new Vector3(Speed, Speed, Speed) * Time.deltaTime);
	}
}
