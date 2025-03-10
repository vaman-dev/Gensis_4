using UnityEngine;

public class Enemey : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; 
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Enemy Died!");
        // Add animation logic here if needed
        Destroy(gameObject); // Remove enemy from the scene
    }
}
