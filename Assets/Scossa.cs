using UnityEngine;
using UnityEngine.SceneManagement;

public class Scossa : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOver");
        }
    }
}
