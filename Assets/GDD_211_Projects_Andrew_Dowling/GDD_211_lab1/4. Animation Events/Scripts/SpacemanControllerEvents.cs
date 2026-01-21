using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemanControllerEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
	[SerializeField] private float walkSpeed;
	[SerializeField] private ParticleSystem smoke_leftFoot;
	[SerializeField] private ParticleSystem smoke_rightFoot;
	[SerializeField] private Light pointLight;
	[SerializeField] private Light spotLight;

	private float facing = 1;

	private void Update()
	{
		float input = (Input.GetAxisRaw("Horizontal"));

		facing = input;

		if (facing < 0)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 270f, 0f), Time.deltaTime * 4f);
		}
		else if(facing > 0)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 90f, 0f), Time.deltaTime * 4f);
		}

		transform.position += new Vector3(input * walkSpeed * Time.deltaTime, 0f);

		animator.SetFloat("MoveSpeed", Mathf.Abs(input));
	}

	public void CreateSmoke(int side)
	{
		if(side < 0)
		{
			smoke_leftFoot.Emit(5);
		}
		else
		{
			smoke_rightFoot.Emit(5);
		}
	}

	public void LightFlicker()
	{
		StartCoroutine(LightFlickerCoroutine());
	}

	private void Test()
	{
		Debug.Log("Animation Event");
	}

	private IEnumerator LightFlickerCoroutine()
	{
		float pointlightStart = pointLight.intensity;
		float spotlightStart = spotLight.intensity;

		pointLight.intensity =  pointlightStart * Random.Range(0.1f, 0.5f);
		spotLight.intensity = spotlightStart * Random.Range(0.1f, 0.5f);
		yield return new WaitForSeconds(0.1f);
		pointLight.intensity = pointlightStart * Random.Range(0.5f, 0.9f);
		spotLight.intensity = spotlightStart * Random.Range(0.5f, 0.9f);
		yield return new WaitForSeconds(0.2f);
		pointLight.intensity = pointlightStart * Random.Range(0.1f, 0.5f);
		spotLight.intensity = spotlightStart * Random.Range(0.1f, 0.5f);
		yield return new WaitForSeconds(0.2f);
		pointLight.intensity = pointlightStart * Random.Range(0.5f, 0.9f);
		spotLight.intensity = spotlightStart * Random.Range(0.5f, 0.9f);
		yield return new WaitForSeconds(0.1f);
		pointLight.intensity = pointlightStart * Random.Range(0.1f, 0.5f);
		spotLight.intensity = spotlightStart * Random.Range(0.1f, 0.5f);
		yield return new WaitForSeconds(0.2f);
		pointLight.intensity = pointlightStart * Random.Range(0.1f, 0.5f);
		spotLight.intensity = spotlightStart * Random.Range(0.1f, 0.5f);
		yield return new WaitForSeconds(0.2f);

		pointLight.intensity = pointlightStart;
		spotLight.intensity = spotlightStart;
	}

}
