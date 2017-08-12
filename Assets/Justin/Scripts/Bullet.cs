using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    //public Vector3 velocity;
    public GameObject flash;
    public int Damage = 3;
    public LayerMask whatToHit;

    public bool bounce = false;
    public int maxbounces = 3;
    private int currentBounces = 0;

    private bool go = true;

    public float lifeTime = 5;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        Move();
        Collide();
    }

    void Move()
    {
        if (go)
        {
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
        }
    }

    void Collide()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.1f, whatToHit);
        if (hit.collider != null)
        {
            Destroy(gameObject);
            //Play Sounds
            //Spawn Explosions
            //Do Damage

            //Deflect
            if (bounce)
            {
                if (currentBounces <= maxbounces)
                {
                    currentBounces++;
                    Ray ray = new Ray(transform.position, transform.right);
                    Vector3 reflectDir = Vector3.Reflect(ray.direction.normalized, hit.normal);
                    float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
                }
            }
        }
    }
}
