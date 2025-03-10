using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;

    public float speedFactor = 5.0f;
    public float jumpForce = 8.0f;
    public Animator animator;

    private int groundLayerMask;
    private bool isAlive = true;

    private float lastClickTime = 0f;
    public float doubleClickThreshold = 0.3f;

    // 🔹 Attack Detection Variables
    public float attackRange = 2.0f; 
    public LayerMask enemyLayer; 

    void Start()
    {
        rb2d ??= GetComponent<Rigidbody2D>();
        playerCollider ??= GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        groundLayerMask = LayerMask.GetMask("Ground");

        if (enemyLayer == 0)
        {
            Debug.LogWarning("Enemy layer is NOT set in Inspector! Set it to 'Enemy'.");
        }
    }

    void Update()
    {
        FlipSprite();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!isAlive || !IsGrounded()) return;

        if (value.isPressed)
        {
            Jump();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (!isAlive || !value.isPressed) return; 

        // for the range ke if the player is in the range or not 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
           Debug.Log(hitEnemies.Length); // tells us how many enemies are in the range 
        if (hitEnemies.Length > 0)
        {
            Debug.Log("✅ Enemy detected in range! Attacking...");
            
            foreach (Collider2D enemeyCollider in hitEnemies) 
            {
                Enemey enemey = enemeyCollider.GetComponent<Enemey>();  // created the local variable and then stored it 
                if (enemey != null)  
                {
                    enemey.TakeDamage(20); 
                }
            }

            CheckDoubleClick();
        }
        else
        {
            Debug.Log("❌ No enemies in range. Attack canceled.");
        }
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * speedFactor, rb2d.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("run", true);
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    void Jump()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        animator.SetTrigger("jump");
    }

    bool IsGrounded()
    {
        return playerCollider.IsTouchingLayers(groundLayerMask);
    }

    void Fire()
    {
        animator.SetTrigger("fire");
        Debug.Log("🔥 Fire Attack!");
    }

    void DoubleFire()
    {
        animator.SetTrigger("doubleFire");
        Debug.Log("🔥🔥 Double Fire Attack!");
    }

    void CheckDoubleClick()
    {
        float currentTime = Time.time;

        if (currentTime - lastClickTime <= doubleClickThreshold)
        {
            DoubleFire();
        }
        else
        {
            Fire();
        }

        lastClickTime = currentTime;
    }

    // 🔹 Draw Attack Range in Scene View
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
