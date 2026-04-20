using UnityEngine;

public class ResizeAndGreen : MonoBehaviour
{
    // Drag your green material into this slot in the Unity Inspector
    public Material Verde;

    void OnMouseDown()
    {
        // 1. Set the exact scale: X=0.2, Y=0.5, Z=0.2
        transform.localScale = new Vector3(1f, 1f, 1f);

        // 2. Swap the material to your green material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && Verde != null)
        {
            renderer.material = Verde;
        }
    }
}