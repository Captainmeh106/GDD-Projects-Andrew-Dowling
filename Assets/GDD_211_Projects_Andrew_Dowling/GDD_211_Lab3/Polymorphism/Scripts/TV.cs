using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Polymorphism
{
    public class TV : Item
    {
		private void OnMouseDown()
		{
			Inspect(cost);
		}
	}
}