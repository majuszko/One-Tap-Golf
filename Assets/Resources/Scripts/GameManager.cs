using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private bool dragged = false;
    private bool shot = false;
    private bool ballInHole = false;
    private int multiplier=1;
    private float distance;
    private float holdDownStartTime;
    private float holdDownTime;
    private float maxHoldDownTime = 2f;
    private float rand;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Vector2 force;
    private Vector2 target;
    private GameObject flagClone;
    public int points=0;
    public static GameManager gm;
    public Trajectory traj;
    public Ball ball;
    public Score score;
    public GameObject flagPref;
    [SerializeField] private float goForce = 4f;
    [SerializeField] GameObject ground;
    [SerializeField] private GameObject GameOverPanel;

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }
    void Start()
    {
        rand = Random.Range(-2.0f, 8.0f);
        ball.DesactivateRb();
        flagClone = (GameObject)Instantiate(flagPref, new Vector3(rand, 0, 0), Quaternion.identity);
        target = ball.pos;
        ground = GameObject.Find("Ground");
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
        if (ballInHole)
        {
            rand = Random.Range(-2.0f, 8.0f);
            ball.DesactivateRb();
            flagClone = (GameObject)Instantiate(flagPref, new Vector3(rand, 0, 0), Quaternion.identity);
            ballInHole = false;
            shot = false;
            ground.GetComponent<BoxCollider2D>().enabled = true;
            ball.transform.position = target;
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
        Invoke("GameLost", 4f);
    }
    
    public void BallInHole()
    {
        points += 1;
        score.ScoreAdd();
        multiplier += points / 10;
        Destroy(flagClone,0f);
        ballInHole = true;
        CancelInvoke();
    }
    void GameLost()
    {
        GameOverPanel.SetActive(true);
    }
    
}
