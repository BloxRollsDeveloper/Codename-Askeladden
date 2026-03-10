using System;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        currentHealth -= damage;
    }
}
