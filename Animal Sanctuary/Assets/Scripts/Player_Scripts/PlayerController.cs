using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;

    private Rigidbody2D rb;

    private Vector2 move;
    public float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
            
    }

    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        if (move.x != 0 || move.y != 0)
        {
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
