using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Polymorphism
{
	public class Drill : Item
	{
		private void OnMouseDown()
		{
			Inspect(yearBuilt);
		}
	}
}