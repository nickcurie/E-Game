using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject turretBulletPrefab;
    
    private GameObject player;
    private Rigidbody2D rb;
    private GameObject bullet;
    private Bullet bulletScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox")
        {
            Destroy(gameObject);
        }
    }

    public void Freeze()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void Shoot()
    {
        //Debug.Log("I'm shooting!");
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - pos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        bullet = Instantiate(turretBulletPrefab, gameObject.transform.position, rotation) as GameObject;
        bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.addVelocity(direction);
    }

    public void addVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

}
