using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Abstraction
{
    public class Camera : Item
    {
        [SerializeField] private Light flash;
		[SerializeField] private float maxBright;
		[SerializeField] private float falloff;
		[SerializeField] private UnityEngine.Camera photoCam;
		[SerializeField] private Animator photoAnimator;
		[SerializeField] private float snapDelay;
		private float intensity;
		private float delay;

		protected override void Update()
		{
			base.Update();
			if(intensity > 0.1f)
			{
				intensity = Mathf.Lerp(intensity, 0, Time.deltaTime * falloff);
				flash.intensity = intensity;
			}
			if(delay > 0)
			{
				delay -= Time.deltaTime;
			}
		}

		public override void HandleMouseOver()
		{
			base.HandleMouseOver();
			Use();
		}

		public override void HandleMouseExit()
		{
			base.HandleMouseExit();
		}

		private void TakePhoto()
		{
			if (!photoCam.enabled) { photoCam.enabled = true; Invoke(nameof(TakePhoto), 0.05f); }
			else if (photoCam.enabled) { photoCam.enabled = false; }
		}

		protected override void Use()
		{
			if (delay > 0) return;
			intensity = maxBright;
			flash.intensity = intensity;
			Invoke(nameof(TakePhoto), 0.05f);
			delay = snapDelay;
			photoAnimator.Play("snap");
		}
	}
}
