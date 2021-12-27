using System.Collections;
using UnityEngine;

public class BuddyMovement : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float rotateSpeed;

    private bool shootingAtEnemy;
    private GameObject player;
    private Vector2 velocity;
    private GameObject enemyToShoot;
    private float randomx;
    private float randomy;
    private Vector2 turretDefaultPos;
    private bool movingToPlayer;
    private float radius;
    private float angle;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        randomx = Random.Range(-2f, 2f);
        randomy = Random.Range(-2f, 2f);
        if (Mathf.Abs(randomx) >= Mathf.Abs(randomy))
        {
            radius = Mathf.Abs(randomx);
        }
        else
        {
            radius = Mathf.Abs(randomy);
        }
    }

    private void Update()
    {
        // if (!movingToPlayer){
        //     StartCoroutine("FollowPlayer");
        // }

        turretDefaultPos = new Vector2(player.transform.position.x + randomx, player.transform.position.y + randomy);

        if (transform.position.x == turretDefaultPos.x && transform.position.y == turretDefaultPos.y)
        {
            movingToPlayer = false;
        }
        else
        {
            movingToPlayer = true;
        }

        // if (movingToPlayer){
        transform.position = Vector2.SmoothDamp(transform.position, turretDefaultPos, ref velocity, 1.0f);
        // }
        // else{
        //StartCoroutine("CirclePlayer");
        //}

        if (!shootingAtEnemy)
        {
            StartCoroutine("FireAtEnemy");
        }
    }

    private GameObject FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5.0f);
        GameObject firstEnemy = null;

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                firstEnemy = enemy.gameObject;
                break;
            }
        }
        //Debug.Log(firstEnemy);
        return firstEnemy;
    }

    // IEnumerator FollowPlayer(){
    //     movingToPlayer = true;

    //     transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - 1, player.transform.position.y + 1, player.transform.position.z), .1f);
    //     yield return new WaitForSeconds(.0005f);

    //     movingToPlayer = false;
    // }

    private IEnumerator FireAtEnemy()
    {
        shootingAtEnemy = true;
        enemyToShoot = FindTarget();
        if (enemyToShoot == null)
        {
            shootingAtEnemy = false;
            yield break;
        }
        for (int i = 0; i < 3; i++)
        {
            if (enemyToShoot.transform.position.x >= transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            Vector2 direction = enemyToShoot.transform.position - transform.position;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.GetComponent<Bullet>().addVelocity(direction);

            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(1.2f);
        shootingAtEnemy = false;
    }

    private IEnumerator CirclePlayer()
    {
        angle += rotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), transform.position.z) * Vector2.Distance(player.transform.position, turretDefaultPos);
        transform.position = player.transform.position + offset;
        yield return null;
    }
}
