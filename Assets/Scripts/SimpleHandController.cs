using UnityEngine;

public class SimpleHandController : MonoBehaviour
{
    [Header("Riferimenti")]
    public Camera mainCamera;
    public Transform mousePlane; // Il piano che definisce la profondità Z

    [Header("Impostazioni")]
    [Tooltip("0 per Tasto Sinistro, 1 per Tasto Destro")]
    public int mouseButton = 0;

    // Questa variabile ricorda se stiamo attualmente trascinando questa palla
    private bool isDragging = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // 1. Quando PREMIAMO il tasto del mouse, controlliamo cosa stiamo cliccando
        if (Input.GetMouseButtonDown(mouseButton))
        {
            // Creiamo un raggio che parte dallo schermo e va verso il mondo 3D
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Lanciamo il raggio: se colpisce qualcosa...
            if (Physics.Raycast(ray, out hit))
            {
                // ...e quel qualcosa è esattamente questa palla, iniziamo a trascinarla!
                if (hit.transform == this.transform)
                {
                    isDragging = true;
                }
            }
        }

        // 2. Quando RILASCIAMO il tasto, smettiamo di trascinare
        if (Input.GetMouseButtonUp(mouseButton))
        {
            isDragging = false;
        }

        // 3. Se stiamo effettivamente trascinando questa palla, aggiorniamo la sua posizione
        if (isDragging && mousePlane != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Usiamo il solito piano matematico per mantenere la profondità corretta
            Plane customPlane = new Plane(-mainCamera.transform.forward, mousePlane.position);

            if (customPlane.Raycast(ray, out float distance))
            {
                // Spostiamo la sfera
                transform.position = ray.GetPoint(distance);
            }
        }
    }
}