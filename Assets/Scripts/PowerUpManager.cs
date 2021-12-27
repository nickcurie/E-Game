using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private type PowerUpType;

    private enum type
    {
        Score,
        Health,
        Damage
    }
    private GameObject healthBar;
    private HealthBarHandler healthBarScript;
    private GameObject player;
    private PlayerMovement playerMovementScript;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovementScript = player.GetComponent<PlayerMovement>();
        healthBarScript = player.GetComponentInChildren<HealthBarHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PowerUpType == type.Score)
            {
                playerMovementScript.score += 10;
                Destroy(gameObject);
            }
            else if (PowerUpType == type.Health)
            {
                StartCoroutine("IncreaseHealth");
            }
            else if (PowerUpType == type.Damage)
            {
                /*
                    TODO: IMPLEMENT DAMAGE POWERUP
                */
                Destroy(gameObject);
            }

        }
    }

    private IEnumerator IncreaseHealth()
    {
        spriteRenderer.enabled = false;

        WaitForSeconds timeToWait = new WaitForSeconds(0.05f);
        while (healthBarScript.GetHealthBarValue() <= 1.0f)
        {
            healthBarScript.SetHealthBarValue(Mathf.Clamp(healthBarScript.GetHealthBarValue() + .03f, 0.0f, 1.0f));
            yield return timeToWait;
        }
        Destroy(gameObject);
    }
}
