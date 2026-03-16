using UnityEngine;
using Unity.Cinemachine;

public class BabyCreeperTrolls : MonoBehaviour
{
   [Header("Target")]
   public Transform target;
   
   [Header("Self")]
   private Vector2 _moveDirection;
   private Rigidbody2D _rigidbody2D;
   public float speed;

   [Header("Range")]
   public float attackRange;
   public bool canChase;

   [Header("Attack")]
   [SerializeField] private float attackDelay = 5f;
   [SerializeField] private float knockBackForce;
   [SerializeField] private float damage;
   [SerializeField] private bool attacking;
   [SerializeField] private float timer = 0f;
   
   private void Start()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      target = GameObject.FindGameObjectWithTag("Player").transform;
      canChase = true;
   }

   private void Update()
   {
      if (target ==null) return;
      
      if (canChase)
      {
         _moveDirection = target.position - transform.position;
      }

      if (Vector2.Distance(target.position, transform.position) <= attackRange)
      {
         canChase = false;
         if (attacking) return;
         
         _rigidbody2D.linearVelocity = Vector2.zero;
         print("This could be an animation");
         
         timer += Time.deltaTime;

         if (timer >= attackDelay)
         {
            Attacking();
         }
      }
   }

   private void FixedUpdate()
   {
      if (canChase) _rigidbody2D.linearVelocity = _moveDirection * speed;
   }

   private void Attacking()
   {
      attacking = true;
      print("Kablooey");
      
      Destroy(gameObject, 0.2f);
      
      TryGetComponent(out CinemachineImpulseSource impulse);
      impulse.GenerateImpulse();
      
      Vector3 direction = -(transform.position - target.position).normalized;
      Vector2 force = direction * knockBackForce;
      
      target.TryGetComponent(out PlayerController playerController);
      playerController.isKnockedBack = true;
      
      target.TryGetComponent(out PlayerHealth playerHealth);
      playerHealth.TakeDamage(damage);
      
      target.TryGetComponent(out Rigidbody2D playerBody);
      playerBody.linearVelocity = force;
      print("I Am Dead");
   }
   
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackRange);
   }
}
