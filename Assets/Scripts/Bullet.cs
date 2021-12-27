using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hitsUntilDestroy;
    
    private Rigidbody2D rb;
    private int currentHits;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy"){
            currentHits += 1;
            if (currentHits >= hitsUntilDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    public void addVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

}
