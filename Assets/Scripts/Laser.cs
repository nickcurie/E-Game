using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox" || other.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }

    public void addVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
}
