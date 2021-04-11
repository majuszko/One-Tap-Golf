using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D coll;
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
    }

    public void Go(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }
    public void DesactivateRb()
    {
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        
    }
    
}
