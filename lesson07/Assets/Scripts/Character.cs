using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    public Transform Visual;
    public float MoveForce;
    public float JumpForce;
    public PlayerLose playerLose;
    public Camera cam;
    public Vector3 point;

    private Rigidbody2D rigidBody2D;
    private TriggerDetector triggerDetector;
    private Animator animator;
    private float visualDirection;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Awake()
    {
        cam = Camera.main;
    }
    protected virtual void Start()
    {        
        visualDirection = 1.0f;
        rigidBody2D = GetComponent<Rigidbody2D>();
        triggerDetector = GetComponentInChildren<TriggerDetector>();
        animator = GetComponentInChildren<Animator>();
    }

    public void MoveLeft()
    {
        //if (triggerDetector.InTrigger)
            rigidBody2D.AddForce(new Vector2(-MoveForce, 0), ForceMode2D.Force);
    }

    public void MoveRight()
    {
        //if (triggerDetector.InTrigger)
            rigidBody2D.AddForce(new Vector2(MoveForce, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        if (triggerDetector.InTrigger)
            rigidBody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Force);
    }

    protected virtual void Update()
    {
        //point = cam.WorldToScreenPoint(transform.position);
        //if (point.y < 0f || point.x < 0f)
        //{
        //    Died();
        //}
        float vel = rigidBody2D.velocity.x;

        if (vel < -0.001f)
            visualDirection = -1.0f;
        else if (vel > 0.001f)
            visualDirection = 1.0f;

        Vector3 scale = Visual.localScale;
        scale.x = visualDirection;
        Visual.localScale = scale;

        animator.SetFloat(Speed, Mathf.Abs(vel));
    }

    public void Died()
    {
        playerLose.PlayerLoseMenuView();
    }
}

