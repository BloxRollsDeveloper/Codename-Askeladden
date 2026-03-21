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

    [Header("Target")] 
    private Transform _target;
    private Vector3 _targetPoint;
    [SerializeField] private float targetDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private float runRange;
    [SerializeField] private float targetMargin; // how close huldra needs to be to a point to stop.

    public int changeRarity = 25;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<HuldraAnimationController>();
        
        _targetPoint = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        
        var dist = Vector2.Distance(_target.position, transform.position);
        if (dist <= runRange)
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
            state = State.Idle;
            _rigidbody2D.linearVelocity = Vector2.zero;
            _animationController.UpdateMoveDirection(Vector2.zero);
            SwitchAnimation(Animation.Idle, false, false, false);
        }

        if (Random.Range(1, changeRarity) > 1) return;
        ResetTargetPoint();
    }

    private void SwitchAnimation(Animation anim, bool isAttacking, bool isDamaged, bool isDead)
    {
        if (currentAnimation != anim)
        {
            _animationController.UpdateAnimation(isAttacking, isDamaged, isDead);
            currentAnimation = anim;
        }
    }

    private void ResetTargetPoint()
    {
        _targetPoint = Vector3.Normalize(_target.position - transform.position) * (Vector2.Distance(_target.position, transform.position) - targetDistance);
    }

    private void Walk()
    {
        state = State.Walk;
        _moveDirection = (_targetPoint - transform.position).normalized;
        _animationController.UpdateMoveDirection(_moveDirection);
        SwitchAnimation(Animation.Walk, false,false, false);
            
        _rigidbody2D.linearVelocity = _moveDirection * moveSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runRange);
    }
}
