using System;
using System.Collections;
using UnityEngine;

public class Huldra : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    
    [Header("Self")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canBeAttacked;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody;
    private HuldraAnimationController _animationController;
    
    [Header("Range")]
    [SerializeField] private float attackRange;
    [SerializeField] private float runRange;
    [SerializeField] private bool running;

    [Header("Attack")]
    [SerializeField] private float attackDelay;
    [SerializeField] private bool attacking;
    [SerializeField] private bool hasShot;
    
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private GameObject projectile;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<HuldraAnimationController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _animationController.UpdateMoveDirection(_moveDirection);
        
        if (target == null) return;

        if (Vector2.Distance(target.position, transform.position) <= runRange && !canBeAttacked)
        {
            running = true;
            _moveDirection = -target.position + transform.position;
        }
        else if (Vector2.Distance(target.position, transform.position) >= attackRange)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            running = false;
            canBeAttacked = true;
        }
    }

    private void FixedUpdate()
    {
        if (running) _rigidbody.linearVelocity = _moveDirection * moveSpeed;
        
        if (_rigidbody.linearVelocity == Vector2.zero)
        {
            if (attacking) return;
            
            if (!hasShot)
            {
                _animationController.UpdateAnimation(true, false, false);
                StartCoroutine(Attacking());
            }
        }
    }

    private IEnumerator Attacking()
    {
        attacking = true;
        Vector3 pdirection = -target.position - transform.position;
        var projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileClone.TryGetComponent(out Rigidbody2D projectileRb);

        projectileRb.linearVelocity = pdirection.normalized * projectileSpeed;
        hasShot = true;
        Destroy(projectileClone, projectileLifetime);
        
        yield return new WaitForSeconds(attackDelay);
        
        attacking = false;
        canBeAttacked = false;
        hasShot = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
