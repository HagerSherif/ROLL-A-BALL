using UnityEngine;
using Leap;

public abstract class BaseController : MonoBehaviour
{
    [Header("Common Components")]
    public Rigidbody ball;
    public LeapProvider leapProvider;
    public float keyboard_speed = 10f;
    public float index_speed = 0.15f;
    public float pinch_strength = 0.6f; 
    public float right_hand_distance = 5f;

    public abstract void HandleInput();
}
