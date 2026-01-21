using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
	[SerializeField] private ParticleSystem ps_flames;

	private ParticleSystem.EmissionModule module;

	private void Start()
	{
		module = ps_flames.emission;
		module.rateOverTime = 1000;
	}
}
