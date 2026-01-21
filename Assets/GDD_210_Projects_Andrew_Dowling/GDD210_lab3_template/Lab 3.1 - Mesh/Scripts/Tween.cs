using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
	public Vector3 Position1;
	public Vector3 Position2;

	private int target = 1;

	private void Update()
	{
		if(target == 2)
		{
			transform.position = Vector3.Lerp(transform.position, Position2, Time.deltaTime * 0.1f);
			if (Vector3.Distance(transform.position, Position2) < 1f)
				target = 1;
		}
		else if (target == 1)
		{
			transform.position = Vector3.Lerp(transform.position, Position1, Time.deltaTime * 0.1f);
			if (Vector3.Distance(transform.position, Position1) < 1f)
				target = 2;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(Position1, 0.5f);
		Gizmos.DrawWireSphere(Position2, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Position1, Position2);
	}
}
