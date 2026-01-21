using UnityEngine;
using UnityEngine.Events;

namespace Game.Encapsulation
{
    public class Knob : MonoBehaviour
    {
		[SerializeField] private TV tv;						// The TV object
		[SerializeField] private int direction;				// Direction to change channel
		[SerializeField] private AudioClip sfx_turnKnob;    // Sound effect for turning knob
		[SerializeField] private AudioSource asource;		//Audiosource to play sound on
		[SerializeField] private UnityEvent<int> onClick;	//Function to call on click

		private void OnMouseDown()
		{
			onClick?.Invoke(direction);
			asource.PlayOneShot(sfx_turnKnob);
			asource.time = Random.Range(0, asource.clip.length);
			asource.volume = Random.Range(0.2f, 0.3f);
		}

	}
}