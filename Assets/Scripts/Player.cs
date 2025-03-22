using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float torqueForce = 5f;

    [Header("References")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isJumping;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null) Debug.LogError("❌ Rigidbody2D is missing!");
        if (animator == null) Debug.LogError("❌ Animator is missing!");

        Debug.Log("PlayerMovement script initialized.");
    }

    private void Update()
    {
        CheckGrounded();
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            isJumping = false;
            Debug.Log("Player has landed.");
        }

        Debug.Log("IsGrounded: " + isGrounded);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            Jump();
            isJumping = true;
        }
        else if (value.isPressed && !isGrounded)
        {
            Debug.Log("Attempted to jump while not grounded.");
        }
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Fire();
            Debug.Log("Fire input received.");
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        Debug.Log("Velocity: " + rb.linearVelocity);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        rb.AddTorque(torqueForce * -Mathf.Sign(rb.linearVelocity.x));
        Debug.Log("Jumped with force: " + jumpForce);
    }

    private void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Fired projectile from: " + firePoint.position);
    }

    private void UpdateAnimationState()
    {
        if (isJumping)
        {
            animator.SetBool("idle", false);
            animator.SetBool("Run", false);
            animator.SetTrigger("Jump");
            Debug.Log("Animation: Jump");
        }
        else
        {
            bool isIdle = rb.linearVelocity.magnitude < 0.01f;
            animator.SetBool("idle", isIdle);
            animator.SetBool("Run", !isIdle);

            Debug.Log(isIdle ? "Animation: Idle" : "Animation: Run");
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
