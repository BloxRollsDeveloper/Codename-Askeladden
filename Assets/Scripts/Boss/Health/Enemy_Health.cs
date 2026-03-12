using System;
using UnityEngine;
using Unity.Cinemachine;
using System.Threading.Tasks;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
            TryGetComponent(out CinemachineImpulseSource impulse);
            impulse.GenerateImpulse();
        }
    }
}
