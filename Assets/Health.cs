using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth = 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetsHit(int damaage)
    {
        currentHealth -= damaage;
        Debug.Log("Health: " + currentHealth.ToString());

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
