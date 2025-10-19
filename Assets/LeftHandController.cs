using UnityEngine;
using Leap;

public class LeftHandController : BaseController
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

        Vector3? leftIndexDir = null;

        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
                leftIndexDir = hand.fingers[1].Direction;
        }

        if (leftIndexDir.HasValue)
            MoveBallTowardLeft(leftIndexDir.Value);
    }

    private void MoveBallTowardLeft(Vector3 targetDir)
    {   //Move ball by applying force in the direction of left index finger
        targetDir.y = transform.position.y;
        targetDir = targetDir.normalized;
        ball.AddForce(targetDir * index_speed);
    }
}
