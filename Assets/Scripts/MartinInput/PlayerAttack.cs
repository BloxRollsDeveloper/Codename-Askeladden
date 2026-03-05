using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public float damage;
    public Transform attackCentre;
    public LayerMask enemyLayer;

    private Collider2D enemyCollider;
    
    private bool DidAttackHitEnemy()
    {
        return enemyCollider = Physics2D.OverlapCircle(attackCentre.forward, attackRange, enemyLayer);
    }

    public void UpdateAttack()
    {
        if (DidAttackHitEnemy())
        {
            print("I am attacking!");
            enemyCollider.TryGetComponent(out EnemyHealth health);
            health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
    
}
