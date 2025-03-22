using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    private Vector2 move;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f; 
    public float torqueForce = 5f; 
    public float delayTime = 2f; 

    [Header("Component References")]
    private Animator animator;
    private BoxCollider2D feetCollider;
    private SpriteRenderer spriteRenderer;
    public AudioSource runAudio; // Running sound audio source

    private bool isAlive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("✅ PlayerMovement2 script has started.");
    }

    private void Update()
    {
        if (isAlive)
        {
            ProcessInput();  // Handle movement input
            Run();           // Apply movement
            FlipSprite();    // Handle sprite flipping
            ApplyTorque();   // Apply torque for rotation
            Fire();          // Trigger fire animation
        }
    }

    /// <summary>
    /// Process player input using traditional GetAxisRaw method.
    /// </summary>
    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // -1, 0, or 1
        move = new Vector2(moveX, 0);
    }

    /// <summary>
    /// Apply horizontal movement to the player.
    /// </summary>
    private void Run()
    {
        rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool("Moving", isMoving);
        animator.SetBool("Idle", !isMoving);

        // Play running audio if moving, stop if not
        if (isMoving && !runAudio.isPlaying)
        {
            runAudio.Play();
        }
        else if (!isMoving)
        {
            runAudio.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gate"))
        {
            Debug.Log("🚪 You have reached the gate.");
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            rb.AddTorque(torqueForce * -Mathf.Sign(rb.linearVelocity.x));
        }
    }

    private void ApplyTorque()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torqueForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torqueForce);
        }
    }

    private void FlipSprite()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0)) // Left Mouse Button
        {
            animator.SetTrigger("fire");
        }
    }

    /// <summary>
    /// Public method to enable this script.
    /// </summary>
    public void EnableThisScript()
    {
        this.enabled = true;
        Debug.Log("✅ PlayerMovement2 script has been enabled.");
    }

    /// <summary>
    /// Public method to disable this script.
    /// </summary>
    public void DisableThisScript()
    {
        this.enabled = false;
        Debug.Log("🛑 PlayerMovement2 script has been disabled.");
    }
}
