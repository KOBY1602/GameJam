using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //Debug.Log("Took DMG");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        //Debug.Log(currentHealth);
    }
    void Die()
    {
        // Perform death actions such as playing death animation, particle effects, etc.
        Destroy(gameObject);
    }
}
