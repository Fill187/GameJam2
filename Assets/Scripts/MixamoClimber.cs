using UnityEngine;

public class MixamoClimber : MonoBehaviour
{
    [Header("Riferimenti")]
    public Rigidbody rbHandL;
    public Rigidbody rbHandR;
    public Rigidbody rbBody;

    [Header("Impostazioni Movimento")]
    public float handSpeed = 30f;
    public float zDepth = 0f;

    void FixedUpdate()
    {
        // 1. Calcola la posizione del mouse nel mondo 3D
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - zDepth);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // 2. Gestione Mano Sinistra (Click Sinistro)
        if (Input.GetMouseButton(0))
        {
            // SE TENGO PREMUTO: La mano � libera e insegue il mouse
            MoveHandTowards(rbHandL, worldMousePos);
        }
        else
        {
            // SE RILASCIO: La mano si ferma istantaneamente (si aggrappa)
            rbHandL.linearVelocity = Vector3.zero;
        }

        // 3. Gestione Mano Destra (Click Destro)
        if (Input.GetMouseButton(1))
        {
            // SE TENGO PREMUTO: La mano � libera e insegue il mouse
            MoveHandTowards(rbHandR, worldMousePos);
        }
        else
        {
            // SE RILASCIO: La mano si ferma istantaneamente (si aggrappa)
            rbHandR.linearVelocity = Vector3.zero;
        }
    }

    // Funzione che spinge fisicamente la mano verso il bersaglio
    void MoveHandTowards(Rigidbody rb, Vector3 target)
    {
        Vector3 direction = target - rb.position;
        rb.linearVelocity = direction * handSpeed;
    }
}