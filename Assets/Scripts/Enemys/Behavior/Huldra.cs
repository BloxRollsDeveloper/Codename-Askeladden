using UnityEngine;

public class Huldra : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    
    [Header("Self")]
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody;
    public float moveSpeed;
    
    [Header("Range")]
    public float attackRange;
    public float runRange;
    public bool canAttack;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null) return;

        if (Vector2.Distance(target.position, transform.position) <= runRange)
        {
            canAttack = false;
            /*_rigidbody.linearVelocity = */
        }
    }
}
