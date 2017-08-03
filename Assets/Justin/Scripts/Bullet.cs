using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public Vector3 velocity;
    public GameObject flash;
    public int Damage = 3;
    
    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(flash, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
