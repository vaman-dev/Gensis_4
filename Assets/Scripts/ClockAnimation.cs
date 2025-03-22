using UnityEngine;

public class ClockAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateClockAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("start", true);
            Debug.Log("Clock animation started.");
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }
    }

    public void DeactivateClockAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("start", false);
            Debug.Log("Clock animation stopped.");
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }
    }
}
