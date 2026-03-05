using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public float damage;
    public Transform attackCentre;
    public LayerMask enemyLayer;

    private bool DidAttackHitEnemy()
    {
        return Physics2D.OverlapCircle(attackCentre.forward, attackRange, enemyLayer);
    }

    public void UpdateAttack()
    {
        if (DidAttackHitEnemy())
        {
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
    
}
