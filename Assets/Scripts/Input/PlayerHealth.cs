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
    private PlayerDirection _playerDirection;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float damageAnimTime;
    [SerializeField] private float deathAnimTime;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        
        _playerController = GetComponent<PlayerController>();
        _playerDirection = GetComponent<PlayerDirection>();
    }

    public void TakeDamage(float damage)
    {
        // _playerController.animationState = PlayerController.AnimationState.Damage;
        UpdateDamage(_playerDirection);
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            // _playerController.animationState = PlayerController.AnimationState.Dead;
            UpdateDeath(_playerDirection);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateDamage(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play(DamageNorth);
                break;
            case PlayerDirection.Direction.East:
                animator.Play(DamageEast);
                break;
            case PlayerDirection.Direction.South:
                animator.Play(DamageSouth);
                break;
            case PlayerDirection.Direction.West:
                animator.Play(DamageWest);
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play(DamageNorthEast);
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play(DamageNorthWest);
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play(DamageSouthEast);
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play(DamageSouthWest);
                break;
        }
    }

    public void UpdateDeath(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play(DeathNorth);
                break;
            case PlayerDirection.Direction.East:
                animator.Play(DeathEast);
                break;
            case PlayerDirection.Direction.South:
                animator.Play(DeathSouth);
                break;
            case PlayerDirection.Direction.West:
                animator.Play(DeathWest);
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play(DeathNorthEast);
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play(DeathNorthWest);
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play(DeathSouthEast);
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play(DeathSouthWest);
                break;
        }
    }
    
    // Damage Animations 
    private static readonly int DamageNorth = Animator.StringToHash("DamageNorth");
    private static readonly int DamageEast = Animator.StringToHash("DmageEast");
    private static readonly int DamageSouth = Animator.StringToHash("DamageSouth");
    private static readonly int DamageWest = Animator.StringToHash("DamageWest");
    private static readonly int DamageNorthEast = Animator.StringToHash("DamageNorthEast");
    private static readonly int DamageNorthWest = Animator.StringToHash("DamageNorthWest");
    private static readonly int DamageSouthEast = Animator.StringToHash("DamageSouthEast");
    private static readonly int DamageSouthWest = Animator.StringToHash("DamageSouthWest");
    
    // Death Animations 
    private static readonly int DeathNorth = Animator.StringToHash("DeathNorth");
    private static readonly int DeathEast = Animator.StringToHash("DeathEast");
    private static readonly int DeathSouth = Animator.StringToHash("DeathSouth");
    private static readonly int DeathWest = Animator.StringToHash("DeathWest");
    private static readonly int DeathNorthEast = Animator.StringToHash("DeathNorthEast");
    private static readonly int DeathNorthWest = Animator.StringToHash("DeathNorthWest");
    private static readonly int DeathSouthEast = Animator.StringToHash("DeathSouthEast");
    private static readonly int DeathSouthWest = Animator.StringToHash("DeathSouthWest");
}
