using System;
using System.Collections;
using UnityEngine;

public class BabyCreeperTrolls : MonoBehaviour
{
   [Header("Target")]
   public Transform target;
   private Vector2 _moveDirection;
   private Rigidbody2D _rigidbody2D;
   
   [Header("Speed")]
   public float speed;

   [Header("Range")]
   public float attackRange;
   public bool canChase;

   [Header("Attack")]
   public float attackTime;
   public float knockBackForce;
   public bool attacking;
   
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
         StartCoroutine(Attacking());
      }
   }

   private void FixedUpdate()
   {
      if (canChase) _rigidbody2D.linearVelocity = _moveDirection * speed;
   }

   private IEnumerator Attacking()
   {
      attacking = true;
      print("Kablooey");
      yield return new WaitForSeconds(0.1f);
      
      Destroy(gameObject, 0.2f);
      
      Vector3 direction = -(target.position - transform.position).normalized;
      Vector2 force = direction * knockBackForce;
      
      target.TryGetComponent(out Rigidbody2D rigidbody2D);
      rigidbody2D.AddForce(force, ForceMode2D.Impulse);
      print("I Am Dead");
   }
   
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackRange);
   }
}
