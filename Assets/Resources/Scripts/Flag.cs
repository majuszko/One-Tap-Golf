using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] private GameObject ground;
    
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ground = GameObject.Find("Ground");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        gm.BallInHole();
        ground.GetComponent<BoxCollider2D>().enabled = false;
        
    }

}
