using UnityEngine;

public class Coin_Animatio : MonoBehaviour
{
    
    public Animator animator;

    void Start()
    {
        animator.SetBool("Active", true);
    }

    void Update()
    {
        
    }
}
