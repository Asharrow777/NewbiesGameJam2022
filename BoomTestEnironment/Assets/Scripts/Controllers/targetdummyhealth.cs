using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetdummyhealth : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {

        Debug.Log(" Take Damage ");
        currentHealth -= damage;
        Debug.Log("currentHealth: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Die animation
        // restart game
        Debug.Log("Die fuction");
    }
    
}
