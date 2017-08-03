using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public Vector3 mouse_pos;
    public Vector3 object_pos;
    public Transform firePoint;
    public float angle;
    public LayerMask whatToHit;
    public GameObject Bullet;
    public GameObject MuzzleFlash;

    public float fireRate = .1f;
    private float nextFire;

    public float Spread;

    void FixedUpdate()
    {
        Aim();
        
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Aim()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5f; //Camera Size (The distance between the camera and object)
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    #region shooting

    void Shoot()
    {

        ///Projectile
        Instantiate(MuzzleFlash, firePoint.position, firePoint.rotation);
        var variedrotation = new Quaternion(firePoint.rotation.x, firePoint.rotation.y, (firePoint.rotation.z + Random.Range(Spread, -Spread)), firePoint.rotation.w);
        Instantiate(Bullet, firePoint.position, variedrotation);

        ///// Hit Scan
        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 10, Color.cyan);
        //if (hit.collider != null)
        //{
        //    Debug.DrawLine(firePointPosition, hit.point, Color.red);
        //    Debug.Log("We hit " + hit.collider.name);
        //}

    }



    #endregion
}
