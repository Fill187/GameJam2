using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BodyPendulumPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    public HandGrabber leftHand;
    public HandGrabber rightHand;

    [Header("Fisica Personalizzata")]
    public float fallDrag = 0f; // Resistenza dell'aria in caduta libera
    public float swingDrag = 2f; // Resistenza dell'aria mentre si dondola (evita rotazioni folli)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Se almeno una mano è attaccata, aumenta lo smorzamento per un dondolio controllato
        if (leftHand.IsGrabbing || rightHand.IsGrabbing)
        {
            rb.linearDamping = swingDrag;
        }
        else
        {
            // In caduta libera, togli la resistenza per far cadere il giocatore velocemente (effetto punitivo!)
            rb.linearDamping = fallDrag;
        }
    }
}