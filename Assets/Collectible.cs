using UnityEngine;

public class Collectible : MonoBehaviour
{

    private TextController text_controller;

    void Start()
    {
        text_controller = FindObjectOfType<TextController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            // inactivate this collectible
            text_controller.OnCollectibleCaptured();
            gameObject.SetActive(false);
        }
    }
}



