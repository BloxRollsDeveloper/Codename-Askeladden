using UnityEngine;
using Random = UnityEngine.Random;

public class NewHuldraFR : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Attack,
        Damage,
        Death
    }

    public enum Animation
    {
        Idle,
        Walk,
        Attack,
        Damage,
        Death
    }
    
    public State state = State.Idle;
    public Animation currentAnimation = Animation.Idle;
    
    [Header("Self")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private HuldraAnimationController _animationController;
    private Vector2 _moveDirection;

    private float xDirection;
    private float yDirection;

    [Header("Targeting")] 
    private Transform _target;
    [SerializeField] private float attackRange;
    [SerializeField] private float runAwayRange;
    [SerializeField] private float approachRange;
    
    [Header("Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private float timer;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<HuldraAnimationController>();
        
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        
        xDirection = _target.position.x -  transform.position.x;
        yDirection = _target.position.y -  transform.position.y;
        var distanceToPlayer = Mathf.Sqrt(Mathf.Abs(xDirection * xDirection) + Mathf.Abs(yDirection * yDirection));

        if (xDirection < 0) xDirection = -1f;
        else if (xDirection > 0) xDirection = 1f;
        
        if (yDirection < 0) yDirection = -1f;
        else if (yDirection > 0) yDirection = 1f;

        if (!(distanceToPlayer > runAwayRange))
        {
            state = State.Walk;
            _moveDirection = (_target.position - transform.position).normalized;
            _animationController.UpdateMoveDirection(-1 * _moveDirection);
            SwitchAnimation(Animation.Walk, false,false, false);
            
            _rigidbody2D.linearVelocityX = -1 * xDirection * moveSpeed;
            _rigidbody2D.linearVelocityY = -1 * yDirection * moveSpeed;
        }
        else if (distanceToPlayer > approachRange)
        {
            state = State.Walk;
            _moveDirection = (_target.position - transform.position).normalized;
            _animationController.UpdateMoveDirection(_moveDirection);
            SwitchAnimation(Animation.Walk, false,false, false);
            
            _rigidbody2D.linearVelocityX = xDirection * moveSpeed;
            _rigidbody2D.linearVelocityY = yDirection * moveSpeed;
        }
        else
        {
            print("Huldra should attack.");
            _rigidbody2D.linearVelocity = Vector2.zero;
            state = State.Idle;
            _animationController.UpdateMoveDirection(Vector2.zero);
            SwitchAnimation(Animation.Idle, false, false, false);
            
            timer += Time.deltaTime;

            if (timer >= attackDelay)
            {
                state = State.Attack;
                _animationController.UpdateMoveDirection(_target.position - transform.position); 
                SwitchAnimation(Animation.Attack, true, false, false);
            }
        }
        
        if (true) return;
        
        /* TODO: Remove this
         
        if (_target == null) return;
        
        var dist = Vector2.Distance(_target.position, transform.position);
        if (dist <= runAwayRange)
        {
            ResetTargetPoint();
            Walk();
            return;
        }
        
        if (dist <= attackRange)
        {
            state = State.Attack;
            _animationController.UpdateMoveDirection(_target.position - transform.position);
            SwitchAnimation(Animation.Attack, true, false, false);
            return;
        }

        if (Vector2.Distance(_targetPoint, transform.position) > targetMargin)
        {
            Walk();
        }
        else
        {
            print("I am stopping here!");
            state = State.Idle;
            _rigidbody2D.linearVelocity = Vector2.zero;
            _animationController.UpdateMoveDirection(Vector2.zero);
            SwitchAnimation(Animation.Idle, false, false, false);
        }

        if (Random.Range(1, changeRarity) > 1) return;
        ResetTargetPoint();
        */
    }

    private void SwitchAnimation(Animation anim, bool isAttacking, bool isDamaged, bool isDead)
    {
        if (currentAnimation != anim)
        {
            _animationController.UpdateAnimation(isAttacking, isDamaged, isDead);
            currentAnimation = anim;
        }
    }

    public void AttackPlayer()
    {
        var playerDirection = _target.position - transform.position;
        var projectileClone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        Destroy(projectileClone, projectileLifetime);
        
        projectileClone.TryGetComponent(out Rigidbody2D projectileRb);
        projectileRb.linearVelocity = playerDirection.normalized * projectileSpeed;

        timer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, approachRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runAwayRange);
    }
}
