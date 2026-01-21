using TMPro;
using UnityEngine;

namespace Game.Abstraction
{
    public class TV : Item
    {
		[SerializeField] private AudioSource asource;
		[SerializeField] private MeshRenderer staticRenderer;
		[SerializeField] private GameObject tvCanvas;
		[SerializeField] private AudioClip sfx_static;

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
			if (asource.isPlaying) asource.Pause();
			staticRenderer.enabled = false;
			tvCanvas.SetActive(false);
		}

		protected override void Use()
		{
			if (!asource.isPlaying) asource.Play();
			staticRenderer.enabled = true;
			tvCanvas.SetActive(true);
		}
	}
}

