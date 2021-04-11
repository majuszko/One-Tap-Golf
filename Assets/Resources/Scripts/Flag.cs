using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] GameObject ground;
    private BoxCollider2D[] bc2d;
    

    private void Start()
    {
        bc2d = GetComponentsInChildren<BoxCollider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ground = GameObject.Find("Ground");

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        gm.BallInHole();
        ground.GetComponent<BoxCollider2D>().enabled = false;
        foreach (BoxCollider2D bc in bc2d)
        {
            bc.enabled = true;
        }

}

}
