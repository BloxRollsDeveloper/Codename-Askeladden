using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
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
        _playerAnimationController.UpdateAnimation(false, true, false, false);
        
        if (currentHealth <= 0)
        {
            _playerAnimationController.UpdateAnimation(false, false, true, false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
