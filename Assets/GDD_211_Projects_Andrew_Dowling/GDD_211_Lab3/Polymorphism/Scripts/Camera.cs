using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Polymorphism
{
	public class Camera : Item
	{
		private void OnMouseDown()
		{
			Inspect(cost);
		}
	}
}