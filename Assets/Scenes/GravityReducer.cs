using UnityEngine;

public class GravityReducer : MonoBehaviour
{
    [SerializeField] private float reducedGravityScale = 0.5f; // Gravity scale when colliding with umbrella
    [SerializeField] private float originalGravityScale = 9.8f; // Set to Earth's gravity
    [SerializeField] private string groundLayerName = "Ground"; // Ground layer name
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground detection

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Umbrella"))
        {
            rb.gravityScale = reducedGravityScale;
            Debug.Log("Gravity Reduced!");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Umbrella"))
        {
            Debug.Log("Left Umbrella Area. Checking Ground...");
        }
    }

    void Update()
    {
        if (IsGrounded())
        {
            rb.gravityScale = originalGravityScale;
            Debug.Log("Gravity Restored to 9.8!");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, groundCheckRadius, LayerMask.GetMask(groundLayerName)) != null;
    }
}

// Ensure the umbrella has a Collider2D and is tagged 'Umbrella'.
// Set the correct layer name for groundLayerName in the Unity Editor.
// Adjust the groundCheckRadius if necessary for accurate ground detection.
