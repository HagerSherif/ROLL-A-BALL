using UnityEngine;

public class KeyboardController : BaseController
{
    void FixedUpdate()
    {
        HandleInput();
    }

    public override void HandleInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        ball.AddForce(movement * keyboard_speed);
    }
}
