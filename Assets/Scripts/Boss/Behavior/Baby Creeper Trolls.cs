using System;
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
   public float stopRange;
   public bool canChase;
   public bool stop;

   private void Start()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      canChase = true;
   }

   private void Update()
   {
      if (canChase)
      {
         
      }
   }

   private void FixedUpdate()
   {
      if (canChase)
      {
         
      }
   }
}
