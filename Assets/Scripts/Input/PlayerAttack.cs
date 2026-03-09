using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform weapon;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;
    

    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            anim.Play("SwordAttack");
            CheckForEnemies();
        }
        else
        {
            anim.Play("SwordHover");
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weapon.position, attackRange, whatIsEnemy);

        if (enemies.Length > 0)
        {
            print("enemies found");
            // TODO: get the enemy health script from here and affect them.
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weapon.position, attackRange);
    }
}
