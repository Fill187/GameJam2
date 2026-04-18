using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Riferimenti alle Mani")]
    public HandGrabber leftHand;
    public HandGrabber rightHand;

    [Header("Impostazioni")]
    public float handMoveSpeed = 15f;
    public float zDepth = 0f; // La profondità a cui si trovano l'antenna e le mani

    void Update()
    {
        // Converti la posizione del mouse in coordinate 3D bloccate su un piano Z fisso
        Vector3 mouseScreenPos = Input.mousePosition;
        // La distanza dalla telecamera al piano in cui si muove il personaggio
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z - zDepth); 
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Input
        if (Input.GetMouseButtonDown(0)) leftHand.TryGrab();
        if (Input.GetMouseButtonUp(0)) leftHand.Release();

        if (Input.GetMouseButtonDown(1)) rightHand.TryGrab();
        if (Input.GetMouseButtonUp(1)) rightHand.Release();

        // Movimento
        if (!leftHand.IsGrabbing) MoveHandTowards(leftHand, targetPosition);
        if (!rightHand.IsGrabbing) MoveHandTowards(rightHand, targetPosition);
    }

    private void MoveHandTowards(HandGrabber hand, Vector3 targetPos)
    {
        Vector3 newPosition = Vector3.Lerp(hand.rb.position, targetPos, handMoveSpeed * Time.deltaTime);
        hand.rb.MovePosition(newPosition);
    }
}