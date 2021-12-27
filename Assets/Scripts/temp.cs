using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        //Vector3 pos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z);
        //Vector3 direction = target - pos;
        //direction.Normalize();
        //Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        //firePoint.transform.rotation = rotation;
        //GameObject bullet = Instantiate(primaryProjectile, firePoint.transform.position, rotation) as GameObject;
        //bullet.GetComponent<Bullet>().addVelocity(direction);
        //shakeScript.TriggerShake();
    }
}
