using UnityEngine;

public class TVStatic : MonoBehaviour
{

	[SerializeField] private Texture2D[] frames;
	[SerializeField] private float delay;

	private int index;
	private Material mat;

	void Start()
	{
		mat = GetComponent<Renderer>().material;
		InvokeRepeating(nameof(ChangeFrame), delay, delay);
	}
	
	private void ChangeFrame()
	{
		index++;
		if (index == frames.Length) index = 0;
		mat.mainTexture = frames[index];
	}
}