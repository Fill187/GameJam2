using UnityEngine;

public class ClimbingCamera : MonoBehaviour
{
    [Header("Bersaglio")]
    public Transform target; // Trascina qui l'oggetto Climbing (il corpo)

    [Header("Impostazioni Arrampicata")]
    public float smoothSpeed = 5f; // Velocità di inseguimento
    public float altezzaVisiva = 3f; // Quanto in alto guarda rispetto al giocatore
    public float distanzaZ = -10f; // Quanto sta lontana la telecamera

    [Header("Blocco Orizzontale")]
    public bool bloccaAsseX = true; // Tieni spuntato per non farla muovere a destra/sinistra
    public float centroSchermoX = 0f; // La posizione X fissa della telecamera

    void FixedUpdate()
    {
        if (target == null) return;

        // 1. Calcola dove dovrebbe stare la telecamera
        // Se l'asse X è bloccato, usa 'centroSchermoX', altrimenti segui il giocatore anche di lato
        float nuovaX = bloccaAsseX ? centroSchermoX : target.position.x;

        // Segue il giocatore in verticale, ma aggiunge l'altezza per inquadrare la torre
        float nuovaY = target.position.y + altezzaVisiva;

        // La Z rimane sempre inchiodata, così non sballa mai la profondità
        float nuovaZ = distanzaZ;

        // Crea il punto di destinazione finale
        Vector3 destinazione = new Vector3(nuovaX, nuovaY, nuovaZ);

        // 2. Muovi la telecamera fluidamente verso quel punto
        transform.position = Vector3.Lerp(transform.position, destinazione, smoothSpeed * Time.deltaTime);
    }
}