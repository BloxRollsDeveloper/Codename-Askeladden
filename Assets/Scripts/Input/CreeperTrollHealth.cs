using UnityEngine;
using Unity.Cinemachine;

public class CreeperTrollHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    private BabyCreeperAnimationController _animController;
    
    private void Start()
    {
        _animController = GetComponent<BabyCreeperAnimationController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            _animController.UpdateAnimation(false, false, false, true);
            Destroy(gameObject, 0.5f);
            TryGetComponent(out CinemachineImpulseSource impulse);
            impulse.GenerateImpulse();
        }
        
        _animController.UpdateAnimation(false, false, true, false);
    }
}
