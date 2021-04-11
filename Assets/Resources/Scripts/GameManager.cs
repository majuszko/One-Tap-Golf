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
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Vector2 force;
    public static GameManager gm;
    public Trajectory traj;
    public Ball ball;
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
        if (Input.GetMouseButtonUp(0))
        {
            dragged = false;
            OnDragEnd();
            
            print(endPosition);
            print(holdDownTime);
        }

        if (dragged)
        {
            OnDrag();
        }
    }

    void OnDragStart()
    {
        ball.DesactivateRb();
        startPosition = ball.pos; //TODO zmienić tu na transform.position i odejmowac wraz z czasem x i y
        traj.Show();
    }
    void OnDrag()
    {
        holdDownTime = Time.time - holdDownStartTime;
        endPosition = new Vector2(ball.pos.x-holdDownTime, ball.pos.y-holdDownTime);//cam.ScreenToWorldPoint(Input.mousePosition); //TODO to co wyzej max -8f,-5f
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
    }
}
