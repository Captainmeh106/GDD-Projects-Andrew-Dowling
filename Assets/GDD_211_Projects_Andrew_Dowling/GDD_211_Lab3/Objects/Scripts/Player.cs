using TMPro;
using UnityEngine;

namespace Game.Objects
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private TMP_Text text;

		private Inventory inventory = new Inventory();  // Create a new inventory object, Inventory constructor initiailizes object

		private void Start()
		{
			text.text += $"<color=#767676>Starting...</color>\n";
			text.text += $"Gold: {inventory.Gold}\n";
			text.text += $"Gold: {inventory.Silver}\n";
			text.text += $"<color=#767676>Adding 5 gold...</color>\n";
			
			inventory.AddGold(5);
			text.text += $"Gold: {inventory.Gold}\n";
			text.text += $"Gold: {inventory.Silver}\n";
			text.text += $"<color=#767676>Emptying...</color>\n";

			inventory.DropAll();
			text.text += $"Gold: {inventory.Gold}\n";
			text.text += $"Gold: {inventory.Silver}\n";

		}
	}
}
