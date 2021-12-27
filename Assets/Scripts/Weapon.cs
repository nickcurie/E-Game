using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int turretDuration;
    [SerializeField] private float distance;
	[Space]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject primaryProjectile;
    [SerializeField] private GameObject secondaryProjectile;
    [SerializeField] private GameObject turretProjectile;
    [SerializeField] GameObject rocketProjectile;
    [SerializeField] private GameObject mainCamera;
    
	private Text remainingShots;
    private GameObject turret;
    private ShakeBehavior shakeScript;
    private Turret turretScript;
    private Rocket rocketScript;
    private bool turretIsMoving = true;
    private LineRenderer line;
    private int currentDuration;
    private GameObject godObject;
    private GameController gameController;
    private bool firePrimaryRunning;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        currentDuration = turretDuration;
    }

	private void Start()
	{
        shakeScript = mainCamera.GetComponent<ShakeBehavior>();
		godObject = GameObject.FindGameObjectWithTag("Controller");
        gameController = godObject.GetComponent<GameController>();
	}

    private void Update()
    {
        if (gameController.isPaused)
        {
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			ShootPrimary();
		}
		// if (Input.GetMouseButtonDown(1))
		// {
		// 	ShootSecondary();
		// }
		if (PlayerData.ownsIceBlast && Input.GetKeyDown("space"))
		{
			ShootIceBall();
		}
		if (PlayerData.ownsDrone && Input.GetKeyDown("e"))
		{

			if (turret == null)
			{
				ShootTurret();
				currentDuration = turretDuration;
				remainingShots = turret.GetComponentInChildren<Text>();
				remainingShots.text = currentDuration.ToString();
			}
			else if (turretIsMoving)
			{
				turretScript.Freeze();
				turretIsMoving = false;
			}
			else
			{
				turretScript.Shoot();
				//Debug.Log(currentDuration);
				currentDuration--;
				remainingShots.text = currentDuration.ToString();

				if (currentDuration < 1)
				{
					Destroy(turret);
					turretIsMoving = true;
				}
			}
		}
		if (PlayerData.ownsRocket && Input.GetKey("q"))
		{
			ShootRocket();
		}

    }

    private void ShootPrimary()
    {
        // Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        // Vector2 direction = target - pos;
        // direction.Normalize();
        // Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // GameObject bullet = Instantiate(primaryProjectile, firePoint.transform.position, rotation) as GameObject;
        // bullet.GetComponent<Bullet>().addVelocity(direction);
        if (!firePrimaryRunning)
        {
            StartCoroutine("FirePrimary");
        }
    }

    private void ShootSecondary()
    {
        firePoint.transform.Find("laser_charge_0").gameObject.SetActive(true);
        StartCoroutine("FireLaser");
    }

    private void ShootIceBall()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - pos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject iceBall = Instantiate(secondaryProjectile, firePoint.transform.position, rotation) as GameObject;
        iceBall.GetComponent<Laser>().addVelocity(direction);
    }

    private void ShootTurret()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - pos;
        direction.Normalize();
        //Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        turret = Instantiate(turretProjectile, firePoint.transform.position, rotation) as GameObject;
        turretScript = turret.GetComponent<Turret>();
        turretScript.addVelocity(direction);
    }

    private void ShootRocket()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        GameObject rocket = Instantiate(rocketProjectile, transform.position, rotation) as GameObject;
        rocketScript = rocket.GetComponent<Rocket>();
        rocketScript.addVelocity(new Vector2(0, 1));
    }

    private IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetMouseButton(1))
        {
            Ray2D ray;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int dir;

            if (mousePos.x >= firePoint.transform.position.x)
                dir = 1;
            else
                dir = -1;

            ray = new Ray2D(firePoint.transform.position, dir * transform.right);

            RaycastHit2D[] allHits;

            line.SetPosition(0, ray.origin);

            allHits = Physics2D.RaycastAll(ray.origin, dir * Vector2.right, distance);
            //Debug.Log(hit.collider.tag);

            foreach (var hit in allHits)
            {
                if (hit.collider.tag == "Platform")
                    line.SetPosition(1, hit.point);
                else if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyMovement>().Die();
                    // Destroy(hit.collider.gameObject);
                }
                else
                    line.SetPosition(1, ray.GetPoint(distance));
            }

            yield return null;
        }
        line.enabled = false;
        firePoint.transform.Find("laser_charge_0").gameObject.SetActive(false);
    }

    private IEnumerator FirePrimary()
    {
        firePrimaryRunning = true;
        for (int i = 0; i < 9; i++)
        {
            if (i > 5)
            {
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = target - pos;
                direction.Normalize();
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                GameObject bullet = Instantiate(primaryProjectile, firePoint.transform.position, rotation) as GameObject;
                bullet.GetComponent<Bullet>().addVelocity(direction);
                shakeScript.TriggerShake();
                yield return new WaitForSeconds(.05f);
            }
            else
            {
                yield return new WaitForSeconds(.1f);
            }
        }
        firePrimaryRunning = false;
    }

}
