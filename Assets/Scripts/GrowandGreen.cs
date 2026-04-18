using UnityEngine;

public class GrowAndGreen : MonoBehaviour
{
    // Drag your green material into this slot in the Inspector
    public Material Verde;

    void OnMouseDown()
    {
        // 1. Double the size
        transform.localScale *= 2f;

        // 2. Swap the material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && Verde != null)
        {
            renderer.material = Verde;
        }
    }
}