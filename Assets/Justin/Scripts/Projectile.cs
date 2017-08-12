using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //Base Properties
    public float lifeTime = 5;
    public float speed = 20;
    public int Damage = 3;
    public LayerMask whatToHit;
    public bool destroyOnImpact = true;
    private bool go = true;

    //Reflection Properties
    public bool bounce = false;
    public int maxbounces = 3;
    private int currentBounces = 0;

    ////Audio
    //public AudioSource SoundOnFire;
    //public AudioSource SoundOnImpact;

    ////Effects
    //public GameObject MuzzleFlash;
    //public GameObject Explosion;


    private void Start()
    {
        //All projectiles are marked for death.
        Destroy(gameObject, lifeTime);

        //if (SoundOnFire !=null)
        //{
        //    //SoundOnFire.Play();
        //}
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

            ////Play Sounds
            //if (SoundOnImpact != null)
            //{
            //    SoundOnImpact.Play();
            //}

            //Destroy
            if (destroyOnImpact)
            {
                Destroy(gameObject);
            }

            //Deflect
            if (bounce)
            {
                if (currentBounces <= maxbounces)
                {
                    currentBounces++;
                    Ray ray = new Ray(transform.position, transform.right);
                    //Vector3 reflectDir = Vector3.Reflect(ray.direction.normalized, hit.normal);
                    Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);

                    Ray ray2 = new Ray(transform.position, reflectDir);
                    
                    //float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
                }
                else
                {
                    go = false;
                }
            }


            //Spawn Explosions

            //Do Damage
            var targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.GetsHit(Damage);
            }
            

        }
    }

    private void OnDestroy()
    {
        //SoundOnFire.Play();
    }
}
