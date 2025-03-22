using UnityEngine;

public class BossFireAtPlayer : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet to fire
    public Transform firePoint; // The point where the bullet is fired
    public float bulletSpeed = 20f; // Speed of the bullet
    public float fireInterval = 2f; // Time between each shot
    public float bulletLifetime = 5f; // Time before the bullet is destroyed

    private Transform player;
    private float nextFireTime;

    private void Start()
    {
        // Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure it has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null && Time.time >= nextFireTime)
        {
            FireAtPlayer();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void FireAtPlayer()
    {
        // Calculate direction to player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Instantiate bullet and apply velocity
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        // Destroy bullet after bulletLifetime seconds
        Destroy(bullet, bulletLifetime);
    }
}