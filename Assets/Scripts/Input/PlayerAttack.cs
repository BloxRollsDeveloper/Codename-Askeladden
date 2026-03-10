using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Animator anim;
    [SerializeField] private Transform weapon;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float damage;

    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            anim.Play("SwordAttack");
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weapon.position, attackRange, whatIsEnemy);

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
        Gizmos.DrawWireSphere(weapon.position, attackRange);
    }
}
