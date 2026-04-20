using System.Collections;
using UnityEngine;

public class CadutaOggetti : MonoBehaviour
{
    public GameObject puntoA;
    public GameObject puntoB;
    public GameObject oggetto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnOggetto());
    }

    IEnumerator SpawnOggetto()
    {
        while (true)
        {
            Vector3 punto1=puntoA.transform.position;
            Vector3 punto2=puntoB.transform.position;
            float random = Random.value;
            Vector3 positionForSpawn=Vector3.Lerp(punto1,punto2,random);
            GameObject go = GameObject.Instantiate(oggetto);
            go.transform.position = positionForSpawn;

            yield return new WaitForSeconds(5f);
        }
    }
}
