using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Niente più 2D
public class HandGrabber : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public bool IsGrabbing { get; private set; }

    private Collider currentClimbableObject;
    private FixedJoint currentJoint; // FixedJoint in 3D è molto stabile

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            currentClimbableObject = collision;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision == currentClimbableObject)
        {
            currentClimbableObject = null;
        }
    }

    public void TryGrab()
    {
        if (currentClimbableObject != null && !IsGrabbing)
        {
            IsGrabbing = true;
            
            // Crea il giunto 3D
            currentJoint = gameObject.AddComponent<FixedJoint>();
            
            Rigidbody targetRb = currentClimbableObject.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                currentJoint.connectedBody = targetRb;
            }
        }
    }

    public void Release()
    {
        if (IsGrabbing)
        {
            IsGrabbing = false;
            if (currentJoint != null)
            {
                Destroy(currentJoint);
            }
        }
    }
}