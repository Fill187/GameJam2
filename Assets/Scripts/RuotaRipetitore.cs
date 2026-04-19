using UnityEngine;

public class RuotaRipetitore : MonoBehaviour
{
    public Transform cubeToRotate;
    public AudioSource audioSource;
    public Material Verde;
    public AudioClip Martellata;

    private bool used = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        if (used) return; // only once
        used = true;

        // sound
        if (audioSource != null)
            audioSource.PlayOneShot(Martellata);

        // turn green
        if (rend != null && Verde != null)
            rend.material = Verde;

        // rotate Cube B exactly 5 degrees
        if (cubeToRotate != null)
            cubeToRotate.Rotate(0f, 0f, -5f);
    }
}