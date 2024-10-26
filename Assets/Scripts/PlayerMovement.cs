using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    private Transform _groundCheck;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private InputAction _move;
    private InputAction _jump;
    private InputAction _run;
    
    private void Start() {
        _groundCheck = transform.Find("GroundCheck");
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _move = InputSystem.actions.FindAction("Move");
        _jump = InputSystem.actions.FindAction("Jump");
        _jump.started += Jump;
    }

    private void FixedUpdate() {
        var direction = _move.ReadValue<Vector2>().x;
        if(direction != 0) Move(direction);
    }

    private void Move(float direction) {
        _rigidbody2D.velocity = new float2(direction * moveSpeed, _rigidbody2D.velocity.y);
        _spriteRenderer.flipX = direction < 0;
    }

    private void Jump(InputAction.CallbackContext context) {
        if(!IsGround()) return;
        _rigidbody2D.AddForce(new float2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGround() => Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, groundLayer);
}