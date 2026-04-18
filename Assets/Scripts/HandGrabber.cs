using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HandGrabber : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public bool IsGrabbing { get; private set; }

    private Collider2D currentClimbableObject;
    private HingeJoint2D currentJoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Rileva se la mano sta toccando qualcosa di scalabile (usa un Tag "Climbable")
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            currentClimbableObject = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
            
            // Crea un giunto per far dondolare il corpo attorno a questo punto
            currentJoint = gameObject.AddComponent<HingeJoint2D>();
            
            // Collega la mano all'oggetto scalabile (se ha un rigidbody), altrimenti la fissa nello spazio
            Rigidbody2D targetRb = currentClimbableObject.GetComponent<Rigidbody2D>();
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