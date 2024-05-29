using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Bounds mapBounds;

    [SerializeField] private float movementSpeed;

    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource audioSource;

    private Vector2 movementInput;
    
    public float stepRate = 0.5f;
    public float stepCoolDown;
    
    [SerializeField] private AudioClip footstepAudioClip;
    private void Awake()
    {
        _playerRb = this.GetComponent<Rigidbody2D>();
        _playerAnimator = this.transform.GetChild(0).GetComponent<Animator>();
        _spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if(GameManager.GameManagerInstance.disableInteract)
        {
            return;
        }

        movementInput = InputManager.MovementInput;

        _playerRb.MovePosition(_playerRb.position + movementInput * movementSpeed * Time.fixedDeltaTime);

        _playerAnimator.SetBool("isMoving", movementInput.x != 0 || movementInput.y != 0);

        if(movementInput.x != 0)
        {
            _playerAnimator.SetFloat("directionIndex", 0);
            _spriteRenderer.flipX = movementInput.x < 0;
        }
        else if(movementInput.y != 0)
        {
            if (movementInput.y > 0)
            {
                _playerAnimator.SetFloat("directionIndex", 1);
            }
            else if (movementInput.y < 0)
            {
                _playerAnimator.SetFloat("directionIndex", 2);
            }
        }

        float clampX = Mathf.Clamp(this.transform.position.x, mapBounds.min.x, mapBounds.max.x);
        float clampY = Mathf.Clamp(this.transform.position.y, mapBounds.min.y, mapBounds.max.y);

        this.transform.position = new Vector3(clampX, clampY, 0);
    }

    private void Update()
    {
        stepCoolDown -= Time.deltaTime;
        if ((movementInput.x != 0f || movementInput.y != 0f) && stepCoolDown < 0f){
            audioSource.pitch = 1f + Random.Range (-0.2f, 0.2f);
            audioSource.PlayOneShot(footstepAudioClip, 0.9f);
            stepCoolDown = stepRate;
        }
    }
    
}
