using UnityEngine;

public class CopiaTransform : MonoBehaviour
{
    public Transform targetTransform;
    private void FixedUpdate()
    {
        transform.position=targetTransform.position;
    }
}
