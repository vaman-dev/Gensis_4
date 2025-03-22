using UnityEngine;

public class Firing : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet to fire
    public Transform firePoint; // The point where the bullet is fired
    public float bulletSpeed = 20f; // Speed of the bullet
    public float fireDelay = 0.5f; // Delay between each shot
    public AudioSource fireSound; // Reference to the audio source

    private float lastFireTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireDelay)
        {
            Fire();
        }
    }

    private void Fire()
    {
        lastFireTime = Time.time;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mousePos.x > transform.position.x) ? Vector2.right : Vector2.left;

        // Instantiate bullet and apply velocity
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = fireDirection * bulletSpeed;
        }

        // Play firing sound if assigned
        if (fireSound != null)
        {
            fireSound.Play();
        }
    }
}