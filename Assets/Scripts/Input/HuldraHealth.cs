using UnityEngine;
using Unity.Cinemachine;
using Random = UnityEngine.Random;

public class HuldraHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private HuldraScrubby scrubby;
    
    [Header("Drops")]
    [SerializeField] private GameObject dropPrefab;
    
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
            scrubby.isDead = true;
            Destroy(gameObject, 0.5f);
            
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
            
            TryGetComponent(out CinemachineImpulseSource impulse);
            impulse.GenerateImpulse();
        }
        
        _animController.UpdateAnimation(false, true, false);
    }
}
