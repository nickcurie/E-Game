using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float foundSpeed;
    [SerializeField] private float searchingSpeed;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float lungeSpeed;
    [SerializeField] private float lungeCooldown;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D[] col;
    private HealthBarHandler healthBar;
    private GameObject playerToChase;
    private float currentCooldown;
    private float step;
    private float lungeStep;
    private bool isDead = false;
    private float randomDist;
    private bool waitingForPosition = true;
    private Vector2 newPos;
    private float currentSpeed;

	private void Awake()
	{
		anim = GetComponent<Animator>();
        col = GetComponents<CircleCollider2D>();
        healthBar = GetComponentInChildren<HealthBarHandler>();
		rb = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
        playerToChase = GameObject.Find("Player");
        currentSpeed = foundSpeed;
        currentCooldown = lungeCooldown;
    }

    private void Update()
    {
        step = currentSpeed * Time.deltaTime;
        currentCooldown -= Time.deltaTime;
        lungeStep = lungeSpeed * Time.deltaTime;

        if (isDead) return;

		float distance = Vector3.Distance(playerToChase.transform.position, transform.position);

		if (distance <= detectionDistance)
		{
			currentSpeed = foundSpeed;
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerToChase.transform.position.x, transform.position.y), step);
			//Debug.Log(transform.position);
			//Debug.Log(distance);
			if (distance <= 1.0f && currentCooldown <= 0)
			{
				//Lunge(lungeStep);
			}
		}
		else
		{
			currentSpeed = searchingSpeed;
			if (waitingForPosition)
			{
				randomDist = Random.Range(-2.0f, 2.0f);
				newPos = new Vector2(transform.position.x + randomDist, transform.position.y);
				waitingForPosition = false;
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + randomDist, transform.position.y), step);
				if (new Vector2(transform.position.x, transform.position.y) == newPos)
				{
					waitingForPosition = true;
				}
			}
		}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Damaging")
        {
            if (!isDead)
            {
                healthBar.SetHealthBarValue(healthBar.GetHealthBarValue() - (1 / (float)health));
                if (healthBar.GetHealthBarValue() <= 0.0f)
                {
                    Die();
                }
            }
        }

        if (other.tag == "Killbox")
        {
            Destroy(gameObject);
        }

    }

    private void Lunge(float step)
    {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, playerToChase.transform.rotation, step);

        int dir = playerToChase.transform.position.x > transform.position.x ? 1 : -1;

        rb.velocity = dir * transform.right * lungeSpeed;

        currentCooldown = lungeCooldown;

    }

    public void Die()
    {
        isDead = true;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        col[0].enabled = false;
        col[1].enabled = false;
        anim.SetTrigger("Death");
        playerToChase.GetComponent<PlayerMovement>().score += 1;
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

}
