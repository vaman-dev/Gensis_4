using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    public float speed = 3f; // Speed of the boss

    private Transform player;

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
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
    