using TMPro;
using UnityEngine;

namespace Game.Inheritance
{
    public class Item : MonoBehaviour
    {
		[SerializeField] protected string itemName;
		[SerializeField] private float cost;

		public virtual void PickUp()
		{
			FindObjectOfType<TMP_Text>().text += $"${cost} - {itemName}\n";
			Destroy(gameObject);
		}

		private void OnMouseDown()
		{
			PickUp();
		}
	}
}