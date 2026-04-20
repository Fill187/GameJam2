using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScenaPulsante : MonoBehaviour
{
    // Questa funzione DEVE avere "string nomeScena" per farti apparire la casella
    public void CaricaScena(string nomeScena)
    {
        SceneManager.LoadScene(nomeScena);
    }
}