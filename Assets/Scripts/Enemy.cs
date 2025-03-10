using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionRange = 5f;
    private Animator enemyAnimator;
    private SpriteRenderer enemySpriteRenderer;
    public AudioSource horrorAudio;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        EnemyRun();
    }

    void EnemyRun()
    {
        if (player == null) return;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            transform.position += (Vector3)directionToPlayer * speed * Time.deltaTime;
            enemyAnimator.SetBool("Moving", true);
            enemyAnimator.SetBool("Idle", false);
            
            // Play horror audio only if it's not already playing
            if (!horrorAudio.isPlaying)
            {
                horrorAudio.Play();
            }

            // Flip enemy sprite based on direction
            enemySpriteRenderer.flipX = directionToPlayer.x < 0;
        }
        else
        {
            enemyAnimator.SetBool("Moving", false);
            enemyAnimator.SetBool("Idle", true);
            
            // Pause the horror audio instead of stopping it immediately
            if (horrorAudio.isPlaying)
            {
                horrorAudio.Pause();
            }
        }
    }
}


