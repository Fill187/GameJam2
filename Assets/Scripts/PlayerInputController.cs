using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public HandGrabber leftHand;
    public HandGrabber rightHand;
    public float handMoveSpeed = 15f;
    public float zDepth = 0f;

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z - zDepth);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0)) leftHand.TryGrab();
        if (Input.GetMouseButtonUp(0)) leftHand.Release();

        if (Input.GetMouseButtonDown(1)) rightHand.TryGrab();
        if (Input.GetMouseButtonUp(1)) rightHand.Release();

        if (!leftHand.IsGrabbing) MoveHand(leftHand, targetPosition);
        if (!rightHand.IsGrabbing) MoveHand(rightHand, targetPosition);
    }

    private void MoveHand(HandGrabber hand, Vector3 targetPos)
    {
        Vector3 newPos = Vector3.Lerp(hand.rb.position, targetPos, handMoveSpeed * Time.deltaTime);
        hand.rb.MovePosition(newPos);
    }
}