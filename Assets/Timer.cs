using UnityEngine;
using TMPro; // Libreria necessaria per TextMeshPro

public class Timer : MonoBehaviour
{
    [Header("Impostazioni Interfaccia")]
    public TextMeshProUGUI testoTimer; // Riferimento al testo sullo schermo

    private float tempoTrascorso = 0f;
    private bool cronometroAttivo = true;

    void Update()
    {
        if (cronometroAttivo)
        {
            // Aumenta il tempo in base ai secondi effettivi trascorsi
            tempoTrascorso += Time.deltaTime;

            // Calcola minuti, secondi e centesimi per un look da vera speedrun
            int minuti = Mathf.FloorToInt(tempoTrascorso / 60);
            int secondi = Mathf.FloorToInt(tempoTrascorso % 60);
            int centesimi = Mathf.FloorToInt((tempoTrascorso * 100) % 100);

            // Aggiorna la scritta a schermo formattandola come 00:00:00
            testoTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minuti, secondi, centesimi);
        }
    }

    // Puoi richiamare questa funzione da un altro script quando il giocatore tocca la cima!
    public void FermaTimer()
    {
        cronometroAttivo = false;
    }
}