using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage, bool isTakingDamage)
    {
        if (isTakingDamage)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }
        
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
