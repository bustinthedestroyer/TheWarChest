using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badguy : MonoBehaviour
{

    public GameObject Bullet;
    public Transform sight;
    public LayerMask target;
    public float range = 8;

    private void FixedUpdate()
    {
        FacePlayer();
        Shoot();
    }

    private void FacePlayer()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float angleToPlayer = Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToPlayer));
    }

    public float fireRate = .1f;

    public float breakRate = 1f;
    public int projectileCount = 3;

    private int currentShots = 0;
    private float nextFire;

    private void Awake()
    {
        nextFire = Random.Range(0.0f, 1.0f);
    }

    private void Shoot()
    {

        RaycastHit2D hit = Physics2D.Raycast(sight.position, sight.right, range, target);
        if (hit.collider != null)
        {
            Debug.DrawLine(sight.position, hit.point, Color.red);

            ///Pew
            ///
            //if (Time.time > nextFire)
            //{
            //    nextFire = Time.time + fireRate;
            //    Instantiate(Bullet, sight.position, sight.rotation);
            //}

            ///Pew pew pew
            ///
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(Bullet, sight.position, sight.rotation);
                currentShots++;
                if (currentShots >= projectileCount)
                {
                    currentShots = 0;
                    nextFire = Time.time + breakRate;
                }
            }
        }
    }

}
