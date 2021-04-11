using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private bool dragged = false;
    private bool shot = false;
    private int multiplier=1;
    private float distance;
    private float holdDownStartTime;
    private float holdDownTime;
    private float maxHoldDownTime = 2f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Vector2 force;
    private GameObject flagClone;
    public int points=0;
    public static GameManager gm;
    public Trajectory traj;
    public Ball ball;
    public Flag flag;
    public GameObject flagPref;
    
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
        float rand = Random.Range(-2.0f, 8.0f);
        ball.DesactivateRb();
        flagClone = (GameObject)Instantiate(flagPref, new Vector3(rand, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0)&&shot==false)
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
            double x = Math.Round(holdDownTime, 1);
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
        endPosition = new Vector2(ball.pos.x - holdDownTime*multiplier, ball.pos.y - holdDownTime*multiplier); 
        distance = Vector2.Distance(startPosition, endPosition);
        direction = (startPosition - endPosition).normalized;
        force = direction * distance * goForce;
        traj.UpdateDots(ball.pos, force);
        
    }
    void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Go(force);
        traj.Hide();
        shot = true;
        //TODO a timer so i can check if u lost or not
    }

    public void GameWon()
    {
        Debug.Log("YOU WON");
    }
    public void BallInHole()
    {
        points += 1;
        multiplier += points / 10;
        Destroy(flagClone,0.2f);
    }

    void GameLost()
    {
        Debug.Log("YOU LOST");
    }

}
