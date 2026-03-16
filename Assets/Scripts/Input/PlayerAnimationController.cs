using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float attackAnimTime;
    [SerializeField] private float damageAnimTime;
    [SerializeField] private float deathAnimTime;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private int _currentAnimation;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Death = Animator.StringToHash("Death");
    
}
