using UnityEngine;
using Direction = PlayerDirection;

public class PlayerAttack : MonoBehaviour
{
    /* TODO:
     * : add a transform for each direction, maybe?
     */
    
    [Header("Direction")]
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private Vector2 moveVector;
    private Rigidbody2D _rigidbody2D;
    
    
    [Header("Weapon")]
    [SerializeField] private Transform activeAttackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float damage;

    private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();
    
    
    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            CheckForEnemies();
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.linearVelocity != Vector2.zero)
        {
            moveVector = _rigidbody2D.linearVelocity.normalized;
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(UpdateSpawnPosition(), attackRange, whatIsEnemy);

        if (enemies.Length > 0)
        {
            print("enemies found");
            foreach (Collider2D col in enemies)
            {
                col.TryGetComponent(out Enemy_Health health);
                health.TakeDamage(damage);
            }
        }
    }
    
    private Vector2 UpdateSpawnPosition()
    {
        spawnPosition.x = transform.localPosition.x + (moveVector.x / 2);
        spawnPosition.y = transform.localPosition.y + (moveVector.y / 2);

        if (spawnPosition == Vector2.zero)
        {
            spawnPosition.x = transform.localPosition.x + 0.5f;
        }
        return spawnPosition;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(UpdateSpawnPosition(), attackRange);
    }
}
