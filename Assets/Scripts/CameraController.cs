using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target da seguire")]
    public Transform playerBody;

    [Header("Impostazioni")]
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0, 2f, -10f); // Tieni la telecamera un po' più in alto del giocatore

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (playerBody != null)
        {
            Vector3 targetPosition = playerBody.position + offset;
            
            // SmoothDamp crea un ritardo fluido molto piacevole per i giochi di arrampicata
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}