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
    private Vector3 mouseWorldPosition;

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);

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

        // Calculate direction towards mouse pointer
        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;

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

    private void OnDrawGizmos()
    {
        // Draw a red circle where the mouse is pointing
        Gizmos.color = pointerColor;
        Gizmos.DrawSphere(mouseWorldPosition, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player!");
        }
    }
}
