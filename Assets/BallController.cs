using Leap;
using UnityEngine;
// using Leap.Unity;

public class BallController : MonoBehaviour
{
    public LeapProvider leapProvider;
    private Rigidbody ball;            // Rigidbody reference
    public GameObject Player;         

    // Called once at the start
    void Start()
    {
        ball = GetComponent<Rigidbody>();  // Get Rigidbody component from the ball
    }

    // Called at fixed time intervals (best for physics updates)
    void FixedUpdate()
    {
        // Get input from arrow keys or WASD
        float moveHorizontal = Input.GetAxis("Horizontal"); // left/right
        float moveVertical = Input.GetAxis("Vertical");     // up/down

        // Create movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force to Rigidbody to move ball
        ball.AddForce(movement * speed);
    }

    private void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }

    private void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }

    void OnUpdateFrame(Frame frame)
{
    if (frame.Hands.Count == 0) return;

    Vector3? leftIndexDir = null;         // Variable for left fingertip direction
    Vector3? rightHandPos = null;         // Variable for right hand palm positions
    float rightPinchStrength = 0f;

    foreach (Hand hand in frame.Hands)
    {
        if (hand.IsLeft)
        {   
            leftIndexDir = hand.fingers[1].Direction;
        }
        else if (hand.IsRight)
        {
            rightHandPos = hand.PalmPosition;
            rightPinchStrength = hand.PinchStrength;
        }
    }

    Vector3 ballPos = transform.position;

    // Decide movement based on right hand closeness to ball and pinch strength
    if (rightHandPos.HasValue && (rightHandPos.Value - ballPos).magnitude < 5f && rightPinchStrength > 0.6f)
    {
        Debug.Log("Right hand ");
        MoveBallTowardRight(rightHandPos.Value);
    }
    else if (leftIndexDir.HasValue)
    {
        Debug.Log("Left hand ");
        MoveBallTowardLeft(leftIndexDir.Value);
    }
}


    void MoveBallTowardRight(Vector3 targetPos)
    {
        if (targetPos.y > 0.5f)       // 0.5 is transform.position.y
        {   // Move ball by changing position directly to the palm position of right hand
            ball.GetComponent<Rigidbody>().MovePosition(targetPos);
        }
        // else
        // {
        //     Vector3 targetPosSameY  = new Vector3(targetPos.x, 0.5f, targetPos.z);
        //     ball.GetComponent<Rigidbody>().MovePosition(targetPosSameY );
        // }
    }

    void MoveBallTowardLeft(Vector3 targetDir)
    {   //Move ball by applying force in the direction of left index finger
        targetDir.y = transform.position.y;
        targetDir = targetDir.normalized;
        ball.AddForce(targetDir * 0.15f);
    }
}
