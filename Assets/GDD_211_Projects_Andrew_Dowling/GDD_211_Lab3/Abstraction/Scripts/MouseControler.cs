using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Abstraction
{
    public class MouseControler : MonoBehaviour
    {
		private Item activeItem = null;

		private void Update()
		{
			RaycastHit hit;
			if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				Item item = hit.collider.GetComponent<Item>();
				if (item != null)
				{
					if(activeItem != item)
					{
						ExitActiveItem();
					}
					activeItem = item;
					item.HandleMouseOver();
				}
			}
			else
			{
				if (activeItem != null)
				{
					ExitActiveItem();
				}
			}
		}

		private void ExitActiveItem()
		{
			if(activeItem != null)
			{
				activeItem.HandleMouseExit();
				activeItem = null;
			}
		}
	}
}