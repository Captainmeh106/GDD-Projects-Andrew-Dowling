using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Statics
{
    public class Player : MonoBehaviour
    {
		public static Player Instance { get { return instance; } }	// Singleton, all other classes can reference the instance of this class.

		private static Player instance;

		[SerializeField] private float fireDelay;
		[SerializeField] private TMP_Text scoreText;
		[SerializeField] private Image ammoImg;

		private float delay;
		private Collider2D[] hits;
		private int boostAmmo;
		private int maxBoostAmmo = 100;
		private int score;
		private float nextBoostAmmo = 0.5f;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			delay = fireDelay;
			ammoImg.fillAmount = (float)boostAmmo / (float)maxBoostAmmo;
		}

		private void Update()
		{
			transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime;

			Vector3 rot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 10f);

			if (delay > 0)
			{
				delay -= Time.deltaTime;
			}
			else
			{
				if (Input.GetMouseButton(0))
				{
					delay = fireDelay;
					nextBoostAmmo = 0.4f;
					

					if (boostAmmo > 0)
					{
						boostAmmo--;
						delay *= 0.4f;
						ammoImg.fillAmount = (float)boostAmmo / (float)maxBoostAmmo;
						BulletPool.GetBullet().Fire(transform.position,
						(transform.right + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))).normalized * 10f);
					}
					else
					{
						BulletPool.GetBullet().Fire(transform.position,
						transform.right * 10f);
					}
				}
			}

			if (!Input.GetMouseButton(0))
			{
				nextBoostAmmo -= Time.deltaTime;
				if(nextBoostAmmo < 0)
				{
					boostAmmo++;
					nextBoostAmmo = 0.4f;
					ammoImg.fillAmount = (float)boostAmmo / (float)maxBoostAmmo;
				}
			}

			CheckHit();
		}

		private void CheckHit()
		{
			hits = Physics2D.OverlapCircleAll(transform.position, 0.25f);
			for (int i = 0; i < hits.Length; i++)
			{
				Powerup p = hits[i].GetComponent<Powerup>();
				if(p != null)
				{
					if(p.Power == 0)
					{
						boostAmmo = maxBoostAmmo;
						Debug.Log("Gun Boost");
					}
					else if( p.Power == 1)
					{
						Enemy.SetSlowTime(5);
						Debug.Log("Enemy Slow");
					}
					else
					{
						AddScore();
						Debug.Log("Point");
					}
					p.Pickup();
					return;
				}
				
				Enemy e = hits[i].GetComponent<Enemy>();
				if (e != null) 
				{
					this.enabled = false;
				}
			}
		}

		public void AddScore()
		{
			score++;
			scoreText.text = $"Score: {score}";
		}
	}
}