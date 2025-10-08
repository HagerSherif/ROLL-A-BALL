using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;   // Drag your ball GameObject here in Inspector
    private Vector3 offset;     // The initial difference between camera and ball

    void Start()
    {
        // Calculate the offset at the start
        offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        // Update camera position to always maintain the same offset
        transform.position = Player.transform.position + offset;
    }
}
