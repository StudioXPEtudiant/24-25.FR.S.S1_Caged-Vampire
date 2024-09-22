using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private LayerMask groundLayer;
    
    private Transform _groundCheck;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    
    private void Start() {
        _groundCheck = transform.Find("GroundCheck");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        var h = Input.GetAxis("Horizontal");
        if(h != 0) _spriteRenderer.flipX = h < 0;
        
        if(Input.GetKeyDown(KeyCode.W)) Jump();
        
        // Movement
        if(Input.GetKey(KeyCode.LeftShift)) Move(h, runSpeed);
        else Move(h, moveSpeed);
    }
    
    private void Move(float value, float speed) {
        var movement = new float3(value, 0f, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void Jump() {
        if(!Physics2D.OverlapCircle(_groundCheck.position, 0.2f, groundLayer)) return;
        _rigidbody2D.AddForce(new float2(0f, jumpForce), ForceMode2D.Impulse);
    }
}