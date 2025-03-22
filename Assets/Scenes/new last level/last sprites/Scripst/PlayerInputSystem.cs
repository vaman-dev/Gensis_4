using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("References")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null) Debug.LogError("❌ Rigidbody2D is missing!");
        if (animator == null) Debug.LogError("❌ Animator is missing!");

        Debug.Log("🌄 PlayerInputSystem script initialized.");
    }

    private void Update()
    {
        CheckGrounded();
        UpdateAnimationState();
        FlipSprite();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Check for landing
        if (!wasGrounded && isGrounded)
        {
            isJumping = false;
            Debug.Log("🏃‍♂️ Player has landed.");
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("🌄 Move Input: " + moveInput);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isJumping = true;
        Debug.Log("🦘 Player has jumped!");
    }

    private void UpdateAnimationState()
    {
        bool isIdle = Mathf.Abs(rb.linearVelocity.x) < 0.1f;
        animator.SetBool("idle", isIdle);
        animator.SetBool("Run", !isIdle);
        animator.SetBool("Jump", isJumping);
    }

    private void FlipSprite()
    {
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
