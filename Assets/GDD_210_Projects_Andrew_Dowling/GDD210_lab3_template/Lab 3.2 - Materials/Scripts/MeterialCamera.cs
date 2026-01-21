using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterialCamera : MonoBehaviour
{
	public Camera Cam;

	private int index = 0;
	private bool zoom = false;

	private void Update()
	{
		int move = 0;
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			index = Mathf.Max(index - 1, 0);
			zoom = false;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			index = Mathf.Min(index + 1, 6);
			zoom = false;
		}
		transform.position = Vector3.Lerp(transform.position, new Vector3(index * 5f, transform.position.y, transform.position.z), Time.deltaTime * 5f);

		//Zooming
		if (Input.GetKeyDown(KeyCode.Space))
		{
			zoom = !zoom;
		}

		if(zoom)
		{ 
			Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, 33f, Time.deltaTime * 2f);
		}
		else
		{
			Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, 60f, Time.deltaTime * 4f);
		}
	}
}
