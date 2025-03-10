using UnityEngine;

public class FloatingPlayer : MonoBehaviour
{
    [SerializeField] private float reducedGravityScale = 0.5f; 
    [SerializeField] private float torqueAmount = 5f; 
    private Rigidbody2D rb;
    private Player Player; 
    private float originalGravity;
    
    [SerializeField] private Collider2D feetCollider;
    [SerializeField] private bool isAlive = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>(); 
        originalGravity = rb.gravityScale; 
    }

    void Update()
    {
        if (!isAlive || !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            EnableFloating(); 
        }
        else
        {
            DisableFloating();
        }
    }

    void EnableFloating()
    {
        rb.gravityScale = reducedGravityScale;

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torqueAmount, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torqueAmount, ForceMode2D.Force);
        }

        if (Player != null)
        {
            Player.enabled = false; 
        }
    }

    void DisableFloating()
    {
        rb.gravityScale = originalGravity; 
        rb.angularVelocity = 0f;

        if (Player != null)
        {
            Player.enabled = true; 
        }
    }
}
