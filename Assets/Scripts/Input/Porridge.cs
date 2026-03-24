using UnityEngine;

public class Porridge : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float healingEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.TryGetComponent(out PlayerHealth playerHealth);
            playerHealth.Heal(healingEffect);
        }
    }
}
