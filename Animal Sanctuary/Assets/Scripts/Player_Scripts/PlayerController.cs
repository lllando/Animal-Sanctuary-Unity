using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Bounds mapBounds;

    [SerializeField] private float movementSpeed;

    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerRb = this.GetComponent<Rigidbody2D>();
        _playerAnimator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 movementInput = InputManager.MovementInput;

        _playerRb.MovePosition(_playerRb.position + movementInput * movementSpeed * Time.fixedDeltaTime);

        _playerAnimator.SetBool("isMoving", movementInput.x != 0 || movementInput.y != 0);

        if(movementInput.x != 0)
        {
            if (movementInput.x > 0)
            {
                _playerAnimator.SetFloat("directionIndex", 0);
            }
            else if (movementInput.x < 0)
            {
                _playerAnimator.SetFloat("directionIndex", 1);
            }
        }
        else if(movementInput.y != 0)
        {
            if (movementInput.y > 0)
            {
                _playerAnimator.SetFloat("directionIndex", 2);
            }
            else if (movementInput.y < 0)
            {
                _playerAnimator.SetFloat("directionIndex", 3);
            }
        }

        float clampX = Mathf.Clamp(this.transform.position.x, mapBounds.min.x, mapBounds.max.x);
        float clampY = Mathf.Clamp(this.transform.position.y, mapBounds.min.y, mapBounds.max.y);

        this.transform.position = new Vector3(clampX, clampY, 0);
    }
}
