using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{



    public Transform firePoint;
    public LayerMask whatToHit;
    public GameObject Bullet;
    public GameObject MuzzleFlash;
    public GameObject Arrow;

    public float ArrowRate = .25f; //4 X second
    public float BulletRate = .1f; //10 X sceond

    private float nextFire1;
    private float nextFire2;

    public float Spread;

    void FixedUpdate()
    {
        Aim();

        ShootRayCast();
        
        if (Input.GetButton("Fire1") && Time.time > nextFire1)
        {
            nextFire1 = Time.time + ArrowRate;
            ShootArrow();
        }
        if (Input.GetButton("Fire2") && Time.time > nextFire2)
        {
            nextFire2 = Time.time + BulletRate;
            ShootBullet();
        }
    }

    void Aim()
    {
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(Input.mousePosition.y - gunPosition.y, Input.mousePosition.x - gunPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    #region shooting

    void ShootArrow()
    {
        Instantiate(Arrow, firePoint.position, firePoint.rotation);
    }

    void ShootBullet()
    {
        Instantiate(MuzzleFlash, firePoint.position, firePoint.rotation);
        var variedrotation = new Quaternion(firePoint.rotation.x, firePoint.rotation.y, (firePoint.rotation.z + Random.Range(Spread, -Spread)), firePoint.rotation.w);
        Instantiate(Bullet, firePoint.position, variedrotation);
    }

    void ShootRayCast()
    {
        /// Hit Scan
        /// 
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //Debug.Log("mousePosition: " + mousePosition);
        //Debug.Log("mousePosition - firePointPosition: " + (mousePosition - firePointPosition).ToString());
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 1000, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);


            //    ///Bounce Deflect for hitscan work
            //    ////Debug.Log("We hit " + hit.collider.name);
            //    //////Debug.DrawLine(hit.point, new Vector2(0,0), Color.cyan);
            //    ////Debug.Log("hit.normal: " + hit.normal);



            //    ////mouse_pos = Input.mousePosition;
            //    ////mouse_pos.z = 5f; //Camera Size (The distance between the camera and object)
            //    ////object_pos = Camera.main.WorldToScreenPoint(transform.position);
            //    ////mouse_pos.x = mouse_pos.x - object_pos.x;
            //    ////mouse_pos.y = mouse_pos.y - object_pos.y;
            //    //////angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            //    //////transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            //    ////from
            //    //float x = hit.point.x - firePointPosition.x;
            //    //float y = hit.point.y - firePointPosition.y;

            //    /////Find the deflect direction
            //    //Vector2 reflctDirection = new Vector2(x,y);

            //    //var raydir = Vector3.Reflect((hit.point - new Vector2(transform.position.x, transform.position.y)).normalized, hit.normal);



            //    //RaycastHit2D hit2 = Physics2D.Raycast(hit.point, raydir, 100, whatToHit);
            //    //if (hit2.collider != null)
            //    //{
            //    //    Debug.DrawLine(hit.point, raydir, Color.green);
            //    //    Debug.Log(hit.collider.name);
            //    //}

            //    ////var angle = Vector2.Angle(firePoint.position, hit.normal);
            //    ////angle = 90.0f - Mathf.Abs(angle);
            //    ////Debug.Log("Angle from surface = " + angle);
            //    ////Debug.DrawLine(hit.point, , Color.cyan);
            //}
        }
    }


    #endregion
}
