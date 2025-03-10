using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    private Vector2 move;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Animator animator;
    public float delayTime = 2f; 
    public float jumpForce = 7f; 
    public float torqueForce = 5f; 
    private BoxCollider2D feetCollider;
    private bool isAlive = true;
    private SpriteRenderer spriteRenderer;
    public AudioSource runAudio; // Added AudioSource for running sound

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ApplyTorque();
            fire();
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>(); // Direct assignment, avoiding extra Vector2 creation
    }

    void Run()
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
            Debug.Log("You have reached the gate");
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delayTime); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnJump(InputValue value)
    {
        if (!isAlive || !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            rb.AddTorque(torqueForce * -Mathf.Sign(rb.linearVelocity.x)); // Adding torque for rotation control
            animator.SetTrigger("Jump");
        }
    }

    void ApplyTorque()
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

    void FlipSprite()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f) 
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }
    }
   void fire()
    {
        if (Input.GetMouseButtonDown(0)) // Left Mouse Button
        {
            animator.SetTrigger("fire");

           
        }
    }
}
