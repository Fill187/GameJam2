using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BodyPendulumPhysics : MonoBehaviour
{
    private Rigidbody rb;
    public HandGrabber leftHand;
    public HandGrabber rightHand;
    public float fallDrag = 0.1f;
    public float swingDrag = 3f;

    void Awake() { rb = GetComponent<Rigidbody>(); }

    void FixedUpdate()
    {
        bool isGrabbing = (leftHand != null && leftHand.IsGrabbing) || (rightHand != null && rightHand.IsGrabbing);
        rb.linearDamping = isGrabbing ? swingDrag : fallDrag;
    }
}