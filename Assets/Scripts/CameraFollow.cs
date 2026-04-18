using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // trascina qui il tuo "Climbing" o "PlayerManager"
    public float smoothSpeed = 0.125f; // velocità di inseguimento (più basso è, più è fluido)
    public Vector3 offset; // distanza tra camera e giocatore

    void FixedUpdate()
    {
        if (target != null)
        {
            // La posizione desiderata è la posizione del giocatore + l'offset
            Vector3 desiredPosition = target.position + offset;

            // Usiamo il Lerp per rendere il movimento fluido
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Applichiamo la posizione alla telecamera
            transform.position = smoothedPosition;
        }
    }
}