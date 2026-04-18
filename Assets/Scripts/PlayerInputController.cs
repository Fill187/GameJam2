using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Riferimenti alle Mani")]
    public HandGrabber leftHand;
    public HandGrabber rightHand;

    [Header("Impostazioni")]
    public float handMoveSpeed = 10f;

    void Update()
    {
        // Ottieni la posizione del mouse nel mondo di gioco
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Input Mano Sinistra (Click Sinistro)
        if (Input.GetMouseButtonDown(0)) leftHand.TryGrab();
        if (Input.GetMouseButtonUp(0)) leftHand.Release();

        // Input Mano Destra (Click Destro)
        if (Input.GetMouseButtonDown(1)) rightHand.TryGrab();
        if (Input.GetMouseButtonUp(1)) rightHand.Release();

        // Muovi le mani verso il mouse se non sono attaccate
        if (!leftHand.IsGrabbing) MoveHandTowards(leftHand, mousePosition);
        if (!rightHand.IsGrabbing) MoveHandTowards(rightHand, mousePosition);
    }

    private void MoveHandTowards(HandGrabber hand, Vector2 targetPos)
    {
        // Sposta fisicamente la mano verso il cursore
        Vector2 newPosition = Vector2.Lerp(hand.rb.position, targetPos, handMoveSpeed * Time.deltaTime);
        hand.rb.MovePosition(newPosition);
    }
}