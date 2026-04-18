using UnityEngine;
using UnityEngine.SceneManagement; // Serve per gestire le scene

public class SceneChanger : MonoBehaviour
{
    // Questa funzione è pubblica, quindi Unity la vede.
    // Il parametro 'nomeScena' lo scriverai tu nel pulsante.
    public void VaiAllaScena(string nomeScena)
    {
        SceneManager.LoadScene(nomeScena);
    }

}