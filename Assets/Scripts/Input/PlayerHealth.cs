using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    private PlayerController _playerController;
    [Header("Animation")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        _playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        _playerController.animationState = PlayerController.AnimationState.Damage;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            _playerController.animationState = PlayerController.AnimationState.Dead;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateDamage(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play("DamageNorth");
                break;
            case PlayerDirection.Direction.East:
                animator.Play("DmageEast");
                break;
            case PlayerDirection.Direction.South:
                animator.Play("DamageSouth");
                break;
            case PlayerDirection.Direction.West:
                animator.Play("DamageWest");
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play("DamageNorthEast");
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play("DamageNorthWest");
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play("DamageSouthEast");
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play("DamageSouthWest");
                break;
        }
    }

    public void UpdateDeath(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play("DeathNorth");
                break;
            case PlayerDirection.Direction.East:
                animator.Play("DeathEast");
                break;
            case PlayerDirection.Direction.South:
                animator.Play("DeathSouth");
                break;
            case PlayerDirection.Direction.West:
                animator.Play("DeathWest");
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play("DeathNorthEast");
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play("DeathNorthWest");
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play("DeathSouthEast");
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play("DeathSouthWest");
                break;
        }
    }
}
