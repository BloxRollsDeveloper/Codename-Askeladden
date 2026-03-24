using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private GameObject deathScreen;
    
    private PlayerAnimationController _playerAnimationController;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            _playerAnimationController.UpdateAnimation(false, false, true, false);
        }
        else
        {
            _playerAnimationController.UpdateAnimation(false, true, false, false);
        }
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        healthSlider.value = currentHealth;
    }
}
