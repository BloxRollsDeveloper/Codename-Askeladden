using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public float damage;
    public Transform attackCentre;
    public LayerMask enemyLayer;
    
    private Collider2D DidAttackHitEnemy()
    {
        return Physics2D.OverlapCircle(attackCentre.forward, attackRange, enemyLayer);
    }

    public void UpdateAttack()
    {
        Collider2D enemyCollider = DidAttackHitEnemy();
        
        if (enemyCollider != null)
        {
            print("I am attacking!");
            enemyCollider.TryGetComponent(out EnemyHealth health);
            health.TakeDamage(damage);
        }
        else
        {
            print("I don't know what hit!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
    
}
