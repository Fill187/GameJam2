using UnityEngine;

public class AdvancedClimbingHand : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform shoulder;
    public Camera mainCamera;
    public string climbableTag = "Climbable";

    [Tooltip("Inserisci qui il piano su cui vuoi far scorrere il cursore")]
    public Transform mousePlane;

    [Header("Parametri Fisici")]
    public float maxArmLength = 0.8f;
    public float moveSpeed = 60f;
    public int mouseButton; // 0 per Sinistro, 1 per Destro          

    private Rigidbody rb;
    private FixedJoint grabJoint;
    private bool isTouchingClimbable = false;
    private Rigidbody targetBody;

    // Variabili per il calcolo
    private Vector3 targetPosition;
    private bool isFollowingMouse = false;

    void Start()
    {
        if(mousePlane == null)
        {
            mousePlane = Object.FindFirstObjectByType<MousePlane>().transform;
        }
        rb = GetComponent<Rigidbody>();

        // Forziamo il Rigidbody a non essere cinematico via codice per sicurezza
        rb.isKinematic = false;

        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. INPUT
        if (Input.GetMouseButtonDown(mouseButton))
        {
            Release();
            isFollowingMouse = true;
        }

        if (Input.GetMouseButtonUp(mouseButton))
        {
            isFollowingMouse = false;
            if (isTouchingClimbable) Grab();
        }

        // 2. CALCOLO POSIZIONE BASATO SUL TUO MOUSEPLANE
        if (isFollowingMouse && mousePlane != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Creiamo un piano matematico usando ESATTAMENTE la posizione e l'inclinazione del tuo mousePlane.
            // Usiamo -mousePlane.forward assumendo che la "faccia" del piano guardi verso la telecamera.
            Plane customPlane = new Plane(-mousePlane.forward, mousePlane.position);

            if (customPlane.Raycast(ray, out float distance))
            {
                Vector3 worldMousePos = ray.GetPoint(distance);

                // Applichiamo il limite del braccio
                Vector3 directionFromShoulder = worldMousePos - shoulder.position;
                if (directionFromShoulder.magnitude > maxArmLength)
                {
                    targetPosition = shoulder.position + (directionFromShoulder.normalized * maxArmLength);
                }
                else
                {
                    targetPosition = worldMousePos;
                }
            }
        }
    }

    void FixedUpdate()
    {
        // 3. MOVIMENTO FISICO
        if (isFollowingMouse && grabJoint == null)
        {
            Vector3 moveDirection = targetPosition - rb.position;

            if (moveDirection.magnitude < 0.05f)
            {
                rb.linearVelocity = Vector3.zero; // Ferma i tremolii
            }
            else
            {
                rb.linearVelocity = moveDirection * moveSpeed;
            }
        }
    }

    // --- Collisioni ---
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other: "+ other.gameObject.name);
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = true;
            targetBody = other.attachedRigidbody;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = false;
            targetBody = null;
        }
    }

    // --- Presa ---
    private void Grab()
    {
        if (grabJoint != null) return;
        rb.linearVelocity = Vector3.zero;
        grabJoint = gameObject.AddComponent<FixedJoint>();
        if (targetBody != null) grabJoint.connectedBody = targetBody;
    }

    private void Release()
    {
        if (grabJoint != null)
        {
            Destroy(grabJoint);
            grabJoint = null;
        }
    }

    // --- DEBUG VISIVO ---
    void OnDrawGizmos()
    {
        if (Application.isPlaying && isFollowingMouse)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPosition, 0.1f);
            Gizmos.DrawLine(shoulder.position, targetPosition);
        }
    }
}