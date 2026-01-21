using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Statics
{
    public class BulletPool : MonoBehaviour
    {
		[SerializeField] private Bullet bulletPrefab;
        private static List<Bullet> bullets = new List<Bullet>();

		private void Start()
		{
			for (int i = 0; i < 100; i++)
			{
				bullets.Add(Instantiate(bulletPrefab));
			}
		}

		public static Bullet GetBullet()
		{
			for(int i = 0;i < bullets.Count;i++)
			{
				if (!bullets[i].gameObject.activeSelf)
				{
					bullets[i].gameObject.SetActive(true);
					return bullets[i];
				}
			}
			//No free bullets, add one
			bullets.Add(Instantiate(bullets[0]));
			return bullets[bullets.Count - 1];
		}
	}
}