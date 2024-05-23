using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector2 MovementInput;

    private PlayerInput _playerInput;

    private void Awake()
    {
        InitialiseInput();
    }

    private void InitialiseInput()
    {
        _playerInput = new PlayerInput();
    }

    private void FixedUpdate()
    {
        MovementInput = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
