using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterManager : MonoBehaviour
{
    public Transform water;
    public float speed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        // Fa salire/muovere l'acqua
        water.position = new Vector3(water.position.x, water.position.y, water.position.z + speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. RIMETTI VISIBILE E SBLOCCA LA FRECCIA DEL MOUSE
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            // 2. CARICA LA SCENA DI GAME OVER
            SceneManager.LoadScene("GameOver");
        }
    }
}