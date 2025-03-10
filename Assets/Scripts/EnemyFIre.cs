using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{
    public Transform player;
    public float playerIsInRegion;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 1f; // Time delay between shots
    private bool canFire = true;

    void Update()
    {
        if (player != null)
        {
            Vector2 distance = player.position - transform.position;
            float absDistance = distance.magnitude; // Absolute distance
            Vector2 direction = distance.normalized;
            
            if (absDistance <= playerIsInRegion && canFire)
            {
                Debug.Log("Player is in range");
                StartCoroutine(FireWithDelay(direction));
            }
        }
    }

    IEnumerator FireWithDelay(Vector2 direction)
    {
        canFire = false;
        Fire(direction);
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    void Fire(Vector2 direction)
    {
        Debug.Log("Enemy fires at the player!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}
