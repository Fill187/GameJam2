using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ClimbingHand : MonoBehaviour
{
    [Header("Impostazioni")]
    public string climbableTag = "Climbable";
    public KeyCode grabKey; // Es: Mouse0 per la sinistra, Mouse1 per la destra

    private Rigidbody rb;
    private FixedJoint grabJoint;
    private bool isTouchingClimbable = false;
    private Rigidbody targetRbToGrab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Per evitare che la mano rimbalzi via quando tocca il muro, 
        // è utile che il collider della mano sia (o abbia anche) un Trigger
    }

    void Update()
    {
        // Se premo il tasto e sto toccando un appiglio, afferro
        if (Input.GetKeyDown(grabKey) && isTouchingClimbable)
        {
            Grab();
        }

        // Se rilascio il tasto, lascio la presa
        if (Input.GetKeyUp(grabKey))
        {
            Release();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = true;
            targetRbToGrab = other.attachedRigidbody; // Può essere null se l'appiglio non ha un rigidbody
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = false;
            targetRbToGrab = null;
        }
    }

    private void Grab()
    {
        // Evita di creare più giunti
        if (grabJoint != null) return;

        // Crea un FixedJoint al volo per incollare la mano al muro
        grabJoint = gameObject.AddComponent<FixedJoint>();

        if (targetRbToGrab != null)
        {
            grabJoint.connectedBody = targetRbToGrab;
        }

        // Opzionale: aumenta la drag o modifica la massa durante la presa per stabilizzare il corpo
    }

    private void Release()
    {
        if (grabJoint != null)
        {
            Destroy(grabJoint);
            grabJoint = null;
        }
    }
}