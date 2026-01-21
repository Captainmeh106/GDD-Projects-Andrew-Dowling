using UnityEngine;

namespace Game.Statics
{
	public class Enemy : MonoBehaviour
	{
		private static float slowTime;				// Shared by all enemy objects

		[SerializeField] private SpriteRenderer sprRend;

		private int health;
		private Color col;
		private float hitTime;

		private void Awake()
		{
			col = sprRend.color;
			gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			health = Random.Range(3, 10);
			transform.localScale = Vector3.one * (1f + health * 0.2f);
			sprRend.color = col;
			transform.position = Player.Instance.transform.position + (Vector3)Random.insideUnitCircle.normalized * 10f;
		}

		void Update()
		{
			float moveSpeed = 2f;
			if(slowTime > 0)
			{
				slowTime -= Time.deltaTime;
				moveSpeed = 0.5f;
			}

			transform.position += 
				(Player.Instance.transform.position + 
				new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f)) * Vector2.Distance(transform.position, Player.Instance.transform.position)
				- transform.position).normalized * Time.deltaTime * moveSpeed;

			if(hitTime > 0 )
			{
				hitTime -= Time.deltaTime;
				if( hitTime < 0 )
					sprRend.color = col;
			}
		}

		public void TakeDamage()
		{
			health -= 1;
			if(health <= 0 )
			{
				Player.Instance.AddScore();
				gameObject.SetActive(false);
			}
			else
			{
				sprRend.color = Color.white;
				hitTime = 0.075f;
			}
		}

		public static void SetSlowTime(float time)
		{
			slowTime = time;
		}
	}
}
