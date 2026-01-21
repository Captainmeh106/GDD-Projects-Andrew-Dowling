using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Statics
{
    public class Bullet : MonoBehaviour
    {
		[SerializeField] private float radius;
		[SerializeField] private LayerMask enemyLayer;

        private Vector3 velocity;
		private Collider2D hit;

		private void Start()
		{
			gameObject.SetActive(false);	// Prepare for pooling
		}

		private void Update()
		{
			transform.position += velocity * Time.deltaTime;

			hit = Physics2D.OverlapCircle(transform.position, radius, enemyLayer);
			if(hit != null )
			{
				hit.GetComponent<Enemy>().TakeDamage();
				gameObject.SetActive(false);
			}

			if(Vector2.Distance(transform.position, Player.Instance.transform.position) > 10)
			{
				gameObject.SetActive(false);
			}
		}

		public void Fire(Vector3 position, Vector3 vel)
		{
			transform.position = position;
			velocity = vel;
		}
	}
}