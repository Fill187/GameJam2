using UnityEngine;

public class Tower : MonoBehaviour
{

    public Transform top;
    
    public float GetTop()
    {
        return top.position.y;
    }
}
