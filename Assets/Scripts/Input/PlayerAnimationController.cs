using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float attackAnimTime;
    [SerializeField] private float damageAnimTime;
    [SerializeField] private float deathAnimTime;
    
    private Animator _animator;
    private bool isWalking;

    private float _lockedTill;
  
    private void Awake() => _animator = GetComponent<Animator>();

    private void Update()
    {
        print(_animator.GetCurrentAnimatorClipInfo(0));
    }

    public void UpdateMoveDirection(Vector2 movementDirection)
    {
        if (movementDirection != Vector2.zero)
        {
            _animator.SetFloat("Horizontal", movementDirection.x);
            _animator.SetFloat("Vertical", movementDirection.y);
        }
        
        var direction = movementDirection.normalized;
        isWalking = direction == Vector2.zero;
    }
    
    public void UpdateAnimation(bool attack, bool takeDamage, bool death)
    {
        var nextAnimation = GetAnimation(attack, takeDamage, death);

        if (nextAnimation == _currentAnimation) return;
        _animator.Play(nextAnimation);
        _currentAnimation = nextAnimation;
    }

    private int GetAnimation(bool attacking, bool takeDamage, bool isDying)
    {
        if (Time.time < _lockedTill) return _currentAnimation;
        
        if (isDying) return LockAnimation(Death, deathAnimTime);
        if (takeDamage) return LockAnimation(Damage, damageAnimTime);
        if (attacking) return LockAnimation(Attack, attackAnimTime);
        
        //return direction != Vector2.zero ? Walk : Idle;
        return isWalking ? Idle : Walk;
        
        int LockAnimation(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
    
    private int _currentAnimation;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Death = Animator.StringToHash("Death");
    
}
