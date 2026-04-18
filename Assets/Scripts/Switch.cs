using UnityEngine;

public class Switch : MonoBehaviour
{
    public Material verde; // Trascina qui il materiale nell'Inspector
    public Vector3 rotazioneTarget = new Vector3(-60, 0, 0); // La rotazione desiderata

    [Header("Audio Settings")]
    public AudioClip click; 
    public AudioSource audioSource;

    // Questa funzione viene chiamata quando clicchi sull'oggetto
    // NOTA: L'oggetto deve avere un COLLIDER per rilevare il click
    private void OnMouseDown()
    {
        // Cambia la rotazione
        transform.eulerAngles = rotazioneTarget;

        // Cambia il materiale (se ne hai assegnato uno)
        if (verde != null)
        {
            GetComponent<Renderer>().material = verde;
        }
        audioSource.PlayOneShot(click);
    }
}