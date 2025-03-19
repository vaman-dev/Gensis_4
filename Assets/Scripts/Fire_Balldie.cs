using UnityEngine;

public class Fire_Balldie : MonoBehaviour
{
    // This method is called when the collider other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destroy this game object
            Destroy(gameObject);
        }
    }
}