using UnityEngine;

public class SignalEnvironmentManager : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform playerBody;
    
    [Header("Soglie di Altezza (Peripezie)")]
    public float heightForWind = 50f;
    public float heightForGlitches = 100f;

    [Header("Impostazioni Vento")]
    public Vector2 windForce = new Vector2(-5f, 0f); // Soffia verso sinistra
    private Rigidbody2D playerRb;

    void Start()
    {
        if (playerBody != null)
        {
            playerRb = playerBody.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (playerBody == null) return;

        float currentHeight = playerBody.position.y;

        // Esempio: Attiva glitch visivi superata una certa altezza
        if (currentHeight > heightForGlitches)
        {
            EnableCameraGlitches(true);
        }
        else
        {
            EnableCameraGlitches(false);
        }
    }

    void FixedUpdate()
    {
        if (playerRb == null) return;

        // Esempio: Applica raffiche di vento intermittenti superata una certa altezza
        if (playerBody.position.y > heightForWind)
        {
            // Applica il vento solo alcune volte usando Mathf.Sin per creare raffiche casuali
            if (Mathf.Sin(Time.time * 2f) > 0.5f) 
            {
                playerRb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }

    private void EnableCameraGlitches(bool enable)
    {
        // Qui puoi attivare/disattivare un effetto di post-processing (es. Chromatic Aberration o Film Grain)
        // per simulare il segnale disturbato dell'antenna.
    }
}