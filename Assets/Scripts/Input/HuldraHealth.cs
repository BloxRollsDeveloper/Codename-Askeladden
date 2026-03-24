using UnityEngine;
using Unity.Cinemachine;

public class HuldraHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    private HuldraAnimationController _animController;
    
    private void Start()
    {
        _animController = GetComponent<HuldraAnimationController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            _animController.UpdateAnimation(false, false, true);
            Destroy(gameObject, 0.5f);
            TryGetComponent(out CinemachineImpulseSource impulse);
            impulse.GenerateImpulse();
        }
        
        _animController.UpdateAnimation(false, true, false);
    }
}
