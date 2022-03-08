using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D obstacle_body;
    private BoxCollider2D obstacle_collider;

    public float moveSpeed ;
    public float left_angle;
    public float right_angle;

    bool moving_clockwise;
    void Start()
    {
        obstacle_body = GetComponent<Rigidbody2D>();
        obstacle_collider = GetComponent<BoxCollider2D>();
        moving_clockwise = true;
    }


    void Update()
    {
        move_Speed();
    }



    private void Change_moveDirection()
    {
        if (transform.rotation.z > right_angle)
            moving_clockwise = false;
        if (transform.rotation.z < left_angle)
            moving_clockwise = true;

    }

    private void move_Speed()
    {
        Change_moveDirection();
        if (moving_clockwise)
            obstacle_body.angularVelocity = moveSpeed;

        if (!moving_clockwise)
            obstacle_body.angularVelocity = -1*moveSpeed;
    }
}
