using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Polymorphism
{
    public abstract class Item : MonoBehaviour
    {
		[SerializeField] private string itemName;
        [SerializeField] protected int cost;
        [SerializeField] protected string yearBuilt;

		protected void Inspect(int cost)
		{
			FindObjectOfType<TMP_Text>().text += $"${cost} - {itemName}\n";
		}

		protected void Inspect(string yearBuilt)
		{
			FindObjectOfType<TMP_Text>().text += $"{itemName} built {yearBuilt}\n";
		}
	}
}