using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Inheritance
{ 
	public class Boombox : Item
	{
		public override void PickUp()
		{
			FindObjectOfType<TMP_Text>().text += $"$NOT FOR SALE! - {itemName}\n";
			this.enabled = false;
		}
	}
}
