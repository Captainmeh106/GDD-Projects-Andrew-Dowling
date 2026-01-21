using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Statics
{
    public class Powerup : MonoBehaviour
    {
		public int Power { get { return power; } }

		[SerializeField] private SpriteRenderer sprRend;

		private int power; // 0 = gun boost, 1 = enemy slows
		private float life;

		private void Start()
		{
			life = 0;
		}

		private void Update()
		{
			if(Time.time > life)
			{
				Setup();
			}
		}

		public void Setup()
		{
			GetComponent<Collider2D>().enabled = true;
			sprRend.enabled = true;
			power = Random.Range(0, 10);
			transform.position = 
				Player.Instance.transform.position + 
				(Vector3)Random.insideUnitCircle.normalized * Random.Range(4f,5f);
			life = Time.time + Random.Range(6.5f,8f);

			if (power == 0)
			{
				sprRend.color = Color.cyan;
			}
			else if(power == 1)
			{
				sprRend.color = Color.green;
			}
			else
			{
				sprRend.color = Color.yellow;
			}
		}

		public void Pickup()
		{
			GetComponent<Collider2D>().enabled = false;
			sprRend.enabled = false;
		}
	}
}