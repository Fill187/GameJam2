using UnityEngine;

public class AdvancedClimbingHand : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform shoulder;
    public Camera mainCamera;
    public string climbableTag = "Climbable";
    public Transform mousePlane;

    [Header("Parametri Fisici")]
    public float maxArmLength = 0.8f;
    public float moveSpeed = 15f; // Rimettilo intorno a 15 nell'Inspector!

    [Tooltip("La forza massima in Newton: abbastanza per muovere il braccio, troppo poca per sollevare il corpo intero")]
    public float maxLiftingForce = 200f;
    public int mouseButton; // 0 Sinistro, 1 Destro          

    private Rigidbody rb;
    private FixedJoint grabJoint;
    private bool isTouchingClimbable = false;
    private Rigidbody targetBody;

    private Vector3 targetPosition;
    private bool isFollowingMouse = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Appena clicco, stacco la presa e inizio a seguire
        if (Input.GetMouseButtonDown(mouseButton))
        {
            Release();
            isFollowingMouse = true;
        }

        // 2. Quando rilascio, smetto di seguire e provo ad afferrare
        if (Input.GetMouseButtonUp(mouseButton))
        {
            isFollowingMouse = false;
            if (isTouchingClimbable) Grab();
        }

        // 3. Calcolo del bersaglio basato sul piano invisibile
        if (isFollowingMouse && mousePlane != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane customPlane = new Plane(-mainCamera.transform.forward, mousePlane.position);

            if (customPlane.Raycast(ray, out float distance))
            {
                Vector3 worldMousePos = ray.GetPoint(distance);
                Vector3 directionFromShoulder = worldMousePos - shoulder.position;

                // Elastico del braccio
                if (directionFromShoulder.magnitude > maxArmLength)
                    targetPosition = shoulder.position + (directionFromShoulder.normalized * maxArmLength);
                else
                    targetPosition = worldMousePos;
            }
        }
    }

    void FixedUpdate()
    {
        // 4. Movimento della mano con limite di Forza (Anti-Superman)
        if (isFollowingMouse && grabJoint == null)
        {
            Vector3 moveDirection = targetPosition - rb.position;
            Vector3 targetVelocity = moveDirection * moveSpeed;

            // Calcoliamo la differenza tra come ci stiamo muovendo e come dovremmo muoverci
            Vector3 velocityDiff = targetVelocity - rb.linearVelocity;

            // Trasformiamo questa differenza in forza fisica controllata
            Vector3 force = velocityDiff * rb.mass * 20f;

            // IL TETTO MASSIMO: Se la forza supera il limite, la "tagliamo"
            if (force.magnitude > maxLiftingForce)
            {
                force = force.normalized * maxLiftingForce;
            }

            rb.AddForce(force, ForceMode.Force);
        }
    }

    // --- Collisioni e Aggancio Automatico ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(climbableTag))
        {
            isTouchingClimbable = true;
            targetBody = other.attachedRigidbody;

            // FONDAMENTALE: Si aggrappa da solo SOLO se non stai tenendo premuto il tasto del mouse!
            // Questo impedisce al braccio di bloccarsi mentre striscia sulla torre.
            if (!Input.GetMouseButton(mouseButton))
            {
                Grab();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Questo garantisce che il gioco sappia sempre se sei a contatto col muro
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
            // Se usciamo e non siamo aggrappati, resettiamo
            if (grabJoint == null) targetBody = null;
        }
    }

    // --- Meccaniche di Presa ---
    private void Grab()
    {
        if (grabJoint != null) return;

        rb.linearVelocity = Vector3.zero;
        grabJoint = gameObject.AddComponent<FixedJoint>();
        if (targetBody != null) grabJoint.connectedBody = targetBody;

        isFollowingMouse = false;
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