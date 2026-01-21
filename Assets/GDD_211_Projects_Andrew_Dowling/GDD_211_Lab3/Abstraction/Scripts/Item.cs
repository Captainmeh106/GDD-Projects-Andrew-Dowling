using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Abstraction
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected string itemName;

        protected bool activeItem;

		private Vector3 startPos;
		private float y;

        protected abstract void Use();

		private void Awake()
		{
			startPos = transform.position;
		}

		protected virtual void Update()
		{
			if (activeItem)
			{
				y += Time.deltaTime;
				transform.position = startPos + new Vector3(0, Mathf.Abs( Mathf.Sin(y * 2f) * 0.05f), 0);
				transform.eulerAngles += new Vector3(0, Time.deltaTime * 60	, 0);
			}
			else
			{
				transform.position = startPos;
			}
		}

		public virtual void HandleMouseOver()
        {
            if (!activeItem)
			{
				Debug.Log("MouseOver" + itemName);
				activeItem = true;
				y = 0;
			}
        }

		public virtual void HandleMouseExit()
		{
			if (activeItem)
			{
				Debug.Log("MouseExit" + itemName);
				activeItem = false;
			}
		}
	}
}