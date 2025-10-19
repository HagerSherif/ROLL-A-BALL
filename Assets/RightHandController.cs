using UnityEngine;
using Leap;

public class RightHandController : BaseController
{
    private void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }

    private void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }

    public override void HandleInput() { /* Not used */ }

    private void OnUpdateFrame(Frame frame)
    {
        if (frame.Hands.Count == 0) return;

        Vector3? rightHandPos = null;
        float rightPinchStrength = 0f;

        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                rightHandPos = hand.PalmPosition;
                rightPinchStrength = hand.PinchStrength;
            }
        }

        Vector3 ballPos = ball.transform.position;

        // Decide movement based on right hand closeness to ball and pinch strength
        if (rightHandPos.HasValue && (rightHandPos.Value - ballPos).magnitude < right_hand_distance && rightPinchStrength > pinch_strength)
            MoveBallTowardRight(rightHandPos.Value);
    }

    private void MoveBallTowardRight(Vector3 targetPos)
    {    // Move ball by changing position directly to the palm position of right hand
        if (targetPos.y > 0.5f)  //// 0.5 is ballPos.y
            ball.MovePosition(targetPos);
    }
}
