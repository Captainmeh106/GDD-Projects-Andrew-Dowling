using TMPro;
using UnityEngine;

namespace Game.Abstraction
{
	public class Drill : Item
	{
		[SerializeField] private Light[] lights;

		private void Start()
		{
			HandleMouseExit();
		}

		public override void HandleMouseOver()
		{
			base.HandleMouseOver();
			Use();
		}

		public override void HandleMouseExit()
		{
			base.HandleMouseExit();
			foreach (Light light in lights) { light.enabled = false; }
		}

		protected override void Use()
		{
			foreach (Light light in lights) { light.enabled = true; }
		}
	}
}

