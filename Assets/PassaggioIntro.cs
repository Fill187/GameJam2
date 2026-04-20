using UnityEngine;
using UnityEngine.SceneManagement;

public class PassaggioIntro : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GiocoPrincipale");
        }
    }

}
