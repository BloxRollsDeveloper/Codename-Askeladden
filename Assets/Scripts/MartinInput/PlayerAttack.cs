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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLayer);

        if (enemies.Length > 0)
        {
            foreach (Collider2D enemyCollider in enemies)
            {
                print("I got you bitch!");
                enemyCollider.TryGetComponent(out EnemyHealth health);
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
    
}
