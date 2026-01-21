using TMPro;
using UnityEngine;

namespace Game.Encapsulation
{
    public class TV : MonoBehaviour
    {
		public string Description { get { return description; } }  // Properties
		public int CurrentChannel { get { return currentChannel; } }

		[SerializeField] private string description;                // Fields
		[SerializeField] private int maxChannels;
		[SerializeField] private TMP_Text channelText;
		[SerializeField] private TMP_Text muteText;
		[SerializeField] private AudioSource asource;
		private int currentChannel = 1;

		/// <summary>
		/// Increment or decrement the currentChannel by 1, looping around at the min/max
		/// </summary>
		/// <param name="direction">Positive or negative value to indicate channel change direction</param>
		public void ChangeChannel(int direction)
		{
			currentChannel += System.Math.Sign(direction);					// Change value + or - 1
			if(currentChannel > maxChannels) { currentChannel = 1; }		// If channel exceeds max channels, go to 1
			else if(currentChannel <= 0) { currentChannel = maxChannels; } // If channel is less than 1, go to max channel
			channelText.text = (currentChannel < 10 ? "0" : "") + currentChannel.ToString();
		}

		/// <summary>
		/// Toggles static sound and mute text
		/// </summary>
		public void ToggleVolume()
		{
			if (asource.isPlaying)
			{
				asource.Stop();
				muteText.enabled = true;
			}
			else
			{
				asource.Play();
				muteText.enabled = false;
			}
		}
	}
}

