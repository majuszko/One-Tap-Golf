using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    private bool dragged = false;
    private float distance;
    private float holdDownStartTime;
    private float holdDownTime;
    private float maxHoldDownTime = 3f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Vector2 force;
    public static GameManager gm;
    public Trajectory traj;
    public Ball ball;
    public Flag flag;
    [SerializeField] private float goForce = 4f;
    

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        
    }

    void Start()
    {
        cam = Camera.main;
        ball.DesactivateRb();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragged = true;
            OnDragStart();
            holdDownStartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0)&&dragged==true)
        {
            dragged = false;
            OnDragEnd();
        }

        if (dragged)
        {
            double x = Math.Round(holdDownTime, 0);
            OnDrag();
            if (x == maxHoldDownTime)
            {
                dragged = false;
                OnDragEnd();
            }
        }
    }

    void OnDragStart()
    {
        ball.DesactivateRb();
        startPosition = ball.pos; 
        traj.Show();
    }

    void OnDrag()
    {
        holdDownTime = Time.time - holdDownStartTime;
        endPosition = new Vector2(ball.pos.x - holdDownTime, ball.pos.y - holdDownTime); 
        distance = Vector2.Distance(startPosition, endPosition);
        direction = (startPosition - endPosition).normalized;
        force = direction * distance * goForce;

        Debug.DrawLine(startPosition, endPosition);
        traj.UpdateDots(ball.pos, force);
        
    }
    void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Go(force);
        traj.Hide();
        //TODO a timer so i can check if u lost or not
    }

    public void GameWon()
    {
        Debug.Log("YOU WON");
    }

    void GameLost()
    {
        Debug.Log("YOU LOST");
    }
    
}
