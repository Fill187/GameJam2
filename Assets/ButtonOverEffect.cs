using UnityEngine;
using UnityEngine.UI; // Necessario per usare il tipo Image
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Ora lo script accetta specificamente un componente Image
    public Image glowImage; 

    void Start()
    {
        if (glowImage != null)
            glowImage.enabled = false; // Spegne l'immagine all'inizio
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glowImage != null)
            glowImage.enabled = true; // Accende l'immagine al passaggio del mouse
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (glowImage != null)
            glowImage.enabled = false; // La spegne quando esci
    }
}