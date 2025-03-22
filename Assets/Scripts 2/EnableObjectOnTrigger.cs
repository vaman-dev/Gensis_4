using UnityEngine;

public class EnableObjectOnTrigger : MonoBehaviour
{
    public GameObject objectToEnable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
                Debug.Log("Object enabled!");
            }
        }
    }
}
