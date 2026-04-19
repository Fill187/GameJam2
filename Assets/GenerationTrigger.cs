using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [Header("Impostazioni Trigger")]
    [SerializeField] private string targetTag = "Player"; // Chi deve attivare il trigger?
    private TowerGenerator generator;

    void Start()
    {
        generator = Object.FindFirstObjectByType<TowerGenerator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // 1. Controlliamo se l'oggetto che è entrato ha il tag corretto
        if (other.CompareTag(targetTag))
        {

            Debug.Log("Trigger");
            
            if (generator != null)
            {
                // 3. Chiamiamo il metodo per aggiungere il pezzo
                generator.AddRandomPiece();

                
            }
            else
            {
                Debug.LogWarning("TowerTrigger: Non ho trovato nessun TowerGenerator nella scena!");
            }
        }
    }
}