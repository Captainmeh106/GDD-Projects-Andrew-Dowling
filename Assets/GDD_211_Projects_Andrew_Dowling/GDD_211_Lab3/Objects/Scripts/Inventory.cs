using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
	public class Inventory
	{
		public int Gold { get { return gold; } }
		public int Silver { get { return silver; } }

		private int gold;
		private int silver;

		public Inventory()
		{
			gold = 5;
			silver = 10;
		}

		public void AddGold(int amnt)
		{
			gold += amnt;
		}

		public void DropAll()
		{
			gold = 0;
			silver = 0;
		}
	}

}