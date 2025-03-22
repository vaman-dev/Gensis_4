using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float torqueForce = 5f;
    public float delayTime = 2f;

    [Header("Audio")]
    public AudioSource runAudio; // Running audio source

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D feetCollider;
    public SpriteRenderer spriteRenderer;

    private Vector2 move;
    private bool isAlive = true;

    private void Start()
    {
        // Cache component references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Validate component assignments
        if (rb == null) Debug.LogError("❌ Rigidbody2D is missing!");
        if (feetCollider == null) Debug.LogError("❌ BoxCollider2D is missing!");
        if (animator == null) Debug.LogError("❌ Animator is missing!");
        if (spriteRenderer == null) Debug.LogError("❌ SpriteRenderer is missing!");
        if (runAudio == null) Debug.LogWarning("⚠️ Run audio source not assigned.");
    }

    private void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ApplyTorque();
            Fire();
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>(); // Direct assignment
    }

    private void Run()
    {
        if (rb == null) return; // Safety check

        rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool("Moving", isMoving);
        animator.SetBool("Idle", !isMoving);

        // Play or stop run audio
        if (runAudio != null)
        {
            if (isMoving && !runAudio.isPlaying)
            {
                runAudio.Play();
            }
            else if (!isMoving && runAudio.isPlaying)
            {
                runAudio.Stop();
            }
        }
    }

    public void OnJump(InputValue value)
    {
        if (!isAlive || rb == null || feetCollider == null)
        {
            Debug.LogError("❌ Rigidbody or BoxCollider2D is missing!");
            return;
        }

        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("⚠️ Player is not grounded.");
            return;
        }

        if (value.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            rb.AddTorque(torqueForce * -Mathf.Sign(rb.linearVelocity.x));
            Debug.Log("🟢 Player jumped successfully.");
        }
    }

    private void ApplyTorque()
    {
        if (rb == null) return;

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
        if (spriteRenderer == null || rb == null) return;

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
            Debug.Log("🔥 Fire triggered.");
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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log($"🟢 Loading scene {nextSceneIndex}");
        }
        else
        {
            Debug.Log("❌ No more levels to load.");
        }
    }
}
