using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPoint; 

    void Start()
    {
        respawnPoint = transform.position; 
        
        
         }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint")) 
        {
            respawnPoint = other.transform.position; 
            Debug.Log("Checkpoint updated to: " + respawnPoint);
        }
        else if (other.CompareTag("EnemyBullet")) 
        {
            Die();
        }
    }

    public void Die()
    {
        
       
        
            Respawn(); 
        
    }

    public void Respawn()
    {
        
        transform.position = respawnPoint; 
        }
    }

