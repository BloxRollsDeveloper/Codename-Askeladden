using System;
using UnityEngine;

public class Huldra : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    
    [Header("Self")]
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody;
    public float moveSpeed;
    public bool canBeAttacked;
    private Animator _animator;
    
    [Header("Range")]
    public float attackRange;
    public float runRange;
    public bool running;

    [Header("Attack")]
    [SerializeField] private float timer;
    [SerializeField] private float resetTimer;
    [SerializeField] private float damage;
    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private bool attacking;
    
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileCooldown;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private GameObject projectile;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        resetTimer = timer;
    }

    private void Update()
    {
        if (target == null) return;

        if (Vector2.Distance(target.position, transform.position) <= runRange && !canBeAttacked)
        {
            running = true;
            _moveDirection = -target.position + transform.position;
        }
        else if (Vector2.Distance(target.position, transform.position) >= attackRange)
        {
            running = false;
            _rigidbody.linearVelocity = Vector2.zero;
            canBeAttacked = true;
            
            timer += Time.deltaTime;

            if (timer >= attackDelay)
            {
                StartAttacking();
            }
        }
    }

    private void FixedUpdate()
    {
        if (running) _rigidbody.linearVelocity = _moveDirection * moveSpeed;
    }

    /*private void Attacking()
    {
        attacking = true;
        print("Get charmed");
        
        
        
        timer = resetTimer;
    }*/

    public void StartAttacking()
    {
        attacking = true;
        Vector3 pdirection = transform.position - target.position;
        var targetPos = target.position;
        var projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileClone.TryGetComponent(out Rigidbody2D projectileRb);
        
        projectileRb.linearVelocity = pdirection.normalized * projectileSpeed;
        Destroy(projectileClone, projectileLifetime);
    }

    public void StopAttacking()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
