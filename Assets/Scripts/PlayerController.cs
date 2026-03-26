using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField]
    private InputActionReference moveAction;
    [SerializeField]
    private InputActionReference jumpAction;

    [Header("Walk Settings")]
    [SerializeField]
    private float walkSpeed = 7f;
    [SerializeField]
    private float acceleration = 5f;
    [SerializeField]
    private float deceleration = 15f;

    [Header("Jump Settings")]
    [SerializeField]
    private float jumpHeight = 2.5f;
    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Vector2 _moveInput;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        moveAction.action.performed += HandleWalkInput;
        moveAction.action.canceled += HandleWalkInput;
        jumpAction.action.performed += HandleJumpInput;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= HandleWalkInput;
        moveAction.action.canceled -= HandleWalkInput;
        jumpAction.action.performed -= HandleJumpInput;
    }

    private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        if (_isGrounded)
        {
            var actualGravity = Mathf.Abs(Physics2D.gravity.y) * _rb.gravityScale;
            _rb.linearVelocityY = Mathf.Sqrt(2 * jumpHeight * actualGravity);
        }
    }

    private void HandleWalkInput(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        _isGrounded = GroundCheck();
        HandleWalking();
    }

    private void HandleWalking()
    {
        var change = _moveInput.x != 0 ? acceleration : deceleration;
        _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, _moveInput.x * walkSpeed, Time.deltaTime * change);
    }

    private bool GroundCheck()
    {
        var colliderBottomCenter = new Vector2(
            _collider.bounds.center.x,
            _collider.bounds.min.y);

        return Physics2D.OverlapBox(
            colliderBottomCenter,
            new Vector2(_collider.bounds.size.x - 0.1f, 0.1f), 0, groundLayer);
    }
}
