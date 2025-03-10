using UnityEngine;

public class LightFollow : MonoBehaviour
{
  private Light myLight;  
  public float time ;
    [SerializeField] private GameObject Player;

    void Update()
    {
        if (Player != null)  // if the player is present or not 

        {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, Time.deltaTime * time);

        }
    }
}
