using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Statics
{
	public class EnemyPool : MonoBehaviour
	{
		[SerializeField] private Enemy enemyPrefab;
		[SerializeField] private int countIncrease;

		private static List<Enemy> enemies = new List<Enemy>();

		private int count;

		private void Start()
		{
			for (int i = 0; i < 100; i++)
			{
				enemies.Add(Instantiate(enemyPrefab));
			}

			InvokeRepeating(nameof(SpawnEnemies), 0f, 15f);
		}

		private void SpawnEnemies()
		{
			count += countIncrease;
			for (int i = 0; i < count; i++)
			{
				GetEnemy().gameObject.SetActive(true);
			}
		}

		public static Enemy GetEnemy()
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				if (!enemies[i].gameObject.activeSelf)
				{
					enemies[i].gameObject.SetActive(true);
					return enemies[i];
				}
			}
			//No free enemies, add one
			enemies.Add(Instantiate(enemies[0]));
			return enemies[enemies.Count - 1];
		}
	}
}