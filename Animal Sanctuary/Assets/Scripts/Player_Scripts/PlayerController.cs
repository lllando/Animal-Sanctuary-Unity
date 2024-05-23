using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _playerRb;

    private void Awake()
    {
        _playerRb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _playerRb.MovePosition(_playerRb.position + InputManager.MovementInput * movementSpeed * Time.fixedDeltaTime);
        Debug.Log(InputManager.MovementInput);
    }
}
