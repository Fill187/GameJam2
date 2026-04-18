using UnityEngine;

public class AdvancedClimbingHand : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform shoulder;
    public Camera mainCamera;
    public string climbableTag = "Climbable";

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

        // 2. CALCOLO POSIZIONE (Il trucco del Piano Matematico)
        if (isFollowingMouse)
        {
            // Creiamo un "muro invisibile" all'altezza della spalla che guarda la telecamera
            Plane plane = new Plane(-mainCamera.transform.forward, shoulder.position);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Troviamo il punto esatto di intersezione tra il mouse e questo piano
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldMousePos = ray.GetPoint(distance);

                // Applichiamo il limite del braccio
                Vector3 directionFromShoulder = worldMousePos - shoulder.position;
                if (directionFromShoulder.magnitude > maxArmLength)
                {
                    directionFromShoulder = directionFromShoulder.normalized * maxArmLength;
                }

                targetPosition = shoulder.position + directionFromShoulder;
            }
        }
    }

    void FixedUpdate()
    {
        // 3. MENTRE TENGO PREMUTO: La mano ricalca la posizione del cursore
        if (Input.GetMouseButton(mouseButton) && grabJoint == null)
        {
            MoveHandToCursor();
        }
    }

    // --- Collisioni ---
    private void OnTriggerEnter(Collider other)
    {
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

    // --- DEBUG VISIVO (Appare solo nella vista Scene di Unity) ---
    void OnDrawGizmos()
    {
        if (Application.isPlaying && isFollowingMouse)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPosition, 0.1f);
            Gizmos.DrawLine(shoulder.position, targetPosition);
        }
    }

    private void MoveHandToCursor()
    {
        float zDistance = Vector3.Distance(mainCamera.transform.position, shoulder.position);
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = zDistance;

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // Limite di estensione del braccio
        Vector3 directionFromShoulder = targetPosition - shoulder.position;
        if (directionFromShoulder.magnitude > maxArmLength)
        {
            // Blocca il target al limite esatto del braccio
            targetPosition = shoulder.position + (directionFromShoulder.normalized * maxArmLength);
        }

        Vector3 moveDirection = targetPosition - rb.position;

        // NUOVO: Se la mano � arrivata a destinazione o � al limite, smettiamo di tirarla
        if (moveDirection.magnitude < 0.05f)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            // La mano si muove verso il target.
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }
}