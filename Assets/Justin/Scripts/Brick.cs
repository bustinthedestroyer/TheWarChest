using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public int Health = 100;
    public GameObject Explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health = Health - 3;
        Debug.Log(Health);

        if (Health <= 0)
        {
            Instantiate(Explosion, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 90f, transform.rotation.w));
            Destroy(gameObject);
        }
    }
}