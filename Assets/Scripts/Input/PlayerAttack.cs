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
    [SerializeField] private Animator anim;
    [SerializeField] private Transform activeAttackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float damage;
    [SerializeField] private float attackAnimTime;
    
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

    /*
    public void UpdateAttackDirection(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                anim.CrossFade(North, attackAnimTime);
                activeAttackPoint = attackPoint[0];
                break;
            case PlayerDirection.Direction.East:
                anim.CrossFade(East, attackAnimTime);
                activeAttackPoint = attackPoint[1];
                break;
            case PlayerDirection.Direction.South:
                anim.CrossFade(South, attackAnimTime);
                activeAttackPoint = attackPoint[2];
                break;
            case PlayerDirection.Direction.West:
                anim.CrossFade(West, attackAnimTime);
                activeAttackPoint = attackPoint[3];
                break;
            case PlayerDirection.Direction.NorthEast:
                anim.CrossFade(NorthEast, attackAnimTime);
                activeAttackPoint = attackPoint[4];
                break;
            case PlayerDirection.Direction.NorthWest:
                anim.CrossFade(NorthWest, attackAnimTime);
                activeAttackPoint = attackPoint[5];
                break;
            case PlayerDirection.Direction.SouthEast:
                anim.CrossFade(SouthEast, attackAnimTime);
                activeAttackPoint = attackPoint[6];
                break;
            case PlayerDirection.Direction.SouthWest:
                anim.CrossFade(SouthWest, attackAnimTime);
                activeAttackPoint = attackPoint[7];
                break;
        }
    }
    
    // Attack Animations
    private static readonly int North = Animator.StringToHash("AttackNorth");
    private static readonly int East = Animator.StringToHash("AttackEast");
    private static readonly int South = Animator.StringToHash("AttackSouth");
    private static readonly int West = Animator.StringToHash("AttackWest");
    private static readonly int NorthEast = Animator.StringToHash("AttackNorthEast");
    private static readonly int NorthWest = Animator.StringToHash("AttackNorthWest");
    private static readonly int SouthEast = Animator.StringToHash("AttackSouthEast");
    private static readonly int SouthWest = Animator.StringToHash("AttackSouthWest"); */
    
}
