using UnityEngine;
using Direction = PlayerDirection;

public class PlayerAttack : MonoBehaviour
{
    /* TODO:
     * : add a transform for each direction, maybe?
     */
    
    [Header("Direction")]
    [SerializeField] private Transform[] attackPoint;
    
    
    [Header("Weapon")]
    [SerializeField] private Transform activeAttackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float damage;
    
    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(activeAttackPoint.position, attackRange, whatIsEnemy);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
