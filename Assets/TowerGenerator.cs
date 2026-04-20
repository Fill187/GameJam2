using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour
{
    [Header("Configurazione Pezzi")]
    [SerializeField] private List<GameObject> towerPrefabs; // Lista dei prefab disponibili
    [SerializeField] private Transform spawnPoint;         // Punto di partenza (base)

    private GameObject _lastPiece; // Riferimento all'ultimo pezzo aggiunto

    void Start()
    {
        AddRandomPiece();
    }

    /// <summary>
    /// Aggiunge un pezzo casuale sopra l'ultimo pezzo della torre.
    /// </summary>
    public void AddRandomPiece()
    {
        if (towerPrefabs == null || towerPrefabs.Count == 0)
        {
            Debug.LogWarning("La lista dei prefab è vuota!");
            return;
        }

        // 1. Scegliamo un pezzo casuale dalla lista
        GameObject prefabToSpawn = towerPrefabs[Random.Range(0, towerPrefabs.Count)];

        // 2. Calcoliamo la posizione di spawn
        Vector3 nextPosition;

        if (_lastPiece == null)
        {
            // Se è il primo pezzo, usa il punto di spawn iniziale
            nextPosition = spawnPoint.position;
        }
        else
        {
            // Calcoliamo l'altezza del pezzo precedente per impilare correttamente
            // Nota: Questo assume che il pivot del prefab sia alla base.
            float offset = _lastPiece.GetComponent<Tower>().GetTop();
            nextPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + offset);
        }

        // 3. Istanziamo il pezzo
        GameObject newPiece = Instantiate(prefabToSpawn, nextPosition, Quaternion.Euler(90,0,0), transform);

        // 4. Aggiorniamo il riferimento per il prossimo round
        _lastPiece = newPiece;
    }

    
}