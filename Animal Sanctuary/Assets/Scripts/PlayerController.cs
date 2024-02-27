using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;

    private Rigidbody2D rb;

    private Vector2 move;
    public float moveSpeed;

    private float hInput;
    private float vInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
            
    }

    private void Update()
    {
        // hInput = Input.GetAxis("Horizontal");
        // vInput = Input.GetAxis("Vertical");

        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        // rb.velocity = new Vector2(hInput * moveSpeed, vInput * moveSpeed)
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
