using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    ///Base Properties
    public float TimeToLive;
    public float Speed;
    public int Damage;
    public LayerMask Targets;
    public bool DestroyOnImpact = true;
    private bool go = true;

    ///Reflection Properties
    public bool Deflects = false;
    public int MaximumDeflections;
    private int currentDeflections = 0;

    ///Audio
    //public AudioSource SoundOnFire;
    //public AudioSource SoundOnImpact;

    ///Effects
    //public GameObject MuzzleFlash;
    //public GameObject Explosion;


    private void Start()
    {
        ///All projectiles are marked for death.
        Destroy(gameObject, TimeToLive);

        //if (SoundOnFire !=null)
        //{
        //    //SoundOnFire.Play();
        //}
    }

    void FixedUpdate()
    {
        //Go untill no!
        if (go)
        {
            Move();
            Collide();
        }
    }

    void Move()
    {
        transform.Translate(new Vector2(1, 0) * Time.deltaTime * Speed);
    }

    void Collide()
    {
        ///Ray cast to see if we hit anything
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.1f, Targets);
        if (hit.collider != null)
        {
            ///Play Sounds
            //if (SoundOnImpact != null)
            //{
            //    SoundOnImpact.Play();
            //}

            ///Destroy
            if (DestroyOnImpact)
            {
                ///Spawn Explosions
                Destroy(gameObject);
            }

            ///Do Damage if there is a health componot of what we hit, or deflect if deflect is checked, long comment award.
            var targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(Damage);
                go = false;
            }
            else if (Deflects)
            {
                Deflection(hit.normal);
            }
        }
    }

    private void Deflection(Vector2 normal)
    {
        Debug.Log("Deflect called");
        if (currentDeflections < MaximumDeflections)
        {
            currentDeflections++;
            Ray ray = new Ray(transform.position, transform.right);
            Vector3 reflectDir = Vector3.Reflect(transform.right, normal);
            float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
        }
        else
        {
            go = false;
        }
    }
}