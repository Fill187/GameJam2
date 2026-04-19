using UnityEngine;

public class Bullone : MonoBehaviour
{
    public Material Verde;
    public AudioSource audioSource;
    public AudioClip Martellata;

    private bool hasExecuted = false;

    void OnMouseDown()
    {
        Debug.Log("click");
        if (hasExecuted) return;

        // Change material
        Renderer rend = GetComponent<Renderer>();
        if (rend != null && Verde != null)
        {
            rend.material = Verde;
        }

        // Move object on Z axis
        transform.position += new Vector3(0f, 0f, -0.09f);

        // Play sound
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Martellata);
        }

        hasExecuted = true;
    }
}