using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 scaleChange;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite explosionSprite;

    private void Awake()
    {
        StartCoroutine("Retarget");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy")
        {
            addVelocity(new Vector2(0, 0));
            StartCoroutine("Explode");
        }
    }

    public void addVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private GameObject FindTarget()
    {
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {

            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }

    private IEnumerator Retarget()
    {
        //Debug.Log("entered coroutine");
        for (int i = 0; i < 2; i++)
        {
            float secondsToWait = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(secondsToWait);
        }

        GameObject closestEnemy = FindTarget();
        speed = speed * 4;
        Vector2 direction = closestEnemy.transform.position - transform.position;
        direction.Normalize();
        addVelocity(direction);
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, -Vector3.forward);
    }

    private IEnumerator Explode()
    {
        GetComponent<Animator>().enabled = false;
        spriteRenderer.sprite = explosionSprite;

        for (int i = 0; i < 5; i++)
        {
            if (i % 2 == 1)
            {
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }
            transform.localScale += scaleChange;
            yield return new WaitForSeconds(.05f);
        }

        Destroy(gameObject);
    }
}
