using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HandGrabber : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public bool IsGrabbing { get; private set; }
    private Collider currentClimbableObject;
    private FixedJoint currentJoint;

    void Awake() { rb = GetComponent<Rigidbody>(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable")) currentClimbableObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentClimbableObject) currentClimbableObject = null;
    }

    public void TryGrab()
    {
        if (currentClimbableObject != null && !IsGrabbing)
        {
            IsGrabbing = true;
            currentJoint = gameObject.AddComponent<FixedJoint>();
            Rigidbody targetRb = currentClimbableObject.GetComponent<Rigidbody>();
            if (targetRb != null) currentJoint.connectedBody = targetRb;
        }
    }

    public void Release()
    {
        if (IsGrabbing)
        {
            IsGrabbing = false;
            if (currentJoint != null) Destroy(currentJoint);
        }
    }
}