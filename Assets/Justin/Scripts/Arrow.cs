using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20;
    public Vector3 velocity;
    public GameObject flash;
    public int Damage = 3;
    bool go = true;


    public int totalBounces = 3;
    public int currentBounces = 0;

    //public Transform head;
    //public Transform tail;

    public LayerMask whatToHit;

    private string lasthitname;


    private void Awake()
    {
        Debug.Log(transform.rotation);
    }

    void Update()
    {

        ///Let's raycast

        //var rb = GetComponent<Rigidbody2D>();
        if (go)
        {
            CastARay();
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
        }
    }

    void CastARay()
    {

        if (currentBounces > totalBounces)
        {
            go = false;
        }
        else
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.1f, whatToHit);
            if (hit.collider != null && hit.collider.name != lasthitname)
            {
                lasthitname = hit.collider.name;
                currentBounces++;
                Vector3 reflectDir = Vector3.Reflect(ray.direction.normalized, hit.normal);
                float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
            }

        }


        ///// Hit Scan
        ///// 
        ////Vector2 headPosition = new Vector2(head.position.x, head.position.y);
        ////Vector2 tailPosition = new Vector2(tail.position.x, tail.position.y);
        ////RaycastHit2D hit = Physics2D.Raycast(tailPosition, headPosition - tailPosition, 1, whatToHit);


        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.5f, whatToHit);

        ////Debug.DrawLine(headPosition, (headPosition - tailPosition) * 1000, Color.cyan);
        //if (hit.collider != null)
        //{
        //    Debug.Log("hit");
        //    if (hit.collider.name == lasthitname)
        //    {
        //        Debug.Log("hits same");
        //    }

        //    //if (hit.collider.name != lasthitname)
        //    //{
        //        hit.collider.name = lasthitname;
        //        var transformVector2 = new Vector2(transform.position.x, transform.position.y);
        //        var inDirection = (hit.point - transformVector2).normalized;

        //        Vector3 rayDir = Vector2.Reflect(inDirection, hit.normal);
        //        float angle = Mathf.Atan2(rayDir.x, rayDir.y) * Mathf.Rad2Deg;
        //        var targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        //        transform.rotation = targetRot;

        //    //}




        //    //var reflectedAngel = Vector3.Reflect(headPosition, hit.normal);

        //    //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 90, transform.rotation.w);
        //    //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        //    //transform.rotation = Quaternion.Euler(reflectedAngel);
        //}
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    ///Bounce

    //    //transform.rotation = Quaternion.Euler(new Vector3(-transform.rotation.x, -transform.rotation.y, -transform.rotation.z));
    //    //Debug.Log("bounce " + collision.transform);
    //    //Debug.DrawLine(collision.collider.name)


    //    if (collision.tag == "ground")
    //    {
    //        go = false;

    //        //var rb = GetComponent<Rigidbody2D>();
    //        ////Debug.Log("rb.gravityScale: " + rb.gravityScale.ToString());
    //        //rb.gravityScale = 0;
    //        //Debug.Log("rb.gravityScale: " + rb.gravityScale.ToString());

    //        //Destroy(gameObject);

    //        //Vector3 arrowPosition = Camera.main.WorldToScreenPoint(transform.position);
    //        //Debug.Log("collision:" + collision.transform.transform.position.x);
    //        ////float angle = Mathf.Atan2(Input.mousePosition.y - gunPosition.y, Inaaaaaput.mousePosition.x - gunPosition.x) * Mathf.Rad2Deg;
    //        //Vector3 collisionPosition = Camera.main.WorldToScreenPoint(collision.transform.position)




    //        ////Vector3 headPosition = Camera.main.WorldToScreenPoint(head.position);
    //        ////Vector3 tailPosition = Camera.main.WorldToScreenPoint(tail.position);
    //        ////Vector3 dir = tail.position - tail.position;

    //        //Vector3 dir = transform.position - collision.transform.position;
    //        ////var reflect = Vector3.Reflect()

    //        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Deg2Rad;
    //        ////Debug.Log("angle: " + angle);

    //        ////float angle = Mathf.Atan2(Input.mousePosition.y - gunPosition.y, Input.mousePosition.x - gunPosition.x) * Mathf.Rad2Deg;
    //        ////Debug.Log("angle " + angle);

    //        ////transform.rotation = new Quaternion(-transform.rotation.x, -transform.rotation.y, 90, -transform.rotation.w); ;// Quaternion.Euler(new Vector3(0, 0, angle));
    //        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //    }
    //}
}
