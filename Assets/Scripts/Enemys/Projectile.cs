using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameObject.TryGetComponent(out PlayerHealth playerHealth);
            playerHealth.TakeDamage(damage);
        }
    }
}
