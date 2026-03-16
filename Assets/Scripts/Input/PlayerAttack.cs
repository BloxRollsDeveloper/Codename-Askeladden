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
    
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        _playerController.animationState = PlayerController.AnimationState.Attack;
        
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

    public void UpdateAttackDirection(PlayerDirection direction)
    {
        switch (direction.direction)
        {
            case PlayerDirection.Direction.North:
                anim.Play("AttackNorth");
                activeAttackPoint = attackPoint[0];
                break;
            case PlayerDirection.Direction.East:
                anim.Play("AttackEast");
                activeAttackPoint = attackPoint[1];
                break;
            case PlayerDirection.Direction.South:
                anim.Play("AttackSouth");
                activeAttackPoint = attackPoint[2];
                break;
            case PlayerDirection.Direction.West:
                anim.Play("AttackWest");
                activeAttackPoint = attackPoint[3];
                break;
            case PlayerDirection.Direction.NorthEast:
                anim.Play("AttackNorthEast");
                activeAttackPoint = attackPoint[4];
                break;
            case PlayerDirection.Direction.NorthWest:
                anim.Play("AttackNorthWest");
                activeAttackPoint = attackPoint[5];
                break;
            case PlayerDirection.Direction.SouthEast:
                anim.Play("AttackSouthEast");
                activeAttackPoint = attackPoint[6];
                break;
            case PlayerDirection.Direction.SouthWest:
                anim.Play("AttackSouthWest");
                activeAttackPoint = attackPoint[7];
                break;
        }
    }
    
}
