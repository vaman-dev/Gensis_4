using UnityEngine;

public class FireScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float destroyAfterSeconds = 3f;
    public Color pointerColor = Color.red;
    public Animator animator;
    public float fireDelay = 0.5f;
    public AudioSource fireSound;

    private float lastFireTime;
    private bool facingRight = true;

    private void Update()
    {
        // Fire when left mouse button is clicked with delay
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireDelay)
        {
            Fire();
        }
    }

    private void Fire()
    {
        lastFireTime = Time.time;

        // Trigger the fire animation
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }

        // Play fire sound
        if (fireSound != null)
        {
            fireSound.Play();
        }

        // Direction based on player facing
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        // Instantiate projectile and apply force
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Destroy the projectile after a set time
        Destroy(projectile, destroyAfterSeconds);
    }

    public void Flip(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }

    private void OnDrawGizmos()
    {
        // Draw a red circle at the fire point
        if (firePoint != null)
        {
            Gizmos.color = pointerColor;
            Gizmos.DrawSphere(firePoint.position, 0.2f);
        }
    }
}
